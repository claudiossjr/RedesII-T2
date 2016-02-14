using Microsoft.Win32;
using RedesII_TII.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        public DecryptingModel modeling;


        public MainWindow()
        {
            InitializeComponent();

            InitComponent();
            DisableContent();
        }

        private void InitComponent()
        {
            modeling                = new DecryptingModel(this);

            lbStatus.IsEnabled      = false;
            lbFileFrom.IsEnabled    = false;
            lbFileTo.IsEnabled      = false;
            lbFrom.IsEnabled        = false;
            lbTo.IsEnabled          = false; 

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;
            OpenFileDialog file = new OpenFileDialog();

            bool? answer = file.ShowDialog();

            if( answer != null && answer == true)
            {
                path = (file.FileName != null) ? file.FileName : string.Empty;

                string fileName = System.IO.Path.GetFileName(path);
                ChangeLabelText(fileName);
                modeling.DecryptFile(file.FileName);

            }

        }

        private void ChangeLabelText( string path )
        {
            lbFrom.Content = path;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;
            OpenFileDialog file = new OpenFileDialog();

            bool? answer = file.ShowDialog();

            if (answer != null && answer == true)
            {
                path = (file.FileName != null) ? file.FileName : string.Empty;

                string fileName = System.IO.Path.GetFileName(path);

                Key.Content = fileName;

                modeling.ProcessKey(file.FileName);
                EnableContent();

            }

        }

        public void SetFileToName(string fileToPath)
        {
            lbTo.Content = fileToPath;
        }

        
        private void EnableContent()
        {
            Key.IsEnabled       = false;
            OpenFile.IsEnabled  = true;
            
        }

        private void DisableContent()
        {
            Key.IsEnabled       = true;
            OpenFile.IsEnabled  = false;
        }

    }
}
