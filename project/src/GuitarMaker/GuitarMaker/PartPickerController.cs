using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GuitarMaker
{

    /// <summary>
    /// PartPickerController is our program's controller which links our model to our view (MainWindow).
    /// </summary>
    public class PartPickerController
    {
        private const string NULL_IMG_PATH = "pack://application:,,,/Resources/BlankImage.png";

        private Guitar _currentGuitar;
        private CommandHistory _commandHistory;

        private Button _undoButton;
        private Button _redoButton;
        private MainWindow _mainWindow;
        private Dictionary<Button, Component> _buttons = new Dictionary<Button, Component>();
        private string _partsListPath = null;
        public IComponentCollection ComponentCollection { get; set; }

        /// <summary>
        /// Constructor for PartPickerController. Assigns data members and initializes the command history.
        /// </summary>
        public PartPickerController(MainWindow mainWindow)
        {
            ComponentCollection = new ConcreteComponentCollection(_partsListPath);
            _currentGuitar = new Guitar(ComponentCollection);
            _commandHistory = new CommandHistory();
            _mainWindow = mainWindow;
            _undoButton = (Button)mainWindow.FindName("undo");
            _redoButton = (Button)mainWindow.FindName("redo");
            LoadAllComponentButtons();
        }

        /// <summary>
        /// Loads all buttons (of every type) for part selection.
        /// </summary>
        private void LoadAllComponentButtons()
        {
            foreach (ComponentType componentType in Enum.GetValues(typeof(ComponentType)))
            {
                LoadComponentButtons(componentType);
            }
        }

        /// <summary>
        /// Loads all the buttons for a given component type.
        /// </summary>
        private void LoadComponentButtons(ComponentType type)
        {
            string containerName = type.ToString() + "Stack";
            StackPanel container = _mainWindow.FindName(containerName) as StackPanel;
            if (container == null)
            {
                return;
            }
            foreach (Component component in ComponentCollection.GetComponents(type)) {
                LoadComponentButton(container, component);
            }
        }

        /// <summary>
        /// Loads a single button for a given component.
        /// </summary>
        private void LoadComponentButton(StackPanel container, Component component)
        {
            ColumnDefinition columnDefinition = new ColumnDefinition();

            Button button = GenerateButton(component);

            container.Children.Add(button);
            _buttons.Add(button, component);
        }

        /// <summary>
        /// Generates a button for a given component.
        /// </summary>
        private Button GenerateButton(Component component)
        {
            Button button = new Button();
            button.Height = 100;
            button.Padding = new Thickness(5);
            button.Margin = new Thickness(10);
            
            button.Content = GenerateStackPanel(component);
            button.Click += PartClicked;

            return button;
        }

        /// <summary>
        /// Method called when a part button is clicked. The sender is passed to the method,
        /// so it knows which part to change.
        /// </summary>
        private void PartClicked(object sender, RoutedEventArgs e)
        {
            Component component = _buttons[(Button)sender];
            _commandHistory.Execute(new ChangeComponentCommand(_currentGuitar, component));
            RefreshImages();
        }

        /// <summary>
        /// Generates a stack panel which holds the part image.
        /// </summary>
        private StackPanel GenerateStackPanel(Component component)
        {
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;
            panel.Children.Add(GenerateImage(component));
            return panel;
        }

        /// <summary>
        /// Returns the image for a given component.
        /// </summary>
        private Image GenerateImage(Component component)
        {
            Image image = new Image();
            image.Width = 68;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            if (component.Image != null)
            {
                bitmapImage.UriSource = new System.Uri(component.Image);
            } else
            {
                bitmapImage.UriSource = new System.Uri(NULL_IMG_PATH);
            }
            bitmapImage.DecodePixelWidth = 68;
            bitmapImage.EndInit();

            image.Source = bitmapImage;
            return image;
        }
        
        /// <summary>
        /// Updates the prices displayed on the GUI. DOES NOT refresh prices via the API.
        /// </summary>
        public void UpdatePriceComponents()
        {
            Dictionary<string, double> prices = new Dictionary<string, double>();
            prices[_currentGuitar.Body.Name] = _currentGuitar.Body.Price;
            prices[_currentGuitar.Bridge.Name] = _currentGuitar.Bridge.Price;
            prices[_currentGuitar.FretBoard.Name] = _currentGuitar.FretBoard.Price;
            prices[_currentGuitar.Headstock.Name] = _currentGuitar.Headstock.Price;
            prices[_currentGuitar.Knob.Name] = _currentGuitar.Knob.Price;
            prices[_currentGuitar.Nut.Name] = _currentGuitar.Nut.Price;
            prices[_currentGuitar.PickGuard.Name] = _currentGuitar.PickGuard.Price;
            prices[_currentGuitar.Pickup_B.Name] = _currentGuitar.Pickup_B.Price;
            prices[_currentGuitar.Pickup_M.Name] = _currentGuitar.Pickup_M.Price;
            prices[_currentGuitar.Pickup_N.Name] = _currentGuitar.Pickup_N.Price;
            prices[_currentGuitar.StrapPeg.Name] = _currentGuitar.StrapPeg.Price;
            prices[_currentGuitar.Switch.Name] = _currentGuitar.Switch.Price;
            prices[_currentGuitar.Tuner.Name] = _currentGuitar.Tuner.Price;
            prices[_currentGuitar.Jack.Name] = _currentGuitar.Jack.Price;

            _mainWindow.UpdatePrice(prices);
        }

        /// <summary>
        /// Undo method called when the undo button is clicked.
        /// Undoes the last command in the command history, updates the guitar image.
        /// </summary>
        public void Undo(object sender, RoutedEventArgs e)
        {
            _commandHistory.Undo();
            RefreshImages();


        }

        /// <summary>
        /// Redo method called when the redo button is clicked.
        /// Redoes the last command in the command history, updates the guitar image.
        /// </summary>
        public void Redo(object sender, RoutedEventArgs e)
        {
            _commandHistory.Redo();
            RefreshImages();
        }

        /// <summary>
        /// Refreshes the images of the parts on the GUI. Also updates the prices on the GUI (does not update via API).
        /// </summary>
        public void RefreshImages()
        {
            _mainWindow.SetComponentImage(ComponentType.Body, 100, 100, new Thickness(1), _currentGuitar.Body.Image);
            _mainWindow.SetComponentImage(ComponentType.Headstock, 100, 100, new Thickness(1), _currentGuitar.Headstock.Image);
            _mainWindow.SetComponentImage(ComponentType.Fretboard, 100, 100, new Thickness(1), _currentGuitar.FretBoard.Image);
            _mainWindow.SetComponentImage(ComponentType.Nut, 100, 100, new Thickness(1), _currentGuitar.Nut.Image);
            _mainWindow.SetComponentImage(ComponentType.Tuner, 100, 100, new Thickness(1), _currentGuitar.Tuner.Image);
            _mainWindow.SetComponentImage(ComponentType.Pickup_B, 100, 100, new Thickness(1), _currentGuitar.Pickup_B.Image);
            _mainWindow.SetComponentImage(ComponentType.Pickup_M, 100, 100, new Thickness(1), _currentGuitar.Pickup_M.Image);
            _mainWindow.SetComponentImage(ComponentType.Pickup_N, 100, 100, new Thickness(1), _currentGuitar.Pickup_N.Image);
            _mainWindow.SetComponentImage(ComponentType.Bridge, 100, 100, new Thickness(1), _currentGuitar.Bridge.Image);
            _mainWindow.SetComponentImage(ComponentType.Knob, 100, 100, new Thickness(1), _currentGuitar.Knob.Image);
            _mainWindow.SetComponentImage(ComponentType.Switch, 100, 100, new Thickness(1), _currentGuitar.Switch.Image);
            _mainWindow.SetComponentImage(ComponentType.Jack, 100, 100, new Thickness(1), _currentGuitar.Jack.Image);
            _mainWindow.SetComponentImage(ComponentType.Strap_Peg, 100, 100, new Thickness(1), _currentGuitar.StrapPeg.Image);
            _mainWindow.SetComponentImage(ComponentType.Pickguard, 100, 100, new Thickness(1), _currentGuitar.PickGuard.Image);
            UpdatePriceComponents();

            _undoButton.IsEnabled = _commandHistory.undoLeft();
            _redoButton.IsEnabled = _commandHistory.redoLeft();
        }

        /// <summary>
        /// Updates the prices of all components in the program using the API.
        /// </summary>
        public async Task UpdatePricesAsync()
        {
            await ComponentCollection.UpdatePricesAsync();
        }

        /// <summary>
        /// Loads a guitar from a file.
        /// </summary>
        public void LoadGuitar(string fileName)
        {
            _commandHistory.Execute(new OpenGuitarCommand(_currentGuitar, fileName));
            RefreshImages();
        }
        
        /// <summary>
        /// Saves the current guitar to a file that can be reopened later.
        /// </summary>
        public void SaveGuitar(string fileName)
        {
            GuitarSaver.Save(fileName, _currentGuitar);
        }
        
        /// <summary>
        /// Saves the current guitar to a human readable text file.
        /// </summary>
        public void ExportGuitar(string fileName)
        {
            GuitarSaver.Export(fileName, _currentGuitar);
        }
    }
}