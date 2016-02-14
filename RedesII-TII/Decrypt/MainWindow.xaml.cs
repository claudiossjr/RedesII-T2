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

        private void Process_Click(object sender, RoutedEventArgs e)
        {
            string pathOpen = string.Empty;
            string pathSave = string.Empty;

            OpenFileDialog fileOpen = new OpenFileDialog();
            SaveFileDialog fileSave = new SaveFileDialog();

            bool? answerOpen = fileOpen.ShowDialog();
            bool? answerSave = fileSave.ShowDialog();

            if ((answerOpen != null && answerOpen == true) && (answerSave != null && answerSave == true))
            {
                pathOpen = (fileOpen.FileName != null) ? fileOpen.FileName : string.Empty;
                pathSave = (fileSave.FileName != null) ? fileSave.FileName : string.Empty;

                string fileOpenedName = System.IO.Path.GetFileName(pathOpen);
                string fileSavedName = System.IO.Path.GetFileName(pathSave);

                ChangeLabelText(fileOpenedName, fileSavedName);
                modeling.DecryptFile(fileOpen.FileName, fileSave.FileName);

            }
            else
            {
                this.SetStatus("You are missing some files.");
            }
        }

        public void SetStatus(string status)
        {
            lbStatus.Content = status;
        }

        private void ChangeLabelText(string pathOpen, string pathSave)
        {
            lbFrom.Content = pathOpen;
            lbTo.Content = pathSave;
        }

        private void Key_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;
            OpenFileDialog file = new OpenFileDialog();

            bool? answer = file.ShowDialog();

            if (answer != null && answer == true)
            {
                path = (file.FileName != null) ? file.FileName : string.Empty;

                string fileName = System.IO.Path.GetFileName(path);

                bool status = modeling.ProcessKey(file.FileName);
                
                if(status)
                {
                    Key.Content = fileName;
                    EnableContent();
                }
                
            }
            else
            {
                this.SetStatus("You are missing key file.");
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
