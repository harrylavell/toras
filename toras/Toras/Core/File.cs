using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Toras.Data;
using Toras.Utilities;

namespace Toras.Core
{
    public class File
    {
        private string fileName; // Stores file name and extension (e.g. fileName.txt)
        private string fileExtension; // Stores file extension (e.g. .txt)
        private string fileSource; // Stores the directory path of the file
        private string fileDestination; // Stores the file transfer destination

        /* Moves parsed file to specific directory based on parsed modifier.
         * @param inputPath, path of the command line argument file
         * @param modifier, int of the modifier used when parsing */
        public File(string inputPath, int modifier)
        {
            string[] data = Loader.GetData();
            fileName = GetFileName(inputPath); // Returns file name and extension (e.g. fileName.txt)
            fileExtension = GetFileExtension(inputPath); // Retuns file extension (e.g. .txt)
            fileSource = inputPath; // Source is inputPath
            fileDestination = GetFileDestination(modifier, fileExtension); // Stores the files transfer destination
            string ftpAddress = data[8];

            // Change fileDestination depending on FTP transfer or not.
            if (modifier == 4)
                fileDestination = ftpAddress + "/" + fileName; // Retuns file destination
            else
                fileDestination = GetFileDestination(modifier, fileExtension) + "/" + fileName; // Retuns file destination

            // Adds file to queue if the file is transferable
            if (IsTransferable(modifier))
                FileTransfer.AddToQueue(this);
        }

        /* Tests whether a file is allowed to be moved based on 
        * the current modifier and modifier settings.
        * return bool, true or false */
        public static bool IsTransferable(int modifier)
        {
            string[] data = Loader.GetData();

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

            // Alt Modifier
            if (modifier == 4)
                return true;

            return false;
        }

        /* Retrieves the actual file name (fileName.torrent) of the parsed
         * file path.
         * @return fileName, the actual file name extracted from file path */
        private string GetFileName(string toGet)
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
            return fileName;
        }

        /* Retrieves a file's extension from the file path.
         * @return fileExtension, .torrent, .png, .jpg */
        private string GetFileExtension(string filePath)
        {
            string fileExtension = "";

            // Iterates through parsed string starting from the last element
            // and adds the element to fileName until a '.' is reached
            for (int i = filePath.Length - 1; i > 0; i--)
            {
                if (filePath[i] == '.')
                    break;
                fileExtension += filePath[i]; // Add element to fileName
            }

            return "." + Reverse(fileExtension);
        }

        /* Determines the transfer destination of the file based on
         * the current modifier and file extension. 
         * @param modifier, enabled key modifier 
         * @param extension, file extension to test against
         * @return destination, transfer destination for file */
        private string GetFileDestination(int modifier, string extension)
        {


            string[] data = Loader.GetData();
            string destination = "";

            


            if (modifier == 0)
                destination = data[0];

            // Shift Modifier
            if (modifier == 1 && data[4] == "1")
                destination = data[1];

            // Ctrl Modifier
            if (modifier == 2 && data[5] == "1")
                destination = data[2];

            // Alt Modifier
            if (modifier == 3 && data[6] == "1")
                destination = data[3];

            // Every user-defined modifier increments total modifiers by 1
            // Iterate through all define modifiers
                // See if theres a match
                    // if so, find what destination it's linked to, set it as current

            return destination;
        }


        /* Converts parsed string into char array and reverses it.
         * @return, reversed string */
        private string Reverse(string reverse)
        {
            char[] charArray = reverse.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

        public string GetFileName() { return fileName; }
        public string GetFileExtension() { return fileExtension; }
        public string GetFileSource() { return fileSource; }
        public string GetFileDestination() { return fileDestination; }
    }
}
