using FakeItEasy;
using GuitarMaker;
using NUnit.Framework;

namespace GuitarMakerTests
{
    [TestFixture]
    public class ChangeComponentCommandTests
    {
        private Guitar fakeGuitar;
        private Component fakeComponent1;
        private Component fakeComponent2;

        [SetUp]
        public void SetUp()
        {
            this.fakeGuitar = A.Fake<Guitar>();
            this.fakeComponent1 = A.Fake<Component>();
            this.fakeComponent2 = A.Fake<Component>();
            this.fakeComponent1.Name = "Component1";
            this.fakeComponent2.Name = "Component2";
        }


        [Test]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            fakeGuitar.Body = fakeComponent1;
            var changeComponentCommand = new ChangeComponentCommand(fakeGuitar, fakeComponent2);

            // Act
            changeComponentCommand.Execute();

            // Assert
            Assert.AreEqual(fakeComponent2, fakeGuitar.Body);
        }

        [Test]
        public void UnExecute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            fakeGuitar.Body = fakeComponent1;
            var changeComponentCommand = new ChangeComponentCommand(fakeGuitar, fakeComponent2);

            // Act
            changeComponentCommand.Execute();
            changeComponentCommand.UnExecute();

            // Assert
            Assert.AreEqual(fakeComponent1, fakeGuitar.Body);
        }
    }
}
