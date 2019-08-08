using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Toras.Core;

/* Debug used be debugging various aspects of the application and 
 * displays console window if enabled and outputs traces to debug.txt */
namespace Toras.Utilities
{
    public static class Debug
    {
        [DllImport("Kernel32")]
        private static extern void AllocConsole();
        [DllImport("Kernel32")]
        private static extern void FreeConsole();

        public static void Init(bool showConsole)
        {
            if (showConsole)
                AllocConsole();

            Trace("Debug Initialised");
            Trace("=================");
        }

        /* Calls trace, parsing new line */
        public static void Trace()
        {
            Trace("");
        }

        /* Prints and writes parsed string on new line to console and debug.txt respectively */
        public static void Trace(string input)
        {
            Console.WriteLine(input); // Writes to Console
            FileManager.WriteDebug(input); // Writes to debug.txt
        }

        /* Prints and writes parsed string on new line to console and debug.txt respectively */
        public static void Trace(string input, ConsoleColor color)
        {
            Console.ForegroundColor = color; // Change text color to green
            Console.WriteLine(input); // Writes to Console
            FileManager.WriteDebug(input); // Writes to debug.txt
            Console.ForegroundColor = ConsoleColor.White; // Change text color to white
        }

        /* Prints and writes parsed string on new line to console and debug.txt respectively */
        public static void TraceRed(string input)
        {
            Console.ForegroundColor = ConsoleColor.Red; // Change text color to green
            Console.WriteLine(input); // Writes to Console
            FileManager.WriteDebug(input); // Writes to debug.txt
            Console.ForegroundColor = ConsoleColor.White; // Change text color to white
        }

        /* Prints and writes parsed string on new line to console and debug.txt respectively */
        public static void TraceGreen(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green; // Change text color to green
            Console.WriteLine(input); // Writes to Console
            FileManager.WriteDebug(input); // Writes to debug.txt
            Console.ForegroundColor = ConsoleColor.White; // Change text color to white
        }

        public static void ShowEnd()
        {
            Trace("=================");
            Trace("Debug Terminated");
            Console.ReadLine();
        }

    }
}
