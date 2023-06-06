using System;
using System.Collections.Generic;
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

namespace TheMostImportantProject
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            if (LastName.Text != "" && FirstName.Text != "" && Password.Password != "" && DateOfBirth.SelectedDate != null & PhoneNumber.Text != "")
            {
                using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
                {
                    if (db.Client.Where(x => x.PhoneNumber == PhoneNumber.Text).FirstOrDefault() == null)
                    {
                        Client client = new Client();
                        client.LastName = LastName.Text;
                        client.FirstName = FirstName.Text;
                        client.Password = Password.Password;
                        client.DateOfBirth = DateOfBirth.SelectedDate;
                        client.PhoneNumber = PhoneNumber.Text;
                        db.Client.Add(client);
                        db.SaveChanges();
                        MainWindow authorization = new MainWindow();
                        authorization.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Такой номер телефона уже зарегестрирован");
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            MainWindow authorization = new MainWindow();
            this.Hide();
            authorization.Show();
        }
    }
}