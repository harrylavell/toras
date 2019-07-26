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
        /* Save directory paths to file */
        public void Save(string[] directories)
        {
            File.WriteAllLines(Directory.GetCurrentDirectory()+"/data.txt", directories); // Writes 
        }

        /* Save directory paths from file */
        public void Load()
        {
            // If file exists, load it



            // Populate textboxes
        }



        private void SavePathsToFile(string path)
        {

        }

        private void loadPathsFromFile()
        {

        }



    }
}
