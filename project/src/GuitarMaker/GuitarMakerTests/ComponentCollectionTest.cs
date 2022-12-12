using GuitarMaker;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarMakerTests
{
    internal class ComponentCollectionTest
    {
        private IComponentCollection ComponentCollection;
        
        [SetUp]
        public void Setup()
        {
            ComponentCollection = new ConcreteComponentCollection(null);
        }

        [Test]
        public void CountBodiesTest()
        {
            Assert.AreEqual(ComponentCollection.GetComponents(ComponentType.Body).Count, 6);
        }

        [Test]
        public void BodyLoadsCorrectly()
        {
            Component testBody = new Component();
            testBody.Name = "Double Cut Body - Maple";
            testBody.Price = 210.00;
            testBody.Type = ComponentType.Body;
            testBody.Image = "pack://application:,,,/Resources/Body/Body_DC_Maple.png";

            bool eq = testBody.Equals(ComponentCollection.GetComponents(ComponentType.Body)[0]);


            Assert.IsTrue(eq);
        }
    }
}
