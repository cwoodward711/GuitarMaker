using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuitarMaker
{
    /// <summary>
    /// Interface for a price API. Allows for the updating and retrevial of prices of components 
    /// based on component names.
    /// </summary>
    public interface IPriceAPI
    {
        /// <summary>
        /// Updates all prices for the API.
        /// </summary>
        Task UpdateCacheAsync();
        
        /// <summary>
        /// Asynchronous method that updates prices for a list of components based on names
        /// </summary>
        Task<Dictionary<string, double>> GetPricesAsync(List<string> productNames);
    }
}