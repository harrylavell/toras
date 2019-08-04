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
            Trace("\n");
        }

        /* Prints and writes parsed string on new line to console and debug.txt respectively */
        public static void Trace(string input)
        {
            Console.WriteLine(input); // Writes to Console
            FileManager.WriteDebug(input); // Writes to debug.txt
        }

        public static void ShowEnd()
        {
            Trace("=================");
            Trace("Debug Terminated");
            Console.ReadLine();
        }

    }
}
