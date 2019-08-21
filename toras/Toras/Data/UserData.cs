using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toras.Data
{
    [Serializable]
    public class UserData
    {
        public string DefaultDirectory { get; set; }
        public string ShiftDirectory { get; set; }
        public string CtrlDirectory { get; set; }
        public bool ShiftCheckbox { get; set; }
        public bool CtrlCheckbox { get; set; }
        public bool FtpCheckbox { get; set; }
        public string FtpAddress { get; set; }
        public string FtpUsername { get; set; }
        public string FtpPassword { get; set; }

        public void SetDefaultData()
        {
            DefaultDirectory = ""; // Default Directory
            ShiftDirectory = ""; // Shift Directory
            CtrlDirectory = ""; // Ctrl Directory
            ShiftCheckbox = false; // Shift Checkbox
            CtrlCheckbox = false; // Ctrl Checkbox
            FtpCheckbox = false; // FTP Checkbox
            FtpAddress = ""; // FTP Address
            FtpUsername = ""; // FTP Username
            FtpPassword = ""; // FTP Password
        }

    }
}
