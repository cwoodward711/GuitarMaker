using FakeItEasy;
using GuitarMaker;
using NUnit.Framework;
using System;

namespace GuitarMakerTests
{
    [TestFixture]
    public class OpenGuitarCommandTests
    {
        private Guitar guitar;

        [SetUp]
        public void Setup()
        {
            guitar = new Guitar();
            guitar.Body = new Component();
            guitar.Headstock = new Component();
            guitar.FretBoard = new Component();
            guitar.Nut = new Component();
            guitar.Tuner = new Component();
            guitar.Pickup_N = new Component();
            guitar.Pickup_B = new Component();
            guitar.Pickup_M = new Component();
            guitar.Knob = new Component();
            guitar.Switch = new Component();
            guitar.Jack = new Component();
            guitar.StrapPeg = new Component();
            guitar.PickGuard = new Component();
            guitar.Bridge = new Component();
            guitar.Body.Name = "GuitarBody";            
        }
        
        private OpenGuitarCommand CreateOpenGuitarCommand()
        {
            var dir = Environment.CurrentDirectory;
            string fileName = dir + @"\Resources\TestSave.json";

            GuitarSaver.Save(fileName, guitar);

            return new OpenGuitarCommand(guitar, fileName);
        }

        [Test]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var openGuitarCommand = this.CreateOpenGuitarCommand();
            guitar.Body.Name = "AnotherBody";

            // Act
            openGuitarCommand.Execute();

            // Assert
            Assert.AreEqual(guitar.Body.Name, "GuitarBody");
        }

        [Test]
        public void UnExecute_StateUnderTest_ExpectedBehavior()
        {
            var openGuitarCommand = this.CreateOpenGuitarCommand();
            guitar.Body.Name = "AnotherBody";

            // Act
            openGuitarCommand.Execute();
            openGuitarCommand.UnExecute();

            // Assert
            Assert.AreEqual(guitar.Body.Name, "GuitarBody");
        }
    }
}
