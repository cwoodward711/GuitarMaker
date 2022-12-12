using FakeItEasy;
using GuitarMaker;
using NUnit.Framework;
using System;

namespace GuitarMakerTests
{
    [TestFixture]
    public class CommandHistoryTests
    {
        private CommandHistory CreateEmptyCommandHistory()
        {
            return new CommandHistory();

        }

        [Test]
        public void Execute_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var commandHistory = this.CreateEmptyCommandHistory();
            ICommand command = A.Fake<ICommand>();

            // Act
            commandHistory.Execute(command);

            // Assert
            A.CallTo(() => command.Execute()).MustHaveHappened();
            Assert.IsTrue(commandHistory.undoLeft());
            Assert.IsFalse(commandHistory.redoLeft());
        }

        [Test]
        public void Undo_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var commandHistory = this.CreateEmptyCommandHistory();
            ICommand command = A.Fake<ICommand>();

            // Act
            commandHistory.Execute(command);
            commandHistory.Undo();

            // Assert
            A.CallTo(() => command.UnExecute()).MustHaveHappened();
        }

        [Test]
        public void Redo_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var commandHistory = this.CreateEmptyCommandHistory();
            ICommand command = A.Fake<ICommand>();

            // Act
            commandHistory.Execute(command);
            commandHistory.Undo();
            commandHistory.Redo();

            // Assert
            A.CallTo(() => command.Execute()).MustHaveHappenedTwiceExactly();
        }

        [Test]
        public void undoLeft_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var commandHistory = this.CreateEmptyCommandHistory();

            Assert.AreEqual(false, commandHistory.undoLeft());
            ICommand command = A.Fake<ICommand>();

            // Act
            commandHistory.Execute(command);
            
            // Assert
            Assert.IsTrue(commandHistory.undoLeft());
        }

        [Test]
        public void redoLeft_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var commandHistory = this.CreateEmptyCommandHistory();


            Assert.AreEqual(false, commandHistory.undoLeft());
            ICommand command = A.Fake<ICommand>();

            // Act
            commandHistory.Execute(command);
            commandHistory.Undo();

            // Assert
            Assert.IsTrue(commandHistory.redoLeft());
        }
    }
}
