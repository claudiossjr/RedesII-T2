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

        public EncryptingModel modeling;


        public MainWindow()
        {
            InitializeComponent();

            InitComponent();
            DisableContent();
        }

        private void InitComponent()
        {
            modeling                = new EncryptingModel(this);

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
            string extension = string.Empty;
            string fileOpenedName = string.Empty;
            string fileSavedName = string.Empty;

            OpenFileDialog fileOpen = new OpenFileDialog();

            bool? answerOpen = fileOpen.ShowDialog();

            if ((answerOpen != null && answerOpen == true))
            {
                pathOpen = (fileOpen.FileName != null) ? fileOpen.FileName : string.Empty;

                FileInfo infoOpen = new FileInfo(fileOpen.FileName);
                extension = infoOpen.Extension;
            }
            else
            {
                this.SetStatus("You are missing open file.");
            }

            //part of File.
            SaveFileDialog fileSave = new SaveFileDialog();
            fileSave.DefaultExt = extension;

            bool? answerSave = fileSave.ShowDialog();

            if ((answerSave != null && answerSave == true))
            {
                pathSave = (fileSave.FileName != null) ? fileSave.FileName : string.Empty;
            }
            else
            {
                this.SetStatus("You are missing save file.");
            }

            if (!string.IsNullOrEmpty(pathOpen) && !string.IsNullOrEmpty(pathSave))
            {
                fileOpenedName = System.IO.Path.GetFileName(pathOpen);
                fileSavedName = System.IO.Path.GetFileName(pathSave);

                ChangeLabelText(fileOpenedName, fileSavedName);
                modeling.EncryptFile(pathOpen, pathSave);
            }

        }

        private void ChangeLabelText( string pathOpen, string pathSave )
        {
            lbFrom.Content  = pathOpen;
            lbTo.Content    = pathSave;
        }

        private void Key_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;
            OpenFileDialog fileOpen = new OpenFileDialog();

            bool? answerOpen = fileOpen.ShowDialog();

            if (answerOpen != null && answerOpen == true)
            {
                path = (fileOpen.FileName != null) ? fileOpen.FileName : string.Empty;

                string fileName = System.IO.Path.GetFileName(path);

                bool status = modeling.ProcessKey(fileOpen.FileName);
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

        public void SetStatus(string status)
        {
            lbStatus.Content = status;
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
