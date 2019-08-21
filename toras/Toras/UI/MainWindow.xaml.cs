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
                DefaultDirectory.Text = data[0];
            if (!data[1].Equals("No Directory")) // Shift
                ShiftDirectory.Text = data[1];
            if (!data[2].Equals("No Directory")) // Ctrl
                CtrlDirectory.Text = data[2];


            // Correct checkboxes
            if (data[4].Equals("1"))
                ShiftCheckbox.IsChecked = true;
            else
                ShiftCheckbox.IsChecked = false;

            if (data[5].Equals("1"))
                CtrlCheckbox.IsChecked = true;
            else
                CtrlCheckbox.IsChecked = false;

            FtpAddress.Text = data[8];
            FtpUsername.Text = data[9];
            FtpPassword.Password = data[10];

            // Deactive Apply Button
            ApplyButton.IsEnabled = false;
        }

        /* Open folder dialog box and adds user selected directory to data array */
        private void DefaultDirectory_click(object sender, RoutedEventArgs e)
        {
            string defaultPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            data[0] = defaultPath; // Sets index 0 of data to default directory path
            DefaultDirectory.Text = data[0]; // Sets text box to path

            if (DefaultDirectory.Text != "")
                ResetApplyState();
        }

        private void ShiftDirectory_click(object sender, RoutedEventArgs e)
        {
            string shiftPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            data[1] = shiftPath; // Sets index 1 of data to shift directory path
            ShiftDirectory.Text = data[1]; // Sets text box to path

            if (ShiftDirectory.Text != "")
                ResetApplyState();
        }

        private void CtrlDirectory_click(object sender, RoutedEventArgs e)
        {
            string ctrlPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            data[2] = ctrlPath; // Sets index 2 of data to ctrl directory path
            CtrlDirectory.Text = data[2]; // Sets text box to path

            if (CtrlDirectory.Text != "")
                ResetApplyState();
        }

        private void FtpConfirm_click(object sender, RoutedEventArgs e)
        {
            data[8] = FtpAddress.Text;
            data[9] = FtpUsername.Text;
            data[10] = FtpPassword.Password;
        }

        private void ShiftCheckbox_changed(object sender, RoutedEventArgs e)
        {
            if (ShiftCheckbox.IsChecked == true)
                data[4] = "1"; // True
            else
                data[4] = "0"; // False
            ResetApplyState();
        }

        private void CtrlCheckbox_changed(object sender, RoutedEventArgs e)
        {
            if (CtrlCheckbox.IsChecked == true)
                data[5] = "1"; // True
            else
                data[5] = "0"; // False
            ResetApplyState();
        }

        private void FtpCheckbox_changed(object sender, RoutedEventArgs e)
        {
            if (FtpCheckbox.IsChecked == true)
                data[6] = "1"; // True
            else
                data[5] = "0"; // False
            ResetApplyState();
        }

        /* Saves directory paths to file */
        private void ApplyButton_click(object sender, RoutedEventArgs e)
        {
            FileManager.Save(data); // Saves directory paths to file
            ConfirmChanges();
            ApplyButton.IsEnabled = false;
        }

        /* Exits the application without saving */
        private void CancelButton_click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        /* Exits the application after saving */
        private void OkButton_click(object sender, RoutedEventArgs e)
        {
            FileManager.Save(data); // Saves directory paths to file
            ConfirmChanges();
            App.Current.Shutdown();
        }
        
        /* Resets the visibility of the 'Apply' button from visible to shaded. */
        private void ResetApplyState()
        {
            ApplyButton.IsEnabled = true;
        }

        private void ConfirmChanges()
        {
            Console.Clear();
            Debug.Trace("Debug Initialised");
            Debug.Trace("=================");

            Debug.Trace("Destinations:");
            Debug.Trace("Default Directory -> "+data[0]);
            Debug.Trace("Shift Directory -> " + data[1]);
            Debug.Trace("Ctrl Directory -> " + data[2]);
            Debug.Trace("FTP Address -> " + data[2]);

            Debug.Trace("\nModifiers:");
            if (data[4] == "1")
                Debug.Trace("Shift Modifier -> Enabled");
            else
                Debug.Trace("Shift Modifier -> Disabled");

            if (data[5] == "1")
                Debug.Trace("Ctrl Modifier -> Enabled");
            else
                Debug.Trace("Ctrl Modifier -> Disabled");

            if (data[6] == "1")
                Debug.Trace("FTP Modifier -> Enabled");
            else
                Debug.Trace("FTP Modifier -> Disabled");

            Debug.Trace();
        }
    }
}
