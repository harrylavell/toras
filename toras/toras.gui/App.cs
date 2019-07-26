using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toras.gui
{
    public class App : System.Windows.Application
    {
        [STAThread]
        public static void Main()
        {
            App app = new App();
            app.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
            app.Run();
        }
    }
}
