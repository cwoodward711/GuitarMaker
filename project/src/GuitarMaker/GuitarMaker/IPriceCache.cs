using System;

namespace GuitarMaker
{
    /// <summary>
    /// Interface for a cache of componeent prices.
    /// </summary>
    public interface IPriceCache
    {
        /// <summary>
        /// Gets the cached price of a component. Returns -1 if the component is not in the cache.
        /// </summary>
        double GetPrice(string componentName);

        /// <summary>
        /// Sets the cached price of a component.
        /// </summary>
        void SetPrice(string componentName, double value);
        
        /// <summary>
        /// Clears the component Cache.
        /// </summary>
        void ClearCache();
        
    }
}