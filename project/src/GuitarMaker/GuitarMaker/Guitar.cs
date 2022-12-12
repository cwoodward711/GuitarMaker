using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace GuitarMaker
{
    /// <summary>
    /// A guitar is a collection of components.
    /// </summary>
    public class Guitar : ICloneable
    {
        public Component Body { get; set; }
        public Component Headstock { get; set; }
        public Component FretBoard { get; set; }
        public Component Nut { get; set; }
        public Component Tuner { get; set; }
        public Component Pickup_N { get; set; }
        public Component Pickup_M { get; set; }
        public Component Pickup_B { get; set; }
        public Component Knob { get; set; }
        public Component Switch { get; set; }
        public Component Jack { get; set; }
        public Component StrapPeg { get; set; }
        public Component PickGuard { get; set; }
        public Component Bridge { get; set; }

        /// <summary>
        /// Creates a new guitar with all components set to null.
        /// </summary>
        public Guitar()
        { }

        /// <summary>
        /// Creates a deep copy of the Guitar
        /// </summary>
        public Guitar(Guitar guitar)
        {
            Body = (Component)guitar.Body.Clone();
            Headstock = (Component)guitar.Headstock.Clone();
            FretBoard = (Component)guitar.FretBoard.Clone();
            Nut = (Component)guitar.Nut.Clone();
            Tuner = (Component)guitar.Tuner.Clone();
            Pickup_N = (Component)guitar.Pickup_N.Clone();
            Pickup_M = (Component)guitar.Pickup_M.Clone();
            Pickup_B = (Component)guitar.Pickup_B.Clone();
            Knob = (Component)guitar.Knob.Clone();
            Switch = (Component)guitar.Switch.Clone();
            Jack = (Component)guitar.Jack.Clone();
            StrapPeg = (Component)guitar.StrapPeg.Clone();
            PickGuard = (Component)guitar.PickGuard.Clone();
            Bridge = (Component)guitar.Bridge.Clone();
        }

        /// <summary>
        /// Constructs a guitar based from a component collection. The first part listed of
        /// each component type is used.
        /// </summary>
        public Guitar(IComponentCollection componentCollection)
        {
            Body = componentCollection.GetComponents(ComponentType.Body)[0];
            Headstock = componentCollection.GetComponents(ComponentType.Headstock)[0];
            FretBoard = componentCollection.GetComponents(ComponentType.Fretboard)[0];
            Nut = componentCollection.GetComponents(ComponentType.Nut)[0];
            Tuner = componentCollection.GetComponents(ComponentType.Tuner)[0];
            Pickup_N = componentCollection.GetComponents(ComponentType.Pickup_N)[1];
            Pickup_M = componentCollection.GetComponents(ComponentType.Pickup_M)[1];
            Pickup_B = componentCollection.GetComponents(ComponentType.Pickup_B)[1];
            Knob = componentCollection.GetComponents(ComponentType.Knob)[0];
            Switch = componentCollection.GetComponents(ComponentType.Switch)[0];
            Jack = componentCollection.GetComponents(ComponentType.Jack)[0];
            StrapPeg = componentCollection.GetComponents(ComponentType.Strap_Peg)[0];
            PickGuard = componentCollection.GetComponents(ComponentType.Pickguard)[0];
            Bridge = componentCollection.GetComponents(ComponentType.Bridge)[5];
        }

        /// <summary>
        /// Clones the current guitar.
        /// </summary>
        public object Clone()
        {
            return new Guitar(this);
        }

        public double CalculatePrice()
        {
            double priceTotal = 0;
            priceTotal += Body.Price;
            priceTotal += Headstock.Price;
            priceTotal += FretBoard.Price;
            priceTotal += Nut.Price;
            priceTotal += Tuner.Price;
            priceTotal += Pickup_N.Price;
            priceTotal += Pickup_M.Price;
            priceTotal += Pickup_B.Price;
            priceTotal += Knob.Price;
            priceTotal += Switch.Price;
            priceTotal += Jack.Price;
            priceTotal += StrapPeg.Price;
            priceTotal += PickGuard.Price;
            priceTotal += Bridge.Price;

            return priceTotal;
        }
    }
}
