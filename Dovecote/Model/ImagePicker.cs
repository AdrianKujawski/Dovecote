// -----------------------------------------------------------------------
// <copyright file="ImagePicker.cs">
//     Copyright (c) 2017, Adrian Kujawski.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Dovecote.Model {

	public class ImagePicker {
		public static BitmapImage GetImage() {
			try {
				var fileDialog = new OpenFileDialog();
				fileDialog.DefaultExt = ".jpg";
				fileDialog.Filter = "Obrazy (*.gif,*.jpg,*.jpeg,*.bmp)|*.gif;*.jpg;*.jpeg;*.bmp";
				fileDialog.Multiselect = false;

				var result = fileDialog.ShowDialog();
				return result == true ? new BitmapImage(new Uri(fileDialog.FileName)) : null;
			}
			catch (Exception exception) {
				var message = "Wystąpił błąd podczas dodawania zdjęcia." + Environment.NewLine + exception.Message;
				MessageBox.Show(message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			return null;
		}

		public static byte[] ConvertToByteArray(ImageSource image, BitmapEncoder bitmapEncoder) {
			try {
				byte[] bytes;
				var bitmapSource = image as BitmapSource;

				if (image == null) return null;

				bitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapSource));

				using (var stream = new MemoryStream()) {
					bitmapEncoder.Save(stream);
					bytes = stream.ToArray();
				}

				return bytes;
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			return null;
		}

		public static BitmapImage LoadImage(byte[] imageData) {
			if (imageData == null || imageData.Length == 0) return null;
			var image = new BitmapImage();
			using (var mem = new MemoryStream(imageData)) {
				mem.Position = 0;
				image.BeginInit();
				image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
				image.CacheOption = BitmapCacheOption.OnLoad;
				image.UriSource = null;
				image.StreamSource = mem;
				image.EndInit();
			}
			image.Freeze();
			return image;
		}
	}

}
