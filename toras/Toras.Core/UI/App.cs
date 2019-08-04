using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Toras.Core;
using Toras.UI;

namespace Toras.UI
{
    public class App : System.Windows.Application
    {
        [STAThread]
        public static void Main ()
        {
            App app = new App();
            app.StartupUri = new System.Uri("UI/MainWindow.xaml", System.UriKind.Relative);
            app.Run();
        }
    }
}
