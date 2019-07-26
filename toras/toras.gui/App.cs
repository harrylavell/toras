using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using toras.utilities;
using System.Windows.Forms;

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
                string inputFilePath = args[0]; // Stores the path of the parsed file
                MessageBox.Show(inputFilePath);
            }
            
        }
     }
}
