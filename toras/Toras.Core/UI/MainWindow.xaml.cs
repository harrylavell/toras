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
using Toras.Core;
using Toras.Data;
using Toras.Utilities;

namespace Toras.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string title = "Toras";
        private string version = " " + "1.0.0";
        private string[] data; // 0-3 (data) & 4-6 (Checkbox Flags)

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            base.Title = title + version;
            base.WindowStartupLocation = WindowStartupLocation.CenterScreen; // Center Window

            data = Loader.GetData();
            LoadData();
        }

        private void LoadData()
        {

            // Populate textboxes
            if (!data[0].Equals("No Directory")) // Default
                Default_directory.Text = data[0];
            if (!data[1].Equals("No Directory")) // Shift
                Shift_directory.Text = data[1];
            if (!data[2].Equals("No Directory")) // Ctrl
                Ctrl_directory.Text = data[2];


            // Correct checkboxes
            if (data[4].Equals("1"))
                Shift_check.IsChecked = true;
            else
                Shift_check.IsChecked = false;

            if (data[5].Equals("1"))
                Ctrl_check.IsChecked = true;
            else
                Ctrl_check.IsChecked = false;


            // Deactive Apply Button
            Apply_button.IsEnabled = false;
        }

        /* Open folder dialog box and adds user selected directory to data array */
        private void Default_directory_click(object sender, RoutedEventArgs e)
        {
            string defaultPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            data[0] = defaultPath; // Sets index 0 of data to default directory path
            Default_directory.Text = data[0]; // Sets text box to path

            if (Default_directory.Text != "")
                Apply_button.IsEnabled = true;
        }

        private void Shift_directory_click(object sender, RoutedEventArgs e)
        {
            string shiftPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            data[1] = shiftPath; // Sets index 1 of data to shift directory path
            Shift_directory.Text = data[1]; // Sets text box to path

            if (Shift_directory.Text != "")
                Apply_button.IsEnabled = true;
        }

        private void Ctrl_directory_click(object sender, RoutedEventArgs e)
        {
            string ctrlPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            data[2] = ctrlPath; // Sets index 2 of data to ctrl directory path
            Ctrl_directory.Text = data[2]; // Sets text box to path

            if (Ctrl_directory.Text != "")
                Apply_button.IsEnabled = true;
        }

        private void Shift_checkbox_changed(object sender, RoutedEventArgs e)
        {
            if (Shift_check.IsChecked == true)
                data[4] = "1"; // True
            else
                data[4] = "0"; // False
            Apply_button.IsEnabled = true;
        }

        private void Ctrl_checkbox_changed(object sender, RoutedEventArgs e)
        {
            if (Ctrl_check.IsChecked == true)
                data[5] = "1"; // True
            else
                data[5] = "0"; // False
            Apply_button.IsEnabled = true;
        }

        /* Saves directory paths to file */
        private void Apply_button_click(object sender, RoutedEventArgs e)
        {
            FileManager.Save(data); // Saves directory paths to file
            ConfirmChanges();
            Apply_button.IsEnabled = false;
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
            ConfirmChanges();
            App.Current.Shutdown();
        }

        private void ConfirmChanges()
        {
            Console.Clear();
            Debug.Trace("Debug Initialised");
            Debug.Trace("=================");

            Debug.Trace("Directory:");
            Debug.Trace("Default Directory -> "+data[0]);
            Debug.Trace("Shift Directory -> " + data[1]);
            Debug.Trace("Ctrl Directory -> " + data[2]);

            Debug.Trace("\nModifiers:");
            if (data[4] == "1")
                Debug.Trace("Shift Modifier -> Enabled");
            else
                Debug.Trace("Shift Modifier -> Disabled");

            if (data[5] == "1")
                Debug.Trace("Ctrl Modifier -> Enabled");
            else
                Debug.Trace("Ctrl Modifier -> Disabled");

            Debug.Trace();
        }
    }
}
