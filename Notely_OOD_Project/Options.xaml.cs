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
using System.Windows.Shapes;
//using MaterialDesignThemes;
//using MaterialDesignColors;

namespace Notely_OOD_Project
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        
        public Options()
        {
            InitializeComponent();
            MainWindow main = this.Parent as MainWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           

        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (rbLight.IsChecked == true)
            {

                

            }
        }
    }
}
