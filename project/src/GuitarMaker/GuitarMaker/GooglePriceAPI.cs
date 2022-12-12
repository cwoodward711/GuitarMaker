using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nito.AsyncEx;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System.Linq;

namespace GuitarMaker
{
    public class GooglePriceApi : IPriceAPI
    {
        private RestClient _client;
        private IPriceCache _priceCache = null;

        private readonly string API_KEY = "JBFFIOUXVECIZSLOCTYGHEHJWMSTDPRAQJAFEUALGRQBZYPVQSQRLZBUOYMWLNHF";
        private readonly string BASE_URL = "https://api.priceapi.com/v2/jobs";

        /// <summary>
        /// Initializes a rest client with default headers and parameters for future requests
        /// </summary>
        public GooglePriceApi()
        {

            _client = new RestClient(BASE_URL);
            _client.AddDefaultQueryParameter(
                    "token", API_KEY
                );
            _client.AddDefaultHeader("Accept", "application/json");
            _client.AddDefaultHeader("Content-Type", "application/json");
            _client.AddDefaultQueryParameter("country", "us");
            _client.AddDefaultQueryParameter("source", "google_shopping");
        }

        /// <summary>
        /// Sends a request to the API with the passed parameters and returns the response
        /// </summary>
        private async Task<dynamic> RequestAsync(Dictionary<string, dynamic> queryParams, Method method,
            string endPoint = "/")
        {
            RestRequest request = new RestRequest(endPoint, method);
            foreach (string key in queryParams.Keys)
            {
                dynamic val = queryParams[key];
                if (val is string)
                {
                    request.AddQueryParameter(key, (string)val);
                }
                else
                {
                    request.AddQueryParameter(key, (int)val);
                }
            }
            RestResponse response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject(response.Content);
        }

        /// <summary>
        /// This method runs until the job completes and returns whether the job completed or not.
        /// </summary>
        private async Task<bool> AwaitJobDoneAsync(string jobId)
        {
            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>();

            string status = null;
            do
            {
                await Task.Delay(3000); // This limits the rate in which the API is queried
                dynamic jsonResponse = await RequestAsync(queryParams, Method.Get, jobId);
                status = jsonResponse["status"];
                Trace.WriteLine(status);
            } while (status != "finished" && status != "cancelled" && status != null);

            return status == "finished";
        }

        /// <summary>
        /// This Method asynchronously creates a Job with the passed parameters, and returns the job ID.
        /// </summary>
        private async Task<string> CreateJobAsync(Dictionary<string, dynamic> parameters)
        {
            dynamic jsonResponse = await RequestAsync(parameters, Method.Post);
            return jsonResponse["job_id"];
        }

        /// <summary>
        /// Returns job response for specified ID.
        /// </summary
        private async Task<dynamic> GetJobResponseAsync(string jobId)
        {
            return await RequestAsync(new Dictionary<string, dynamic>(), Method.Get, jobId + "/download");
        }

        /// <summary>
        /// Asynchronously queries to download multiple jobs simultaneously
        /// </summary>
        private async Task<List<dynamic>> GetJobResponsesAsync(List<string> jobIds)
        {
            List<dynamic> jobResponses = new List<dynamic>();
            List<Task<dynamic>> jobsToQuery = (from string jobId in jobIds select GetJobResponseAsync(jobId)).ToList();
            while (jobsToQuery.Count() > 0)
            {
                Task<dynamic> jobTask = await Task.WhenAny(jobsToQuery);
                dynamic jobResult = jobTask.Result;
                jobResponses.Add(jobResult);
                jobsToQuery.Remove(jobTask);
            }
            return jobResponses;
        }

        /// <summary>
        /// General asynchronous method for completing Jobs with the Cost API. This method creates a job,
        /// waits for it to finish, and returns the job results as a RestResponse
        /// </summary>
        private async Task<dynamic> CompleteJobAsync(Dictionary<string, dynamic> parameters)
        {
            string jobId = await CreateJobAsync(parameters);
            bool success = await AwaitJobDoneAsync(jobId);
            if (success)
            {
                return await GetJobResponseAsync(jobId);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Async method which returns prices for each product name by making an API call. Uses 1 credit for each item in productNames.
        /// </summary>
        private async Task<Dictionary<string, double>> RequestPrices(List<string> productNames)
        {
            Dictionary<string, double> prices = new Dictionary<string, double>();

            Dictionary<string, dynamic> queryParams = new Dictionary<string, dynamic>
            {
                { "topic", "search_results" },
                { "key", "term" },
                { "values", String.Join("\n", productNames) }
            };

            dynamic jobResponse = await CompleteJobAsync(queryParams);
            foreach (dynamic result in jobResponse["results"])
            {
                if (result["success"] == true
                    && result["query"]["topic"] == "search_results"
                    && result["query"]["source"] == "google_shopping")
                {
                    string productName = result["query"]["value"];
                    string priceString = result["content"]["search_results"][0]["price"];
                    if (priceString is null)
                    {
                        priceString = "0";
                    }
                    double price = Double.Parse(priceString);
                    prices[productName] = price;
                    _priceCache.SetPrice(productName, price);
                }
            }

            return prices;
        }

        /// <summary>
        /// Updates local cache of prices based on already aquired prices found in cached jobs
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCacheAsync()
        {
            _priceCache = new ConcretePriceCache();
            dynamic responseJobList = await RequestAsync(new Dictionary<string, dynamic> { { "per_page", 100 } }, Method.Get);
            dynamic jobList = responseJobList["data"];
            List<string> jobIdsToQuery = new List<string>();
            foreach (dynamic job in jobList)
            {
                jobIdsToQuery.Add((string)job["job_id"]);
            }

            List<dynamic> jobResponses = await GetJobResponsesAsync(jobIdsToQuery);

            foreach (dynamic jobResponse in jobResponses)
            {
                if (jobResponse["success"] == false)
                {
                    continue;
                }
                foreach (dynamic result in jobResponse["results"])
                {
                    if (result["success"] == true
                    && result["query"]["topic"] == "search_results"
                    && result["query"]["source"] == "google_shopping")
                    {
                        string productName = result["query"]["value"];
                        string price = result["content"]["search_results"][0]["price"];
                        if (price is null)
                        {
                            price = "0";
                        }
                        _priceCache.SetPrice(productName, Double.Parse(price));
                    }
                }
            }
        }

        /// <summary>
        /// This asynchronous method returns a dictionary of productName, Price for each productName in productNames
        /// </summary>
        public async Task<Dictionary<string, double>> GetPricesAsync(List<string> productNames)
        {
            if (_priceCache is null)
            {
                throw new InvalidOperationException("UpdateCache() must be called once prior to using GetPrice()");
            }

            Dictionary<string, double> cachedPrices = new Dictionary<string, double>();
            List<string> productsToRequest = new List<string>();

            foreach (string productName in productNames)
            {
                double cached_val = _priceCache.GetPrice(productName);
                if (cached_val == -1)
                {
                    productsToRequest.Add(productName);
                }
                else
                {
                    cachedPrices[productName] = cached_val;
                }
            }

            if (productsToRequest.Count > 0)
            {
                Dictionary<string, double> requestedPrices = await RequestPrices(productsToRequest);
                // merge the cached and requested prices together
                Dictionary<string, double> mergedPrices = new List<Dictionary<string, double>>() {
                    cachedPrices, requestedPrices
                }.SelectMany(x => x).ToDictionary(x => x.Key, y => y.Value);

                return mergedPrices;
            }
            else
            {
                return cachedPrices;
            }
        }
    }
}
