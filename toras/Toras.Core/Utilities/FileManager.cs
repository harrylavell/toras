using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Toras.Utilities
{
    public static class FileManager
    {
        // Stores the path of the data.txt file used throughout the program
        private static readonly string dataPath = Application.StartupPath + "/" + "data.txt";
        private static readonly string debugPath = Application.StartupPath + "/" + "debug.txt";

        /* FileManager initialisation */
        public static void Init()
        {
            File.Delete(debugPath); // Delete debug.txt
        }

        /* Writes parsed string[] data to file.
         * @param string[], directory paths & modifier settings */
        public static void Save(string[] data)
        {   
            File.WriteAllLines(dataPath, data); // Writes output array to data.txt
        }

        /* Load data file, reading all data to string[].
         * @return string[], contains directory paths and modifier settings */
        public static string[] Load()
        {
            //File.Delete(debugPath); // Delete debug.txt

            // If file exists, load it
            if (File.Exists(dataPath))
                return File.ReadAllLines(dataPath);
            return new string[8];
        }

        public static void Move(string source, string destination)
        {
            File.Move(source, destination);
        }

        /* Checks if data.txt exists within home directory
         * @return false, if data.txt found
         * @return true, if data.txt not found */
        public static bool DataExists()
        {
            if (File.Exists(dataPath))
                return false;
            return true;
        }

        /* Searches parsed directory for the parsed file paths.
         * @return true, if the file has successfully been loaded by torrent client
         * @return false, if file still exists within directory after 5 seconds*/
        private static void Loaded(string file)
        {
            File.AppendAllText(debugPath, "Loaded? ");

            if (File.Exists(file))
                File.AppendAllText(debugPath, "Torrent Failed to Load"  + Environment.NewLine);
            else
                File.AppendAllText(debugPath, "Torrent Loaded Successfully" + Environment.NewLine);
            File.AppendAllText(debugPath, Environment.NewLine);
        }

        /* Write parameter string to debug.txt */
        public static void WriteDebug(string output)
        {
            File.AppendAllText(debugPath, output + Environment.NewLine);
        }

        /* Records a log of the file transfer to debug.txt */
        private static void Log(string s, string destination, string fileName)
        {
            File.AppendAllText(debugPath, DateTime.Now + Environment.NewLine);
            File.AppendAllText(debugPath, "File Name: " + fileName + Environment.NewLine);
            File.AppendAllText(debugPath, "Source: " + s + Environment.NewLine);
            File.AppendAllText(debugPath, "Destination: " + destination + Environment.NewLine);
        }

    }
}
