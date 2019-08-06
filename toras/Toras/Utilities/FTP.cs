using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Toras.Data;
using System.IO;

namespace Toras.Utilities
{
    public static class FTP
    {

        public static void Init()
        {
            // prefix = ftp://

            //string ftpAddress = 



            // Get ftp addresses
            // Get ftp credentials

            // load into array
            // 0 = address
            // 1 = username
            // 2 = password
        }

        public static void Transfer(string filePath, string fileName)
        {
            string[] data = Loader.GetData();
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(data[8] + "/" + fileName);

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(data[9], data[10]);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            try {
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(CreateByteArray(filePath), 0, CreateByteArray(filePath).Length);
                reqStream.Close();
            } catch (Exception e)
            {
                Debug.Trace(e.ToString());
            }

        }

        public static void TestConnection()
        {
            // Check if parsed address is valid

            // Check if connection can be made
        }

        private static byte[] CreateByteArray(string path)
        {
            FileStream stream = File.OpenRead(path);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            return buffer;
        }
    }
}
