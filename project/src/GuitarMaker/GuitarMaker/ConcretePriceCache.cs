using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GuitarMaker
{
    /// <summary>
    /// Concrete implementation of IPriceCache
    /// </summary>
    public class ConcretePriceCache : IPriceCache
    {
        private string _filePath;

        /// <summary>
        /// Constructor for the ConcretePriceCache. Aquires the filepath for the file for caching prices.
        /// </summary>
        public ConcretePriceCache(string cacheFileName="price_cache.json")
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string guitarMakerFolder = Path.Combine(appDataFolder, "GuitarMaker");
            Directory.CreateDirectory(guitarMakerFolder);
            _filePath = Path.Combine(guitarMakerFolder, cacheFileName);
        }

        /// <summary>
        /// Deserializes the cache file into a dictionary of string, double pairs. Dictionary maps product name
        /// to price.
        /// </summary>
        private Dictionary<string, double> Deserialize()
        {
            try
            {
                string text = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<Dictionary<string, double>>(text);
            } catch (FileNotFoundException)
            {
                return new Dictionary<string, double>();
            }
        }

        /// <summary>
        /// Serializes the dictionary of string, double pairs into a json string, and writes it to the cache file.
        /// </summary>
        private void Serialize(Dictionary<string, double> ids)
        {
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(ids));
        }
        
        /// <summary>
        /// Returns cached price for the given product name.
        /// </summary>
        public double GetPrice(string componentName)
        {
            double priceNotFoundPrice = -1;
            Dictionary<string, double> idDict = Deserialize();
            if (idDict.ContainsKey(componentName))
            {
                return idDict[componentName];
            }
            else
            {
                return priceNotFoundPrice;
            }
        } 

        /// <summary>
        /// Updates the cache with the given product name and price.
        /// </summary>
        public void SetPrice(string componentName, double value)
        {
            Dictionary<string, double> idDict = Deserialize();
            idDict[componentName] = value;
            Serialize(idDict);
        }

        /// <summary>
        /// Clears the cache, removing all cached prices.
        /// </summary>
        public void ClearCache()
        {
            Serialize(new Dictionary<string, double>());
        }
    }
}
