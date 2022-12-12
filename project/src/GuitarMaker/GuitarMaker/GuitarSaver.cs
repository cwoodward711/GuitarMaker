using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Text;
using System;

namespace GuitarMaker
{
    /// <summary>
    /// Class is a collection of static methods used for saving and loading guitars.
    /// </summary>
    public class GuitarSaver
    {       
        /// <summary>
        /// Saves a guitar to a file.
        /// </summary>
        public static void Save(string fileName, Guitar guitar)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(guitar));
        }
        
        /// <summary>
        /// Loads a guitar from a file. Returns loaded guitar.
        /// </summary>
        public static Guitar Load(string fileName)
        {
            string text = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<Guitar>(text);
        }

        /// <summary>
        /// Exports a guitar to a txt file.
        /// </summary>
        public static void Export(string fileName, Guitar guitar)
        {
            StringBuilder guitarString = new StringBuilder();
            guitarString.AppendLine(string.Format("Guitar exported {0:ddd, MMM d, yyyy}\n", DateTime.Now));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10}", "Component", "Name", "Price"));
            guitarString.AppendLine(new string('-', 64));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Body", guitar.Body.Name, guitar.Body.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Headstock", guitar.Headstock.Name, guitar.Headstock.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Fret Board", guitar.FretBoard.Name, guitar.FretBoard.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Nut", guitar.Nut.Name, guitar.Nut.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Tuner", guitar.Tuner.Name, guitar.Tuner.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Neck Pickup", guitar.Pickup_N.Name, guitar.Pickup_N.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Middle Pickup", guitar.Pickup_M.Name, guitar.Pickup_M.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Body Pickup", guitar.Pickup_B.Name, guitar.Pickup_B.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Knob", guitar.Knob.Name, guitar.Knob.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Switch", guitar.Switch.Name, guitar.Switch.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Jack", guitar.Jack.Name, guitar.Jack.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Strap Peg", guitar.StrapPeg.Name, guitar.StrapPeg.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Pick Guard", guitar.PickGuard.Name, guitar.PickGuard.Price));
            guitarString.AppendLine(string.Format("{0,-15} {1,-35} | {2,10:C}", "Bridge", guitar.Bridge.Name, guitar.Bridge.Price));
            guitarString.AppendLine(new string('-', 64));
            guitarString.AppendLine(string.Format("{0,-15} {1,35} | {2,10:C}", "", "Total Price:", guitar.CalculatePrice()));
            

            File.WriteAllText(fileName, guitarString.ToString());
        }
    }
}