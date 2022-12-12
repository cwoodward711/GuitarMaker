using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GuitarMaker
{
    /// <summary>
    /// Concrete implementation of IComponentCollection. Loads a collection of components from a JSON file.
    /// </summary>
    public class ConcreteComponentCollection : IComponentCollection
    {
        private Dictionary<ComponentType, List<Component>> _componentDict = new Dictionary<ComponentType, List<Component>>();
        private IPriceAPI _priceAPI;
        private IPriceCache _idCache;

        /// <summary>
        /// Constructor for the ConcreteComponentCollection.
        /// </summary>
        public ConcreteComponentCollection(string partsListPath)
        {
            readJson(partsListPath);
            _priceAPI = new GooglePriceApi();
        }

        /// <summary>
        /// Parse json file for parts.
        /// </summary>
        public void readJson(string dir)
        {
            _componentDict.Clear();
            if (dir == null)
                dir = Directory.GetCurrentDirectory() + "/Resources/PartsList.json";
            var PartsListFile = File.ReadAllText(dir);
            dynamic PartsList = JsonConvert.DeserializeObject(PartsListFile);

            foreach (ComponentType componentType in Enum.GetValues(typeof(ComponentType)))
            {
                _componentDict.Add(componentType, new List<Component>());
                foreach (dynamic body in PartsList[componentType.ToString()])
                {
                    Component Comp = new Component();
                    Comp.Name = body["name"];
                    Comp.Price = body["default-price"];
                    Comp.Image = body["image"];
                    Comp.Type = componentType;
                    _componentDict[componentType].Add(Comp);
                }
            }
        }

        /// <summary>
        /// Get a list of components of a specific type.
        /// </summary>
        public List<Component> GetComponents(ComponentType Type) { return _componentDict[Type]; }

        /// <summary>
        /// Get a list of all components.
        /// </summary>
        private List<Component> GetAllComponents()
        {
            List<Component> components = new List<Component>();
            foreach (ComponentType componentType in Enum.GetValues(typeof(ComponentType)))
            {
                components.AddRange(GetComponents(componentType));
            }
            return components;
        }

        /// <summary>
        /// Asynchronous method to update prices of all components.
        /// </summary>
        public async Task UpdatePricesAsync()
        {
            await _priceAPI.UpdateCacheAsync();
            
            Dictionary<string, double> prices = await _priceAPI.GetPricesAsync((from Component component in GetAllComponents() select component.Name).ToList());
            
            foreach (Component component in GetAllComponents())
            {
                component.Price = prices[component.Name];
            }
        }
    }
}
