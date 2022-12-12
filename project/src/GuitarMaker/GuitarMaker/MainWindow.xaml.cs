using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Diagnostics;
using Nito.AsyncEx;
using Microsoft.Win32;
using System.Drawing.Imaging;
using System.Data;

namespace GuitarMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PartPickerController _partPickerController;

        /// <summary>
        /// MainWindow constructor. Instantiates the PartPickerController, and initializes the price table.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _partPickerController = new PartPickerController(this);

            DataGrid priceTable = (DataGrid)FindName("PriceTable");
            DataGridTextColumn col1 = (DataGridTextColumn)priceTable.Columns[0];
            DataGridTextColumn col2 = (DataGridTextColumn)priceTable.Columns[1];

            col1.Binding = new Binding("Component");
            col2.Binding = new Binding("Price");

            _partPickerController.UpdatePriceComponents();
            _partPickerController.RefreshImages();

        }

        /// <summary>
        /// Reassigns the image for a specific component type.
        /// </summary>
        public void SetComponentImage(ComponentType componentType, int width, int height, Thickness margin, string imagepath)
        {
            foreach (Image image in findGuitarImages(componentType))
            {
                AssignImageSource(image, imagepath);
            }
        }

        /// <summary>
        /// Called once the window content is loaded. Asynchronosly updates part prices.
        /// </summary>
        protected override async void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            await _partPickerController.UpdatePricesAsync();
        }

        /// <summary>
        /// Returns the list of UI Image components that correspond to a specific componeent type.
        /// </summary>
        private List<Image> findGuitarImages(ComponentType componentType)
        {
            List<Image> images = new List<Image>();
            Image img = (Image)FindName(componentType.ToString() + "Image");
            if (img != null)
            {
                images.Add(img);
            }
            else
            {
                for(int i = 1; FindName(componentType.ToString() + "Image" + i.ToString()) != null; i ++)
                {
                    images.Add((Image)FindName(componentType.ToString() + "Image" + i.ToString()));
                }
            }

            return images;
        }

        /// <summary>
        /// Assigns an image to a UI Image component.
        /// </summary>
        private void AssignImageSource(Image image, string imagePath)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new System.Uri(imagePath);
            bitmapImage.DecodePixelWidth = (int)image.Width;
            bitmapImage.EndInit();

            image.Source = bitmapImage;
        }

        /// <summary>
        /// Called when the user clicks the "Save" button.
        /// </summary>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "json files (*.json)|*.json";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == true)
                _partPickerController.SaveGuitar(saveFileDialog.FileName);
        }

        /// <summary>
        /// Called when the user clicks the "Load" button.
        /// </summary>
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "json files (*.json)|*.json|all files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                _partPickerController.LoadGuitar(openFileDialog.FileName);
        }
        

        /// <summary>
        /// Called when the user clicks the "Export" button.
        /// </summary>
        private void Export(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == true)
                _partPickerController.ExportGuitar(saveFileDialog.FileName);
        }

        /// <summary>
        /// Called when the user clicks the "Undo" button.
        private void Undo(object sender, RoutedEventArgs e)
        {
            _partPickerController.Undo(sender, e);
        }

        /// <summary>
        /// Called when the user clicks the "Redo" button.
        /// </summary>
        private void Redo(object sender, RoutedEventArgs e)
        {
            _partPickerController.Redo(sender, e);
        }

        /// <summary>
        /// DataStruct used for adding prices to the price table.
        /// </summary>
        private struct DataStruct {
            
            public string Component { get; set; }
            public string Price { get; set; }
        }

        /// <summary>
        /// Updates the prices on the price table based on passed prices.
        /// </summary>
        public void UpdatePrice(Dictionary<string, double> prices)
        {
            DataGrid priceTable = (DataGrid)FindName("PriceTable");
            
            double sum = 0;

            priceTable.Items.Clear();
            
            foreach (var name in prices.Keys)
            {
                priceTable.Items.Add(new DataStruct { Component = name, Price = String.Format("{0:C}", prices[name]) } );
                sum += prices[name];
            }

            priceTable.Items.Add(new DataStruct { Component = "Total", Price = String.Format("{0:C}", sum) });
        }
        
        /// <summary>
        /// Necessary empty method used for tab controls
        /// </summary>
        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}