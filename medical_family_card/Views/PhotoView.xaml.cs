using medical_family_card.Models;
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

namespace medical_family_card.Views
{
    /// <summary>
    /// Логика взаимодействия для PhotoView.xaml
    /// </summary>
    public partial class PhotoView : UserControl
    {
        /// Режим 0 - MyCard, Режим 1 - Лекарства, Посещения...
        public EnumView Regime { get; set; }
        //Свойства для хранения фотографий в stackPanel
        public StackPanel StackPanelParent { get; set; }        

        public PhotoView()
        {
            InitializeComponent();
        }

        public PhotoView(StackPanel stackPanelParent, EnumView regime)
        {
            InitializeComponent();
            StackPanelParent = stackPanelParent;
            Regime = regime;
        }

        private void uploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            PhotoModel.UploadPhoto(this);
        }

        private void downloadPhoto_Click(object sender, RoutedEventArgs e)
        {
            PhotoModel.DownloadPhoto(this);
        }

        private void deletePhoto_Click(object sender, RoutedEventArgs e)
        {
            switch (Regime)
            {
                case EnumView.MyCard:
                    PhotoModel.DeleteImageFromPhotoView(this);
                    break;
                default:
                    PhotoModel.DeletePhotoViewFromStackPanel(this, StackPanelParent);
                    break;
            }
        }

        private void openPhoto_Click(object sender, RoutedEventArgs e)
        {
            PhotoModel.OpenPhoto(this);
        }

        public void ExtraButtonVisible(bool regime)
        {
            if (regime)
            {
                this.IsEnabled = true;
                this.uploadPhoto.Visibility = Visibility.Visible;
                this.downloadPhoto.Visibility = Visibility.Visible;
                this.deletePhoto.Visibility = Visibility.Visible;
                this.openPhoto.Visibility = Visibility.Visible;
            }
            else
            {
                this.IsEnabled = true;
                this.uploadPhoto.Visibility = Visibility.Hidden;
                this.downloadPhoto.Visibility = Visibility.Visible;
                this.deletePhoto.Visibility = Visibility.Hidden;
                this.openPhoto.Visibility = Visibility.Visible;
            }
        }
    }
}
