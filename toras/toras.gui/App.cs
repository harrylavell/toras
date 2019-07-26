using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toras.utilities;
using System.Windows.Forms;
using System.Windows.Input;

namespace toras.gui
{
    public class App : System.Windows.Application
    {
        [STAThread]
        public static void Main(string[] args)
        {
            App app = new App();

            // Standard startup
            if (args == null || args.Length == 0)
            {
                app.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
                app.Run();
            }
            else
            {
                FileParser(args);
            }
        }

        private static void FileParser(string[] args)
        {
            if (KeyModifier.NoModifier()) // [0]
                MessageBox.Show("Null - " + args[0]);

            else if (KeyModifier.ShiftModifier()) // [1]
                MessageBox.Show("Shift - " + args[0]);

            else if (KeyModifier.CtrlModifier()) // [2]
                MessageBox.Show("Ctrl - "+args[0]);

            else if (KeyModifier.AltModifier()) // [3]
                MessageBox.Show("Alt - " + args[0]);

            /*
            string inputFilePath = args[0]; // Stores the path of the parsed file
            MessageBox.Show(inputFilePath);
            */
        }
    }
}
