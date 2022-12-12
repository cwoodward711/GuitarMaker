namespace GuitarMaker
{
    /// <summary>
    /// Command interface. Used for implementation of command pattern and undo/redo functionality.
    /// </summary>
     public interface ICommand {
        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute();
        
        /// <summary>
        /// Un-executes the command.
        /// </summary>
        void UnExecute();
    }  
}