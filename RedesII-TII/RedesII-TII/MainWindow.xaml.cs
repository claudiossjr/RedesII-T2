using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace RedesII_TII
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;
            OpenFileDialog file = new OpenFileDialog();

            bool? answer = file.ShowDialog();

            if( answer != null && answer == true)
            {
                path = (file.FileName != null) ? file.FileName : string.Empty;
            }

            string fileName = System.IO.Path.GetFileName( path );

            ChangeLabelText( fileName );

        }

        private void ChangeLabelText( string path )
        {
            lbStatus.Content = path;
        }
    }
}
