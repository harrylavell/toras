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
        public string Name { get; }
        public string Extension { get; }
        public string Source { get; }
        public string Destination { get; }

        private static int modifier;
        private static UserData userData = Loader.GetUserData();

        /* Moves parsed file to specific directory based on parsed modifier.
         * @param inputPath, path of the command line argument file
         * @param modifier, int of the modifier used when parsing */
        public File(string inputPath, int mod)
        {
            Source = inputPath; // Source is inputPath
            Name = GetFileName(); // Returns file name and extension (e.g. fileName.txt)
            Extension = GetFileExtension(); // Retuns file extension (e.g. .txt)
            Destination = GetFileDestination(); // Stores the file transfer destination
            modifier = mod;

            // Adds file to queue if the file is transferable
            if (IsTransferable())
            {
                FileTransfer.AddToQueue(this);
            }
        }

        /* Tests whether a file is allowed to be moved based on 
        * the current modifier and modifier settings.
        * return bool, true or false */
        private static bool IsTransferable()
        {
            if (modifier == 0)
                return true;

            // Shift Modifier
            if (modifier == 1 && userData.ShiftCheckbox == true)
                return true;

            // Ctrl Modifier
            if (modifier == 2 && userData.CtrlCheckbox == true)
                return true;

            // Shift+Ctrl Modifier
            if (modifier == 4 && userData.FtpCheckbox == true)
                return true;

            return false;
        }

        /* Retrieves the actual file name (fileName.torrent) of the parsed
         * file path.
         * @return fileName, the actual file name extracted from file path */
        private string GetFileName()
        {
            string fileName = "";

            // Iterates through parsed string starting from the last element
            // and adds the element to fileName until '\' or '/' is reached
            for (int i = Source.Length - 1; i > 0; i--)
            {
                if (Source[i] == '/' || Source[i] == '\\')
                    break;
                fileName += Source[i]; // Add element to fileName
            }

            fileName = Reverse(fileName); // Backwards string is corrected
            return fileName;
        }

        /* Retrieves a file's extension from the file path.
         * @return fileExtension, .torrent, .png, .jpg */
        private string GetFileExtension()
        {
            string fileExtension = "";

            // Iterates through parsed string starting from the last element
            // and adds the element to fileName until a '.' is reached
            for (int i = Source.Length - 1; i > 0; i--)
            {
                if (Source[i] == '.')
                    break;
                fileExtension += Source[i]; // Add element to fileName
            }

            return $".{Reverse(fileExtension)}";
        }

        /* Determines the transfer destination of the file based on
         * the current modifier and file extension. 
         * @param modifier, enabled key modifier 
         * @param extension, file extension to test against
         * @return destination, transfer destination for file */
        private string GetFileDestination()
        {
            string destination = "";

            // Modifier is local directory modifier
            if (modifier < 4)
            {
                if (modifier == 0)
                    destination = userData.DefaultDirectory;

                // Shift Modifier
                else if (modifier == 1 && userData.ShiftCheckbox == true)
                    destination = userData.ShiftDirectory;

                // Ctrl Modifier
                else if (modifier == 2 && userData.CtrlCheckbox == true)
                    destination = userData.CtrlDirectory;

                destination = $"{destination}/{Name}";
            } else
            {
                destination = $"{FileTransfer.Ftp.Address}/{Name}"; // Retuns file destination
            }

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

    }
}
