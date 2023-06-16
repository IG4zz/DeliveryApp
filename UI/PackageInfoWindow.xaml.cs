using DeliveryApp.UI.UC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DeliveryApp.DBEntities;
using DeliveryApp.Libs;

namespace DeliveryApp.UI
{
    /// <summary>
    /// Логика взаимодействия для PackageInfoWindow.xaml
    /// </summary>
    public partial class PackageInfoWindow : Window
    {
        public PackageInfoWindow()
        {
            InitializeComponent();
            var currentPackages = Delivery_DBEntities.GetContext().Packages.ToList();
            PackageInfo.ViewPackages.ItemsSource = currentPackages;
        }
    }
}
