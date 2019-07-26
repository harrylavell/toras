using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace toras.utilities
{
    public class FileManager
    {

        public string ChooseFileDirectory()
        {
            string path = ""; // Empty string file directory path
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                path = fbd.SelectedPath;
            }

            return path;
        }

        /* Searches specified directory inputing files into array.
         * @param directory, path of directory to be searched
         * @return files, string array containing files */
        public void CheckDirectory(string directory)
        {
            string[] files = Directory.GetFiles(directory); // Populates array with files contained within directory
            string display = "";

            // Iterate array adding elements to display string
            foreach (string s in files) {
                display += (s + "\n");
            }

            MessageBox.Show(display);

            //return files;
        }

        /* Searches specified directory for the parsed file.
         * @param directory, path of directory to search in
         * @param filepath, path of file to search for
         * @return true, if file found */
        public bool CheckDirectoryForFile(String directory, String filepath)
        {
            //string[] files = CheckDirectory(directory); // Loads array with all files from directory

            return true;
        }

    }
}
