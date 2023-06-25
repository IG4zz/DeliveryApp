using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DeliveryApp.DBEntities;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeliveryApp.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {    
        public MainWindow()
        {
            InitializeComponent();           
        }   

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btn_GetPackages_Click(object sender, RoutedEventArgs e)
        {
            PackageInfoWindow packageInfoWindow = new PackageInfoWindow();
            packageInfoWindow.textBox_LogInName.Text = textBox_LogInName.Text;
            packageInfoWindow.Show();
            this.Close();
        }

        private void btn_Movings_Click(object sender, RoutedEventArgs e)
        {
            PackageMovingsInfoWindow packageMovingsInfoWindow = new PackageMovingsInfoWindow();
            packageMovingsInfoWindow.textBox_LogInName.Text = textBox_LogInName.Text;
            packageMovingsInfoWindow.Show();
            this.Close();
        }
    }
}
