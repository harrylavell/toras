using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using toras.utilities;

namespace toras.gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DirectoryManager dm = new DirectoryManager(); // Create instance of toras FileManager
        private FileManager fm = new FileManager();
        private string[] directories = new string[4]; // Stores all user selected directories

        public MainWindow()
        {
            InitializeComponent();
        }

        // Default directory button for choosing folder for transfer
        private void Default_directory_click(object sender, RoutedEventArgs e)
        {
            string defaultPath = dm.ChooseFileDirectory();
            directories[0] = defaultPath; // Sets index 0 of directories to default directory path
            Default_directory.Text = directories[0];
            dm.CheckDirectory(defaultPath);
            fm.Save(directories);
        }

        private void Apply_button_click(object sender, RoutedEventArgs e)
        {
            Default_directory.Text = "Apply button clicked!";
        }

        /* Exits the application without saving */
        private void Cancel_button_click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Ok_button_click(object sender, RoutedEventArgs e)
        {
            
        }


    }
}
