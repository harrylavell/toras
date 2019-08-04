using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Toras.Utilities
{
    public static class DirectoryManager
    {
        /* Opens folder dialog window allowing user to designate the
         * directiory to be used as the default directory for file transfers.
         * @return directory path if it passes suitability check */
        public static string ChooseFileDirectory()
        {
            var fbd = new FolderBrowserDialog(); // Creates instance of folder browser
            DialogResult result = fbd.ShowDialog();

            // Suitability check on designated path
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) {
                return fbd.SelectedPath;
            }

            return "";
        }

        /* Searches specified directory inputing files into array.
         * @param directory, path of directory to be searched
         * @return files, string array containing files inside directory */
        private static string[] LoadDirectoryFiles(string directory)
        {
            string[] files = Directory.GetFiles(directory); // Populates array with files contained within directory
            string display = "";

            // Iterate array adding elements to display string
            foreach (string s in files) {
                display += (s + "\n");
            }

            //MessageBox.Show(display);

            return files;
        }

        /* Searches specified directory for the parsed file.
         * @param directory, path of directory to search in
         * @param filepath, path of file to search for
         * @return true, if file found */
        public static bool FileExists(string directory, string filePath)
        {
            string[] files = LoadDirectoryFiles(directory); // Loads array with all files within parsed directory

            MessageBox.Show("FileExists() Loaded");

            // Iterate through directory files
            foreach (string s in files)
            {
                MessageBox.Show(s+" ==== "+filePath);

                if (string.Equals(s, filePath)) // Equal
                    return true;
            }
            return false;
        }

    }
}
