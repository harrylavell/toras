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

            // Initialise & load directory paths into text boxes
            //directories = fm.Load(); // Loads directory paths from file into directories
            LoadDirectoryPaths(); // Loads directory paths into corresponding text boxes
        }

        /* Open folder dialog box and adds user selected directory to directories array */
        private void Default_directory_click(object sender, RoutedEventArgs e)
        {
            string defaultPath = dm.ChooseFileDirectory(); // Retuns path of chosen directory
            directories[0] = defaultPath; // Sets index 0 of directories to default directory path
            Default_directory.Text = directories[0]; // Sets text box to path
            dm.CheckDirectory(defaultPath); // Shows message box of files within directory   
        }

        /* Saves directory paths to file */
        private void Apply_button_click(object sender, RoutedEventArgs e)
        {
            fm.Save(directories); // Saves directory paths to file
        }

        /* Exits the application without saving */
        private void Cancel_button_click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        /* Exits the application after saving */
        private void Ok_button_click(object sender, RoutedEventArgs e)
        {
            fm.Save(directories); // Saves directory paths to file
            App.Current.Shutdown();
        }

        /* Populates text boxes with the corresponding directory path */
        private void LoadDirectoryPaths()
        {
            Default_directory.Text = directories[0];
        }
    }
}
