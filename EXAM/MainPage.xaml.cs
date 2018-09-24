using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EXAM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private String Message;
        public MainPage()
        {
            this.InitializeComponent();
            
            
        }

        private async void Search(object sender, RoutedEventArgs e)
        {
            String Name = FileName.Text;
            String Content = ContentText.Text;
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile file = await folder.GetFileAsync(Name);
                if (file == null)
                {
                    Message = "File not found";
                }
                else
                {
                    String text = await FileIO.ReadTextAsync(file);
                    if (text.Contains(Content))
                    {
                        Message = "File found and text found";
                    }
                    else
                    {
                        Message = "File found but text not found";
                    }
                }
            }
            catch (Exception exception)
            {
                Message = "File not found";
                Debug.WriteLine(exception.Message);
            }
            
            

            DisplayNoWifiDialog();
        }

        private async void DisplayNoWifiDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "",
                Content = Message,
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
    }
}
