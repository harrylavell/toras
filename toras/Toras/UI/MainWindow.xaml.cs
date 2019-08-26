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
        private static UserData userData = Loader.GetUserData();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            base.Title = title + version;
            base.WindowStartupLocation = WindowStartupLocation.CenterScreen; // Center Window

            LoadData();
        }

        private void LoadData()
        {
            // Populate Textboxes
            DefaultDirectory.Text = userData.DefaultDirectory;
            ShiftDirectory.Text = userData.ShiftDirectory;
            CtrlDirectory.Text = userData.CtrlDirectory;

            // Correct Checkboxes
            ShiftCheckbox.IsChecked = userData.ShiftCheckbox;
            CtrlCheckbox.IsChecked = userData.CtrlCheckbox;
            FtpCheckbox.IsChecked = userData.FtpCheckbox;

            // Load Ftp information
            FtpAddress.Text = userData.FtpAddress;
            FtpUsername.Text = userData.FtpUsername;
            FtpPassword.Password = userData.FtpPassword;

            // Deactive Apply Button
            ApplyButton.IsEnabled = false;
        }

        /* Open folder dialog box and adds user selected directory to data array */
        private void DefaultDirectory_click(object sender, RoutedEventArgs e)
        {
            string defaultPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            DefaultDirectory.Text = userData.DefaultDirectory = defaultPath; // Sets user data and textbox to defaultPath

            if (DefaultDirectory.Text != "")
                ResetApplyState();
        }

        private void ShiftDirectory_click(object sender, RoutedEventArgs e)
        {
            string shiftPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            ShiftDirectory.Text = userData.ShiftDirectory = shiftPath; // Sets user data and textbox to shiftPath

            if (ShiftDirectory.Text != "")
                ResetApplyState();
        }

        private void CtrlDirectory_click(object sender, RoutedEventArgs e)
        {
            string ctrlPath = DirectoryManager.ChooseFileDirectory(); // Retuns path of chosen directory
            CtrlDirectory.Text = userData.CtrlDirectory = ctrlPath; // Sets user data and textbox to ctrlPath

            if (CtrlDirectory.Text != "")
                ResetApplyState();
        }

        private void FtpConfirm_click(object sender, RoutedEventArgs e)
        {
            userData.FtpAddress = FtpAddress.Text;
            userData.FtpUsername = FtpUsername.Text;
            userData.FtpPassword = FtpPassword.Password;
        }

        private void ShiftCheckbox_changed(object sender, RoutedEventArgs e)
        {
            userData.ShiftCheckbox = (bool)ShiftCheckbox.IsChecked;
            ResetApplyState();
        }

        private void CtrlCheckbox_changed(object sender, RoutedEventArgs e)
        {
            userData.CtrlCheckbox = (bool)CtrlCheckbox.IsChecked;
            ResetApplyState();
        }

        private void FtpCheckbox_changed(object sender, RoutedEventArgs e)
        {
            userData.FtpCheckbox = (bool)FtpCheckbox.IsChecked;
            ResetApplyState();
        }

        /* Saves directory paths to file */
        private void ApplyButton_click(object sender, RoutedEventArgs e)
        {
            FileManager.Save(); // Saves directory paths to file
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

            if (ApplyButton.IsEnabled == true)
                FileManager.Save(); // Saves directory paths to file
            App.Current.Shutdown();
        }
        
        /* Resets the visibility of the 'Apply' button from visible to shaded. */
        private void ResetApplyState()
        {
            ApplyButton.IsEnabled = true;
        }


    }
}
