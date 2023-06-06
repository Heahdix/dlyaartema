using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace TheMostImportantProject
{

    public partial class PositionAdd : Window
    {
        Position currentPos = new Position();
        public System.Drawing.Image positionImageToConvert;
        public string fileName;
        public PositionAdd(Position pos = null)
        {
            currentPos = pos;
            InitializeComponent();
            if (pos != null)
            {
                fileName = @"C:\Users\Almaz\OneDrive\Рабочий стол\Praktika-master\TheMostImportantProject\Pictures\" + pos.PositionID + ".png";

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new FileStream(@"C:\Users\Almaz\OneDrive\Рабочий стол\Praktika-master\TheMostImportantProject\Pictures\" + pos.PositionID + ".png", FileMode.Open, FileAccess.Read);
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                bi.StreamSource.Dispose();
                PositionImage.Source = bi;
                Name.Text = pos.Name;
                Price.Text = string.Format("{0:0.00}", pos.Price);
                IsHidden.IsChecked = pos.IsHidden;
                SaveBtn.Content = "Изменить позицию";
            }
        }


        private void SelectImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog pictureDialog = new OpenFileDialog();
            pictureDialog.Title = "Выбрать изображение";
            pictureDialog.Filter = "Image Files(*.JPG;*.PNG;*.GIF)|*.JPG;*PNG;*.GIF";

            if (pictureDialog.ShowDialog() == true)
            {
                fileName = pictureDialog.FileName;
                BitmapImage positionPicture = new BitmapImage();
                positionPicture.BeginInit();
                positionPicture.StreamSource = new FileStream(pictureDialog.FileName, FileMode.Open, FileAccess.Read);
                positionPicture.CacheOption = BitmapCacheOption.OnLoad;
                positionPicture.EndInit();
                positionPicture.StreamSource.Dispose();

                positionImageToConvert = System.Drawing.Image.FromFile(pictureDialog.FileName);
                PositionImage.Source = positionPicture;
            }
        }

        public void AddNew(string name, decimal price, bool IsHidden)
        {
            using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
            {
                ImageConverter converter = new ImageConverter();
                var ImageConvert = converter.ConvertTo(positionImageToConvert, typeof(byte[]));

                Position position = new Position();
                position.Name = name;
                position.Price = price;
                position.Image = (byte[])ImageConvert;
                position.IsHidden = IsHidden;

                db.Position.Add(position);
                db.SaveChanges();

                Bitmap bmp = new Bitmap(fileName);
                bmp.Save(@"C:\Users\Almaz\OneDrive\Рабочий стол\Praktika-master\TheMostImportantProject\Pictures\" + position.PositionID + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        public void Change(string name, decimal price, bool IsHidden)
        {
            using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
            {
                Position position = db.Position.Where(x => x.PositionID == currentPos.PositionID).FirstOrDefault();
                position.Name = name;
                position.Price = price;

                if (positionImageToConvert != null)
                {
                    ImageConverter converter = new ImageConverter();
                    var ImageConvert = converter.ConvertTo(positionImageToConvert, typeof(byte[]));
                    position.Image = (byte[])ImageConvert;
                    positionImageToConvert.Dispose();
                    File.Delete(@"C:\Users\Almaz\OneDrive\Рабочий стол\Praktika-master\TheMostImportantProject\Pictures\" + position.PositionID + ".png");
                    Bitmap bmp = new Bitmap(fileName);
                    bmp.Save(@"C:\Users\Almaz\OneDrive\Рабочий стол\Praktika-master\TheMostImportantProject\Pictures\" + position.PositionID + ".png", System.Drawing.Imaging.ImageFormat.Png);
                }

                position.IsHidden = IsHidden;

                db.SaveChanges();


            }
        }

        private void Price_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.OemComma)
            {
                e.Handled = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "" || Price.Text == "" || PositionImage.Source == null)
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
                {
                    if (currentPos == null)
                    {
                        //Position position = new Position();
                        //position.Name = Name.Text;
                        //position.Price = Convert.ToDecimal(Price.Text);

                        AddNew(Name.Text, Convert.ToDecimal(Price.Text), (bool)IsHidden.IsChecked);
                        //position.Image = (byte[])ImageConvert;
                        //if ((bool)IsHidden.IsChecked)
                        //{
                        //    position.IsHidden = true;
                        //}
                        //else
                        //{
                        //    position.IsHidden = false;
                        //}
                        //db.Position.Add(position);
                        //db.SaveChanges();
                        //Bitmap bmp = new Bitmap(fileName);
                        //bmp.Save(@"C:\Users\Almaz\OneDrive\Рабочий стол\Praktika-master\TheMostImportantProject\Pictures\" + position.PositionID + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        MessageBox.Show("Позиция успешно добавлена");
                    }
                    else
                    {
                        //ImageConverter converter = new ImageConverter();
                        //object ImageConvert;

                        //Position position = db.Position.Where(x => x.PositionID == currentPos.PositionID).FirstOrDefault();
                        //position.Name = Name.Text;
                        //position.Price = Convert.ToDecimal(Price.Text);
                        if (positionImageToConvert != null)
                        {
                            //ImageConvert = converter.ConvertTo(positionImageToConvert, typeof(byte[]));
                            //position.Image = (byte[])ImageConvert;
                            //positionImageToConvert.Dispose();
                            //File.Delete(@"C:\Users\Almaz\OneDrive\Рабочий стол\Praktika-master\TheMostImportantProject\Pictures\" + position.PositionID + ".png");
                            //Bitmap bmp = new Bitmap(fileName);
                            //bmp.Save(@"C:\Users\Almaz\OneDrive\Рабочий стол\Praktika-master\TheMostImportantProject\Pictures\" + position.PositionID + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        }

                        //if ((bool)IsHidden.IsChecked)
                        //{
                        //    position.IsHidden = true;
                        //}
                        //else
                        //{
                        //    position.IsHidden = false;
                        //}
                        //db.SaveChanges();

                        Change(Name.Text, Convert.ToDecimal(Price.Text), (bool)IsHidden.IsChecked);
                        MessageBox.Show("Позиция успешно изменена");
                    }
                }
            }
        }

        private void ToEmployees_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Employees employees = new Employees();
            employees.Show();
            this.Close();
        }

        private void ToMenu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
            this.Close();
        }
    }
}
