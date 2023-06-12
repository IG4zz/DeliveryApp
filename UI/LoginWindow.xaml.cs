using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using DeliveryApp.DBEntities;

namespace DeliveryApp.UI
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void checkBox_ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox_ShowPassword.IsChecked == true)
            {
                textBox_PasswordShow.Text = pswrdBox_Login.Password;
                pswrdBox_Login.Visibility = Visibility.Hidden;
                textBox_PasswordShow.Visibility = Visibility.Visible;
            }

            else
            {
                pswrdBox_Login.Password = textBox_PasswordShow.Text;
                pswrdBox_Login.Visibility = Visibility.Visible;
                textBox_PasswordShow.Visibility = Visibility.Hidden;
            }
        }

        private void btn_LogIn_Click(object sender, RoutedEventArgs e)
        {
            bool loginStatus = false;
            MainWindow mainWindow = new MainWindow();
            try
            {
                foreach (Users user in Delivery_DBEntities.GetContext().Users.Where(c => c.Login.Equals(textBox_Login.Text) && c.Password.Equals(pswrdBox_Login.Password)))
                {
                    loginStatus = true;
                }

                if (loginStatus == true)
                {
                    Users user = Delivery_DBEntities.GetContext().Users.FirstOrDefault(c => c.Login.Equals(textBox_Login.Text));
                    string currentUser = user.Login;
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Введен неверный логин или пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Отсутствует подключение к базе данных");
            }
        }
    }
}
