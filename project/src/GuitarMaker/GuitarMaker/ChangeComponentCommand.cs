namespace GuitarMaker {
    public class ChangeComponentCommand : ICommand {
        /// <summary>
        /// The change component command will 
        private Guitar guitar;
        private Component oldComponent;
        private Component newComponent;

        /// <summary>
        /// Constructor for the ChangeComponentCommand.
        /// </summary>
        public ChangeComponentCommand(Guitar guitar, Component component) {
            this.guitar = guitar;
            newComponent = component;
            switch (component.Type) {
                case ComponentType.Body:
                    oldComponent = guitar.Body;
                    break;
                case ComponentType.Headstock:
                    oldComponent = guitar.Headstock;
                    break;
                case ComponentType.Fretboard:
                    oldComponent = guitar.FretBoard;
                    break;
                case ComponentType.Nut:
                    oldComponent = guitar.Nut;
                    break;
                case ComponentType.Tuner:
                    oldComponent = guitar.Tuner;
                    break;
                case ComponentType.Pickup_N:
                    oldComponent = guitar.Pickup_N;
                    break;
                case ComponentType.Pickup_M:
                    oldComponent = guitar.Pickup_M;
                    break;
                case ComponentType.Pickup_B:
                    oldComponent = guitar.Pickup_B;
                    break;
                case ComponentType.Knob:
                    oldComponent = guitar.Knob;
                    break;
                case ComponentType.Switch:
                    oldComponent = guitar.Switch;
                    break;
                case ComponentType.Jack:
                    oldComponent = guitar.Jack;
                    break;
                case ComponentType.Strap_Peg:
                    oldComponent = guitar.StrapPeg;
                    break;
                case ComponentType.Pickguard:
                    oldComponent = guitar.PickGuard;
                    break;
                case ComponentType.Bridge:
                    oldComponent = guitar.Bridge;
                    break;
            }
        }

        /// <summary>
        /// Execute the change component command. Changes component on the guitar to the one passed.
        /// </summary>
        private void setComponent(Component component)
        {
            switch(component.Type)
            {
                case ComponentType.Strap_Peg:
                    guitar.StrapPeg = component;
                    break;
                case ComponentType.Knob:
                    guitar.Knob = component;
                    break;
                case ComponentType.Nut:
                    guitar.Nut = component;
                    break;
                case ComponentType.Pickguard:
                    guitar.PickGuard = component;
                    break;
                case ComponentType.Pickup_M:
                    guitar.Pickup_M = component;
                    break;
                case ComponentType.Pickup_N:
                    guitar.Pickup_N = component;
                    break;
                case ComponentType.Pickup_B:
                    guitar.Pickup_B = component;
                    break;
                case ComponentType.Body:
                    guitar.Body = component;
                    break;
                case ComponentType.Bridge:
                    guitar.Bridge = component;
                    break;
                case ComponentType.Fretboard:
                    guitar.FretBoard = component;
                    break;
                case ComponentType.Headstock:
                    guitar.Headstock = component;
                    break;
                case ComponentType.Jack:
                    guitar.Jack = component;
                    break;
                case ComponentType.Switch:
                    guitar.Switch = component;
                    break;
                case ComponentType.Tuner:
                    guitar.Tuner = component;
                    break;
            }
        }

        /// <summary>
        /// Executes the change component command.
        /// </summary>
        public void Execute() {
            setComponent(newComponent);
        }

        /// <summary>
        /// UnExecutes the change component command.
        /// </summary>
        public void UnExecute() {
            setComponent(oldComponent);
        }
    }
}