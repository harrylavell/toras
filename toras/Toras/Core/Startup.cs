using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toras.UI;
using Toras.Data;
using Toras.Utilities;

namespace Toras.Core
{
    class Startup
    {
        private static bool debug = true;

        [STAThread]
        static void Main(string[] args)
        {
            Init();

            // Starts application UI if no arguments are found (e.g. no input files)
            if (args == null || args.Length == 0)
                App.Main(); // Run App.cs main method

            // Calls Parse for file transfer to determined location
            else
            {
                var watch = new System.Diagnostics.Stopwatch(); // Used for recording code execution time
                watch.Start();

                Parse(args, KeyModifier.GetModifier());

                watch.Stop();
                Debug.Trace($"Total Transfer Time: {watch.ElapsedMilliseconds} ms");
            }

            if (debug)
                Debug.ShowEnd();
        }

        /* Iterates through args, creating a new Parser instance
         * for each element. */
        private static void Parse(string[] args, int modifier)
        {
            Debug.Trace("Command Line Arguments: " + "Arguments = " + args.Length + " Modifier = " + modifier);

            // Iterate args, creating instance of Parser for each
            foreach (string path in args)
            {
                new Parser(path, modifier);
            }
        }

        private static void Init()
        {
            Debug.Init(debug);
            FileManager.Init();
            Loader.Init();
        }

    }
}
