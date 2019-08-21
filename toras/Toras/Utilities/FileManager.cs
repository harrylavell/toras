using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Toras.Utilities
{
    public static class FileManager
    {
        // Stores the path of the data.txt file used throughout the program
        public static string DataPath { get; } = Application.StartupPath + "/" + "data.txt";
        public static string DebugPath { get; } = Application.StartupPath + "/" + "debug.txt";
        public static string LogPath { get; } = Application.StartupPath + "/" + "log.txt";

        /* FileManager initialisation */
        public static void Init()
        {
            File.Delete(DebugPath); // Delete debug.txt
        }

        /* Writes parsed string[] data to file.
         * @param string[], directory paths & modifier settings */
        public static void Save(string[] data)
        {   
            File.WriteAllLines(DataPath, data); // Writes output array to data.txt
        }

        /* Load data file, reading all data to string[].
         * @return string[], contains directory paths and modifier settings */
        public static string[] Load()
        {
            // If file exists, load it
            if (File.Exists(DataPath))
                return File.ReadAllLines(DataPath);
            return new string[Load().Length];
        }

        /* Moves the parsed source file to the parsed destination.
         * @param source, the source to be moved
         * @param destination, where source is to be moved to */
        public static void Move(string source, string destination)
        {
            File.Move(source, destination);
        }

        /* Checks if data.txt exists within home directory
         * @return false, if data.txt found
         * @return true, if data.txt not found */
        public static bool DataExists()
        {
            if (File.Exists(DataPath))
                return false;
            return true;
        }

        /* Write parameter string to debug.txt */
        public static void WriteDebug(string output)
        {
            File.AppendAllText(DebugPath, output + Environment.NewLine);
        }

        /* Records a log of the file transfer to debug.txt */
        public static void Log(string fileName, string source, string destination)
        {
            File.AppendAllText(LogPath, DateTime.Now + Environment.NewLine);
            File.AppendAllText(LogPath, "File Name: " + fileName + Environment.NewLine);
            File.AppendAllText(LogPath, "Source: " + source + Environment.NewLine);
            File.AppendAllText(LogPath, "Destination: " + destination + Environment.NewLine);
            File.AppendAllText(LogPath, Environment.NewLine);
        }

    }
}
