using Dropbox.Api;
using Dropbox.Api.Files;
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
using System.Windows.Shapes;
using System.Data.Entity;
using Microsoft.Win32;

namespace Dropbox_ControlWork
{
    public partial class DropboxWindow : Window
    {
        public DropboxWindow()
        {
            InitializeComponent();
            Run();
        }
        

        public async void Run()
        {
            using (var dbx = new DropboxClient("tAGRkUNmw2sAAAAAAAAE0GKGtkDkQPwMhfcr9Xr3wtwVXyWieZy-t8G31JlU9MsM"))
            {
                var list = await dbx.Files.ListFolderAsync(string.Empty);
                
                foreach (var item in list.Entries.Where(i => i.IsFile))
                {
                    listFiles.Items.Add(item.Name);
                }
            }
        }

        public async void Upload()
        {
            using (var dbx = new DropboxClient("tAGRkUNmw2sAAAAAAAAE0GKGtkDkQPwMhfcr9Xr3wtwVXyWieZy-t8G31JlU9MsM"))
            {
                OpenFileDialog file = new OpenFileDialog();
                file.ShowDialog();
                await dbx.Files.UploadAsync($"/{file.SafeFileName}", null, false, null, false, null, false, file.OpenFile());
            }
        }

        public async void Download()
        {
            using (var dbx = new DropboxClient("tAGRkUNmw2sAAAAAAAAE0GKGtkDkQPwMhfcr9Xr3wtwVXyWieZy-t8G31JlU9MsM"))
            {
                string folder = "mobil";
                string file = "12.jpg";
                string localFilePath = "C:\\Users\\мукушева\\Downloads";
                using (var response = await dbx.Files.DownloadAsync("/" + folder + "/" + file))
                {
                    using (var fileStream = File.Create(localFilePath))
                    {
                        (await response.GetContentAsStreamAsync()).CopyTo(fileStream);
                    }
                }
            }
        }

        private void ListFilesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void DownloadCloud(object sender, RoutedEventArgs e)
        {
            Download();
        }

        private void UploadCloud(object sender, RoutedEventArgs e)
        {
            Upload();
        }
    }
}
