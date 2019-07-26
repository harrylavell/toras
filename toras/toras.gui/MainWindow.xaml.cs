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

namespace toras.gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Apply_button_click(object sender, RoutedEventArgs e)
        {
            Default_directory.Text = "Apply button clicked!";
        }

        private void Cancel_button_click(object sender, RoutedEventArgs e)
        {
            Default_directory.Text = "Cancel button clicked!";
        }

        private void Ok_button_click(object sender, RoutedEventArgs e)
        {
            Default_directory.Text = "OK button clicked!";
        }

        private void Default_directory_click(object sender, RoutedEventArgs e)
        {
            Default_directory.Text = "Directory Button Clicked!";
        }

    }
}
