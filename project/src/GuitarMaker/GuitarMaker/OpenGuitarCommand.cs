namespace GuitarMaker {

    /// <summary>
    /// OpenGuitarCommand impolements ICommand interface.
    /// Command used to open a guitar file.
    /// </summary>
    public class OpenGuitarCommand : ICommand {
        private Guitar _oldGuitar;
        private string _guitarFile;

        private Guitar _guitarReference;

        /// <summary>
        /// OpenGuitarCommand constructor. Saves references to the old guitar, the current guitar and the file name.
        /// </summary>
        public OpenGuitarCommand(Guitar guitarReference, string guitarFile) {
            this._guitarReference = guitarReference;
            this._oldGuitar = (Guitar)guitarReference.Clone();
            this._guitarFile = guitarFile;
        }

        /// <summary>
        /// Assigns components on newGuitar to guitarReference.
        /// </summary>
        private void AssignComponents(Guitar guitarReference, Guitar newGuitar)
        {
            guitarReference.Body = newGuitar.Body;
            guitarReference.Headstock = newGuitar.Headstock;
            guitarReference.FretBoard = newGuitar.FretBoard;
            guitarReference.Nut = newGuitar.Nut;
            guitarReference.Tuner = newGuitar.Tuner;
            guitarReference.Pickup_N = newGuitar.Pickup_N;
            guitarReference.Pickup_M = newGuitar.Pickup_M;
            guitarReference.Pickup_B = newGuitar.Pickup_B;
            guitarReference.Knob = newGuitar.Knob;
            guitarReference.Switch = newGuitar.Switch;
            guitarReference.Jack = newGuitar.Jack;
            guitarReference.StrapPeg = newGuitar.StrapPeg;
            guitarReference.PickGuard = newGuitar.PickGuard;
            guitarReference.Bridge = newGuitar.Bridge;
        }

        /// <summary>
        /// Executes the command. Assigns guitar components based on what's found within the guitar file.
        /// </summary>
        public void Execute() {
            AssignComponents(_guitarReference, GuitarSaver.Load(_guitarFile));
        }

        /// <summary>
        /// Reverts command execution. Reassigns guitar components to how they were before.
        /// </summary>
        public void UnExecute() {
            AssignComponents(_guitarReference, _oldGuitar);
        }
    }
}