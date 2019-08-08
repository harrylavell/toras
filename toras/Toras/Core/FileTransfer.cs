using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Toras.Utilities;

namespace Toras.Core
{
    public static class FileTransfer
    {
        private static List<File> fileQueue = new List<File>(); // Stores all files to be transferred
        private static int fileCounter = 1;

        /* Adds the parsed File to the fileQueue List */
        public static void AddToQueue(File file)
        {
            fileQueue.Add(file);
        }

        // Transfers all files within fileQueue to the appropriate file directory or FTP server
        public static void Transfer(bool isFtpTransfer)
        {
            if (isFtpTransfer)
                FtpTransfer(); // Transfer to FTP server
            else
                DirectoryTransfer(); // Transfer to directory
        }

        // Transfers files to a local directory
        private static void DirectoryTransfer()
        {
            // Iterate through fileQueue list, moving each file to destination
            foreach (File file in fileQueue)
            {
                string name = file.GetFileName(); // Stores the name of the current file
                string source = file.GetFileSource(); // Stores the source directory path of the current file
                string destination = file.GetFileDestination(); // Stores the desired destination of the current file
                string argument = "(" + fileCounter + ") "; //

                try {
                    FileManager.Move(source, destination); // Move source file to destination
                    Debug.Trace(argument + name + " -> " + destination);
                } catch (System.IO.IOException)
                {
                    Debug.Trace(argument + name + " -$ " + "File already exists!");
                }
                fileCounter++; // Incrememnet argument counter
            }
        }

        // Transfers files to an FTP server
        private static void FtpTransfer()
        {

        }

        public static void Move(string source, string fileName, string destination, int modifier)
        {

            if (modifier == 4)
            {
                FTP.Transfer(source, fileName);
            }
            else
                FileManager.Move(source, destination);

        }

        public static void Log(string fileName, string source, string destination)
        {
            FileManager.Log(fileName, source, destination);
        }


    }
}
