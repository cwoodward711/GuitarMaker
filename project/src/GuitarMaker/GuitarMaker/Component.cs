using System;

namespace GuitarMaker
{
    public class Component : ICloneable
    {
        /// <summary>

        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public ComponentType Type { get; set; }

        public Component () {}

        /// <summary>
        /// Returns a deep copy of the component
        /// </summary>
        public Component(Component component) {
            Name = component.Name is null ? null : string.Copy(component.Name);
            Price = component.Price;
            Image = component.Image is null ? null : String.Copy(component.Image);
            Type = component.Type;
        }

        /// <summary>
        /// Returns whehther the component is equal to another component
        /// </summary>
        public bool Equals(Object? other)
        {
            if (other == null)
            {
                return false;
            }
            Component otherComponent = (Component)other;
            return this.Name.Equals(otherComponent.Name);
        }

        /// <summary>
        /// Creates a deep copy of the component
        /// </summary>
        public Object Clone()
        {
            return new Component(this);
        }
    }

}
