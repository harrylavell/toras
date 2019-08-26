using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

using Toras.Utilities;
using Toras.Data;

namespace Toras.Core
{

    public static class FileTransfer
    {
        public struct Ftp
        {
            public static string Address { get; set; }
            public static string Username { get; set; }
            public static string Password { get; set; }
        }

        private static List<File> fileQueue = new List<File>(); // Stores all files to be transferred
        private static int fileCounter = 1; // Records the file count
        private static UserData userData = Loader.GetUserData();

        /* Adds the parsed File to the fileQueue List */
        public static void AddToQueue(File file)
        {
            fileQueue.Add(file);
        }

        // Transfers all files within fileQueue to the appropriate file directory or FTP server
        public static void Transfer(bool standard)
        {
            if (standard)
                DirectoryTransfer(); // Transfer to directory
            else
                FtpTransfer(); // Transfer to FTP server
        }

        // Transfers files to a local directory
        private static void DirectoryTransfer()
        {
            // Iterate through fileQueue list, moving each file to destination
            foreach (File file in fileQueue)
            {
                string name = file.Name; // Stores the name of the current file
                string source = file.Source; // Stores the source directory path of the current file
                string destination = file.Destination; // Stores the desired destination of the current file
                string argument = "(" + fileCounter + ")"; // Indicates the file's posititon within queue

                try
                {
                    FileManager.Move(source, destination); // Move source file to destination
                    FileManager.Log(name, source, destination); // Log transfer record to file
                    Debug.TraceGreen($"{argument} {name} -> {destination}");
                }
                catch (System.IO.IOException)
                {
                    Debug.TraceGreen($"{argument} {name} -$ File already exists!");
                }
                fileCounter++; // Increment file counter
            }
        }

        // Transfers files to an FTP server
        private static void FtpTransfer()
        {
            Ftp.Address = userData.FtpAddress;
            Ftp.Username = userData.FtpUsername;
            Ftp.Password = userData.FtpPassword;

            foreach (File file in fileQueue)
            {
                string name = file.Name; // Stores the name of the current file
                string argument = "(" + fileCounter + ")"; // Indicates the file's posititon within queue

                // Setup FTP connection
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(Ftp.Address + "/" + name);
                request.Credentials = new NetworkCredential(Ftp.Username, Ftp.Password); // FTP Server credentials
                request.UsePassive = true;
                request.UseBinary = true;
                request.EnableSsl = true;

                // Check if file is last in fileQueue
                // Keep FTP request alive if not the last file
                if (fileCounter < fileQueue.Count)
                    request.KeepAlive = true;
                else
                    request.KeepAlive = false;

                request.Method = WebRequestMethods.Ftp.UploadFile; // File is to be uploaded to FTP server

                byte[] fileBuffer = CreateBufferArray(file); // Create buffer array

                if (!FtpFileExists(argument, name))
                {
                    try
                    {
                        Stream reqStream = request.GetRequestStream();
                        reqStream.Write(fileBuffer, 0, fileBuffer.Length);
                        Debug.TraceGreen($"{argument} {name} -> {Ftp.Address}");
                        reqStream.Close();
                    }
                    catch (WebException we)
                    {
                        Debug.Trace(we.ToString());
                    }
                }

                fileCounter++; // Increment file counter
            }


        }

        /* Sends request to FTP server to check if file exists before attempting to transfer it */
        private static bool FtpFileExists(string argument, string name)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(Ftp.Address + "/" + name);
            request.Credentials = new NetworkCredential(Ftp.Username, Ftp.Password); // FTP Server credentials
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.EnableSsl = true;

            if (fileCounter < fileQueue.Count)
                request.KeepAlive = true;
            else
                request.KeepAlive = false;

            try
            {
                request.GetResponse();
                Debug.TraceGreen($"{argument} {name} -$ File already exists!");
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }

        /* Convert file into byte array for FTP transfer
         * @return buffer, Byte[] to be transferred */
        private static byte[] CreateBufferArray(File file)
        {
            FileStream stream = System.IO.File.OpenRead(file.Source);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            return buffer;
        }


    }
}
