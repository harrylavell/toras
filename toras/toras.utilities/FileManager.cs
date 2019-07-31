using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace toras.utilities
{
    public static class FileManager
    {
        private static readonly string dataPath = Directory.GetCurrentDirectory() + "/" + "data.txt";

        /* Save directory paths to file */
        public static void Save(string[] data)
        {   
            // Writes directory paths & checkbox status to data.txt
            File.WriteAllLines(dataPath, data); // Writes output array to data.txt
        }

        /* Load directory paths from file */
        public static string[] Load()
        {
            // If file exists, load it
            if (File.Exists(dataPath))
                return File.ReadAllLines(dataPath);
            return null;
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

        public static void Parser(string[] args, int modifier)
        {
            // Load Data File into string array
            string[] data = Load();

            // Tests whether modifier is active
            if (CanMove(modifier))
            {
                string destination = data[modifier]; // Set position to the corresponding path

                foreach (string s in args)
                {
                    File.Move(s, destination+GetFileName(s));
                }
            }
        }

        /* Tests whether a modifier is allowed to be run */
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

        private static string GetFileName(string toGet)
        {
            string fileName = "";

            for (int i = toGet.Length - 1; i > 0; i--)
            {
                if (toGet[i] == '/' || toGet[i] == '\\')
                    break;

                fileName += toGet[i];
            }

            fileName = Reverse(fileName);

            Trace.WriteLine(fileName);

            return "/"+fileName;
        }

        private static string Reverse(string reverse)
        {
            char[] charArray = reverse.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

    }
}
