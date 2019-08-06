using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toras.Utilities
{
    public static class FileTransfer
    {
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
