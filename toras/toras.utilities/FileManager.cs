using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace toras.utilities
{
    public static class FileManager
    {
        // Stores the path of the data.txt file used throughout the program
        private static readonly string dataPath = Application.StartupPath + "/" + "data.txt";
        private static readonly string debugPath = Application.StartupPath + "/" + "debug.txt";
        private static int loadingTime = 100;

        /* Convert parsed string to int and assigned it to loadingTime */
        public static void SetLoadingTime(string lt)
        {
            loadingTime = Int32.Parse(lt);
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

        /* Checks if data.txt exists within home directory
         * @return false, if data.txt found
         * @return true, if data.txt not found */
        public static bool IsFirstSession()
        {
            if (File.Exists(dataPath))
                return false;
            return true;
        }

        /* Loads data file into array to test directory paths against 
         * parsed args, and moves the file if so. */
        public static void Parser(string[] args, int modifier)
        {
            // Load Data File into string array
            string[] data = Load();

            // Tests whether modifier is active
            if (CanMove(modifier))
            {
                string destination = data[modifier]; // Set position to the corresponding path
                string fileName = "";

                /* Iterates each string within args array moving
                 * each to their required destination */
                foreach (string s in args)
                {
                    fileName = GetFileName(s);
                    File.AppendAllText(debugPath, DateTime.Now + Environment.NewLine);
                    File.AppendAllText(debugPath, "File Name: " + fileName + Environment.NewLine);
                    File.AppendAllText(debugPath, "Source: " + s + Environment.NewLine);
                    File.AppendAllText(debugPath, "Destination: " +destination + Environment.NewLine);
                    File.Move(s, destination+=fileName); // Moves file to destination with correct file name
                    Loaded(destination+fileName);
                }
            }
        }

        /* Searches parsed directory for the parsed file paths.
         * @return true, if the file has successfully been loaded by torrent client
         * @return false, if file still exists within directory after 5 seconds*/
        private static void Loaded(string file)
        {
            System.Threading.Thread.Sleep(loadingTime); // X amount of seconds
            File.AppendAllText(debugPath, "Loaded? ");

            if (File.Exists(file))
                File.AppendAllText(debugPath, "Torrent Failed to Load"  + Environment.NewLine);
            else
                File.AppendAllText(debugPath, "Torrent Loaded Successfully" + Environment.NewLine);
            File.AppendAllText(debugPath, Environment.NewLine);
        }

        /* Tests whether a file is allowed to be moved based on 
         * the current modifier and modifier settings.
         * return bool, true or false */
        private static bool CanMove(int modifier)
        {
            string[] data = Load();

            if (modifier == 0)
                return true;

            // Shift Modifier
            if (modifier == 1 && data[4] == "1")
                return true;

            // Ctrl Modifier
            if (modifier == 2 && data[5] == "1")
                return true;

            // Alt Modifier
            if (modifier == 3 && data[6] == "1")
                return true;

            return false;
        }

        /* Retrieves the actual file name (file name.torrent) of the parsed
         * file path.
         * @return fileName, the actual file name extracted from file path */
        private static string GetFileName(string toGet)
        {
            string fileName = "";

            // Iterates through parsed string starting from the last element
            // and adds the element to fileName until '\' or '/' is reached
            for (int i = toGet.Length - 1; i > 0; i--)
            {
                if (toGet[i] == '/' || toGet[i] == '\\')
                    break;
                fileName += toGet[i]; // Add element to fileName
            }

            fileName = Reverse(fileName); // Backwards string is corrected
            return "/"+fileName;
        }

        /* Converts parsed string into char array and reverses it.
         * @return, reversed string */
        private static string Reverse(string reverse)
        {
            char[] charArray = reverse.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

    }
}
