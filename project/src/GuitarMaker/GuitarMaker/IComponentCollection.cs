using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuitarMaker
{
    /// <summary>
    /// Interface for a component collection. Allows querying of components, and has functionality for
    /// updating component prices.
    /// </summary>
    public interface IComponentCollection
    {
        /// <summary>
        /// Returns a list of all components of a given type.
        /// </summary>
        List<Component> GetComponents(ComponentType Type);
        
        /// <summary>
        /// Asynchronous method that updates prices for a list of components based on names
        /// </summary>
        Task UpdatePricesAsync();
    }
}