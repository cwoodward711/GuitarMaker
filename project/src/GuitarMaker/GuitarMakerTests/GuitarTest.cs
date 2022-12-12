using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuitarMaker;
using Newtonsoft.Json;
using NUnit.Framework;



namespace GuitarMakerTests
{
    public class GuitarTest
    {

        [Test]
        public void TestGuitarPrice()
        {
            Guitar guitar = new Guitar();

            Component body = new Component();
            Component headstock = new Component();
            Component fretboard = new Component();
            Component nut = new Component();
            Component tuner = new Component();
            Component PickupH = new Component();
            Component PickupM = new Component();
            Component PickupB = new Component();
            Component Knob = new Component();
            Component Switch = new Component();
            Component Jack = new Component();
            Component StrapPeg = new Component();
            Component PickGuard = new Component();
            Component Bridge = new Component();

            body.Price = 5;
            headstock.Price = 10;
            fretboard.Price = 15;
            nut.Price = 20;
            tuner.Price = 25;
            PickupH.Price = 30;
            PickupM.Price = 35;
            PickupB.Price = 40;
            Knob.Price = 45;
            Switch.Price = 50;
            Jack.Price = 55;
            StrapPeg.Price = 60;
            PickGuard.Price = 65;
            Bridge.Price = 70;

            guitar.Body = body;
            guitar.Headstock = headstock;
            guitar.FretBoard = fretboard;
            guitar.Nut = nut;
            guitar.Tuner = tuner;
            guitar.Pickup_N = PickupH;
            guitar.Pickup_M = PickupM;
            guitar.Pickup_B = PickupB;
            guitar.Knob = Knob;
            guitar.Switch = Switch;
            guitar.Jack = Jack;
            guitar.StrapPeg = StrapPeg;
            guitar.PickGuard = PickGuard;
            guitar.Bridge = Bridge;

            Assert.AreEqual(5 + 10 + 15 + 20 + 25 + 30 + 35 + 40 + 45 + 50 + 55 + 60 + 65 + 70, guitar.CalculatePrice());
        }


    }
}
