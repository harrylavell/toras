using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toras.Utilities;

namespace Toras.Data
{
    public static class Loader
    {
        private static string[] data = new string[8];

        public static void Init()
        {
            // Setup default data for data.txt if user's first session
            // otherwise, load data from file.
            if (FirstSession())
                CreateData();
            else
                LoadData();
        }

        public static string[] GetData()
        {
            return data;
        }

        /* Checks whether data.txt exists. 
         * @return true, if data.txt not found
         * @return false, if data.txt found */
        private static bool FirstSession()
        {
            return FileManager.DataExists();
        }

        /* Create default data */
        private static void CreateData()
        {
            data[0] = "No Directory"; // Default Directory
            data[1] = "No Directory"; // Shift Directory
            data[2] = "No Directory"; // Ctrl Directory
            data[3] = "No Directory"; // Alt Directory
            data[4] = "0"; // Shift Checkbox
            data[5] = "0"; // Ctrl Checkbox
            data[6] = "0"; // Alt Checkbox
            data[7] = "100"; // Loading Time
            FileManager.Save(data); // Writes new data to file

            Debug.Trace("User's First Session: Creating data file");
        }

        private static void LoadData()
        {
            Debug.Trace("Loading Data...");

            data = new string[8]; // Allocate correct size for data
            data = FileManager.Load();

            Debug.Trace("Success");
        }

    }
}
