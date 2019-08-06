using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Toras.Data;
using Toras.Utilities;

namespace Toras.Core
{
    class Parser
    {
        private static int argCount = 1; // Records the number of arguments in args

        /* Moves parsed file to specific directory based on parsed modifier.
         * @param inputPath, path of the command line argument file
         * @param modifier, int of the modifier used when parsing */
        public Parser(string inputPath, int modifier)
        {
            string[] data = Loader.GetData();
            string fileName = GetFileName(inputPath); // Returns file name and extension (e.g. fileName.txt)
            string fileExtension = GetFileExtension(inputPath); // Retuns file extension (e.g. .txt)
            string fileSource = inputPath; // Source is inputPath
            string argument = "(" + argCount + ") ";
            string fileDestination = "";
            string ftpAddress = data[8];

            // Change fileDestination depending on FTP transfer or not.
            if (modifier == 4)
                fileDestination = ftpAddress + "/" + fileName; // Retuns file destination
            else
                fileDestination = GetFileDestination(modifier, fileExtension)+"/"+fileName; // Retuns file destination

            if (CanMove(modifier))
            {

                try
                {
                    FileTransfer.Move(fileSource, fileName, fileDestination, modifier);
                    FileTransfer.Log(fileName, fileSource, fileDestination);
                    Debug.Trace(argument + fileName + " -> " + fileDestination);
                }
                catch (IOException)
                {
                    Debug.Trace(argument + fileName + " -$ " + "File already exists!");
                }

            }

            argCount++;
        }

        /* Tests whether a file is allowed to be moved based on 
        * the current modifier and modifier settings.
        * return bool, true or false */
        private static bool CanMove(int modifier)
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

        /* Retrieves the actual file name (file name.torrent) of the parsed
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

        private string GetFileExtension(string file)
        {
            string fileExtension = "";

            // Iterates through parsed string starting from the last element
            // and adds the element to fileName until a '.' is reached
            for (int i = file.Length - 1; i > 0; i--)
            {
                if (file[i] == '.')
                    break;
                fileExtension += file[i]; // Add element to fileName
            }

            fileExtension = Reverse(fileExtension); // Backwards string is corrected
            return "." + fileExtension;
        }

        private string GetFileDestination(int modifier, string extension)
        {
            string[] data = Loader.GetData();

            if (modifier == 0)
                return data[0];

            // Shift Modifier
            if (modifier == 1 && data[4] == "1")
                return data[1];

            // Ctrl Modifier
            if (modifier == 2 && data[5] == "1")
                return data[2];

            // Alt Modifier
            if (modifier == 3 && data[6] == "1")
                return data[3];

            return "";
        }

        /* Converts parsed string into char array and reverses it.
         * @return, reversed string */
        private string Reverse(string reverse)
        {
            char[] charArray = reverse.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

    }
}
