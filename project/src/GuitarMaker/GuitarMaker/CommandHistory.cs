using System.Collections.Generic;

namespace GuitarMaker {
    public class CommandHistory {
        /// <summary>
        /// Stores the commands that have been executed.
        /// </summary>
        private Stack<ICommand> undoStack;
        private Stack<ICommand> redoStack;

        /// <summary>
        /// Constructor for the CommandHistory.
        /// </summary>
        public CommandHistory() {
            undoStack = new Stack<ICommand>();
            redoStack = new Stack<ICommand>();
        }

        /// <summary>
        /// Executes a command.
        /// </summary>
        public void Execute(ICommand command) {
            command.Execute();
            undoStack.Push(command);
            redoStack.Clear();
        }

        /// <summary>
        /// Undoes the last command.
        /// </summary>
        public void Undo() {
            if (undoStack.Count > 0) {
                ICommand command = undoStack.Pop();
                command.UnExecute();
                redoStack.Push(command);
            }
        }

        /// <summary>
        /// Redoes the last command.
        /// </summary>
        public void Redo() {
            if (redoStack.Count > 0) {
                ICommand command = redoStack.Pop();
                command.Execute();
                undoStack.Push(command);
            }
        }

        /// <summary>
        /// Returns whether the undo stack is empty.
        /// </summary>
        public bool undoLeft()
        {
            return undoStack.Count > 0;
        }

        /// <summary>
        /// Returns whether the redo stack is empty.
        /// </summary>
        public bool redoLeft()
        {
            return redoStack.Count > 0;
        }
    }
}