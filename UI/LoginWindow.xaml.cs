using System;
using System.Linq;
using System.Windows;
using DeliveryApp.DBEntities;
using DeliveryApp.Libs;


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
            MainWindow mainWindow = new MainWindow();
            try
            {
                User currentUser = Delivery_DBEntities.GetContext().Users.FirstOrDefault(p => p.Login == textBox_Login.Text && (p.Password == pswrdBox_Login.Password || p.Password == textBox_PasswordShow.Text));
                if (currentUser == null)
                {
                    MessageBox.Show("Введен неверный логин или пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                else
                {
                    SaveEntryInfo.SaveLoginHistory(currentUser.User_ID, DateTime.Now);
                    mainWindow.textBox_LogInName.Text = currentUser.Login;
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Отсутствует подключение к базе данных");
            }
        }
    }
}
