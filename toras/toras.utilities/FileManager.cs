using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace toras.utilities
{
    public static class FileManager
    {
        private static readonly string dataPath = Directory.GetCurrentDirectory() + "/data.txt";

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

        public static void Parser(string[] args)
        {

            /*
            if (KeyModifier.NoModifier()) // [0]
            {
                foreach (string path in args)
                {
                    FileManager.Move(path, )
                }
            }
            */
        }

        private static void Move(string source, string destination)
        {

        }

    }
}
