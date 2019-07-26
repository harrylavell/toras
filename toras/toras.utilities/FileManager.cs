using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace toras.utilities
{
    public class FileManager
    {
        private readonly string dataFilePath = Directory.GetCurrentDirectory() + "/data.txt";

        /* Save directory paths to file */
        public void Save(string[] directories)
        {
            File.WriteAllLines(dataFilePath, directories); // Writes directory paths to data file
        }

        /* Load directory paths from file */
        public string[] Load()
        {
            // If file exists, load it
            if (File.Exists(dataFilePath)) {
                return File.ReadAllLines(dataFilePath);
            }

            return null; // Failed to read file
        }
    }
}
