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
        [STAThread]
        static void Main(string[] args)
        {
            Initialise();

            // Starts application GUI if no arguments are found (e.g. no input files)
            if (args == null || args.Length == 0)
                App.Main(); // Run App.cs main method
            else
                InitTransfer(args); // Transfer files within args to their destination
        }

        /* Run initialisation code for resetting and preparing data */
        private static void Initialise()
        {
            Debug.Init(true);
            FileManager.Init();
            Loader.Init();
        }

        /* Transfers all files (paths) found within args to their designated
         * file destination based on user-defined directories and key modifiers. */
        private static void InitTransfer(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch(); // Used for recording code execution time
            watch.Start();

            Parse(args, KeyModifier.GetModifier());
            FileTransfer.Transfer(true);

            watch.Stop();
            Debug.Trace($"Total Transfer Time: {watch.ElapsedMilliseconds}ms");

            if (true)
                Debug.ShowEnd();
        }

        /* Iterates through args, creating a new File instance
         * for each element. */
        private static void Parse(string[] args, int modifier)
        {
            Debug.Trace($"Command Line Arguments: Arguments = {args.Length} Modifier = {modifier}");

            // Iterate args, creating instance of File for each
            foreach (string path in args)
                new File(path, KeyModifier.GetModifier());
        }

    }
}
