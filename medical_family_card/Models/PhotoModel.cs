using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using medical_family_card.Views;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Security.Policy;

namespace medical_family_card.Models
{
    public static class PhotoModel
    {

        public static byte[] ConvertPhotoToBinaryData(PhotoView photoView)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)photoView.photo.Source));
            MemoryStream memoryStream = new MemoryStream();
            encoder.Save(memoryStream);

            return memoryStream.ToArray();
        }

        public static BitmapImage ConvertBinaryDataToPhoto(byte[] binaryData, PhotoView photoView)
        {
            BitmapImage bitmapImage = new BitmapImage();
            
            if (binaryData != null)
            {
                var binaryBuffer = binaryData.ToArray();
                MemoryStream memoryStream = new MemoryStream(binaryBuffer);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();

                photoView.photo.Source = bitmapImage;
            }

            return bitmapImage;
        }


        //==============================//
        public static BitmapImage ConvertStringBase64ToPhoto(string stringBase64, PhotoView photoView)
        {
            BitmapImage bitmapImage = new BitmapImage();
            var byteBuffer = Convert.FromBase64String(stringBase64);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();

            photoView.photo.Source = bitmapImage;

            return bitmapImage;

            //=//

            //Image photo = Image.FromStream(new MemoryStream(Convert.FromBase64String(stringBase64)));
            //return photo;
        }

        public static string ConvertPhotoToStringBase64(PhotoView photoView)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)photoView.photo.Source));
            MemoryStream memoryStream = new MemoryStream();
            encoder.Save(memoryStream);
            byte[] binaryData = memoryStream.ToArray();

            return Convert.ToBase64String(binaryData);

            //PngBitmapEncoder encoder = new PngBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            //MemoryStream memoryStream = new MemoryStream();
            //encoder.Save(memoryStream);
            //byte[] binaryData = memoryStream.ToArray();

            //return Convert.ToBase64String(binaryData);

            //string stringBase64 = Convert.ToBase64String(File.ReadAllBytes(photoPath));
            //return stringBase64;
        }
        //==============================//

        public static void DeletePhotoViewFromStackPanel(PhotoView photoView, StackPanel stackPanelParent)
        {
            stackPanelParent.Children.Remove(photoView);
        }

        /// <summary>
        /// Метод для удаления контрола из дерева (Для Лекарств...)
        /// </summary>
        public static void DeletePhotoViewFromTree(PhotoView photoView, TreeView treeView)
        {

        }
        
        /// <summary>
        /// Метод для удаления только изображения с контрола (для MyCard)
        /// </summary>
        /// <param name="photoView"></param>
        public static void DeleteImageFromPhotoView(PhotoView photoView)
        {
            photoView.photo.Source = null;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("pack://application:,,,/Views/IconDefaultUserPhoto.png");
            bitmapImage.EndInit();

            photoView.photo.Source = bitmapImage;
        }

        public static void DownloadPhoto(PhotoView photoView)
        {
            if (photoView.photo.Source != null)
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)photoView.photo.Source));
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp";
                if (saveFileDialog.ShowDialog() == true)
                {
                    using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        encoder.Save(stream);
                    }
                }
            }
        }

        public static bool UploadPhoto(PhotoView photoView)
        {
            bool result = false;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                byte[] binaryData = File.ReadAllBytes(openFileDialog.FileName);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(binaryData);
                bitmapImage.EndInit();

                photoView.photo.Source = bitmapImage;
                result = true;
            }

            return result;
        }

        public static void OpenPhoto(PhotoView photoView)
        {
            if (photoView.photo.Source != null)
            {
                //
            }
        }
    }
}
