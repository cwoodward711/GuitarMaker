using FakeItEasy;
using GuitarMaker;
using NUnit.Framework;
using System;

namespace GuitarMakerTests
{
    [TestFixture]
    public class ComponentTests
    {


        [SetUp]
        public void SetUp()
        {

        }

        private Component CreateComponent()
        {
            Component component = new Component();
            component.Name = "Test Component";
            component.Price = 100;
            component.Image = "Test Image";
            component.Type = ComponentType.Body;
            return component;
        }

        [Test]
        public void Equals_StateUnderTest_ExpectedBehavior()
        {
            var component = this.CreateComponent();
            Component nullComponent = null;
            Component equalComponent = this.CreateComponent();
            Component differentComponent = this.CreateComponent();
            differentComponent.Name = "Different Name";

            Assert.IsFalse(component.Equals(nullComponent));
            Assert.IsTrue(component.Equals(equalComponent));
            Assert.IsFalse(component.Equals(differentComponent));
        }

        [Test]
        public void Clone_StateUnderTest_ExpectedBehavior()
        {
            var component = this.CreateComponent();
            var cloned = component.Clone();

            Assert.IsTrue(component.Equals(cloned));
        }
    }
}
