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
        string title = "Toras";
        string version = " " + "1.0.0";
        string[] data = new string[8]; // 0-3 (data) & 4-6 (Checkbox Flags)

        public MainWindow()
        {
            InitializeComponent();

            this.Title = title + version;
            
            Startup();
        }

        public void Startup()
        {
            // Resume session if not first session
            if (!FileManager.IsFirstSession())
            {
                data = FileManager.Load(); // Loads data from file into array
                ResumeSession();
            }
            else {
                CreateSession();
            }
        }

        private void CreateSession()
        {
            data[0] = "No Directory"; // Default Directory
            data[1] = "No Directory"; // Shift Directory
            data[2] = "No Directory"; // Ctrl Directory
            data[3] = "No Directory"; // Alt Directory
            data[4] = "0"; // Shift Checkbox
            data[5] = "0"; // Ctrl Checkbox
            data[6] = "0"; // Alt Checkbox
            data[7] = "100"; // Loading Time

            ResumeSession();
        }

        private void ResumeSession()
        {
            // Populate textboxes
            if (!data[0].Equals("No Directory")) // Default
                Default_directory.Text = data[0];
            if (!data[1].Equals("No Directory")) // Shift
                Shift_directory.Text = data[1];
            if (!data[2].Equals("No Directory")) // Ctrl
                Ctrl_directory.Text = data[2];
            if (!data[3].Equals("No Directory")) // Alt
                Alt_directory.Text = data[3];

            // Correct checkboxes
            if (data[4].Equals("1"))
                Shift_check.IsChecked = true;
            else
                Shift_check.IsChecked = false;

            if (data[5].Equals("1"))
                Ctrl_check.IsChecked = true;
            else
                Ctrl_check.IsChecked = false;

            if (data[6].Equals("1"))
                Alt_check.IsChecked = true;
            else
                Alt_check.IsChecked = false;

            // Loading Time
            FileManager.SetLoadingTime(data[7]);
            Loading_time_allowed.Text = data[7];
        }

        /* Open folder dialog box and adds user selected directory to data array */
        private void Default_directory_click(object sender, RoutedEventArgs e)
        {
            string defaultPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            data[0] = defaultPath; // Sets index 0 of data to default directory path
            Default_directory.Text = data[0]; // Sets text box to path
        }

        private void Shift_directory_click(object sender, RoutedEventArgs e)
        {
            string shiftPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            data[1] = shiftPath; // Sets index 1 of data to shift directory path
            Shift_directory.Text = data[1]; // Sets text box to path
        }

        private void Ctrl_directory_click(object sender, RoutedEventArgs e)
        {
            string ctrlPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            data[2] = ctrlPath; // Sets index 2 of data to ctrl directory path
            Ctrl_directory.Text = data[2]; // Sets text box to path
        }

        private void Alt_directory_click(object sender, RoutedEventArgs e)
        {
            string altPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            data[3] = altPath; // Sets index 3 of data to default directory path
            Alt_directory.Text = data[3]; // Sets text box to path
        }

        private void Shift_checkbox_changed(object sender, RoutedEventArgs e)
        {
            if (Shift_check.IsChecked == true)
                data[4] = "1"; // True
            else
                data[4] = "0"; // False
        }

        private void Ctrl_checkbox_changed(object sender, RoutedEventArgs e)
        {
            if (Ctrl_check.IsChecked == true)
                data[5] = "1"; // True
            else
                data[5] = "0"; // False
        }

        private void Alt_checkbox_changed(object sender, RoutedEventArgs e)
        {
            if (Alt_check.IsChecked == true)
                data[6] = "1"; // True
            else
                data[6] = "0"; // False
        }

        private void LoadingTimeChanged(object sender, TextChangedEventArgs e)
        {
            data[7] = Loading_time_allowed.Text;
        }

        /* Saves directory paths to file */
        private void Apply_button_click(object sender, RoutedEventArgs e)
        {
            FileManager.Save(data); // Saves directory paths to file
        }

        /* Exits the application without saving */
        private void Cancel_button_click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        /* Exits the application after saving */
        private void Ok_button_click(object sender, RoutedEventArgs e)
        {
            FileManager.Save(data); // Saves directory paths to file
            App.Current.Shutdown();
        }
    }
}
