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
            List<Package> currentPackages = Delivery_DBEntities.GetContext().Packages.ToList();
           
            PackageInfo.ViewPackages.ItemsSource = currentPackages;
            List<SortTypes> sort_types = new List<SortTypes>()
            {  new SortTypes {ID=1, Name = "Без упорядочивания"},
               new SortTypes {ID=2, Name = "возрастанию Номеров посылок"},
               new SortTypes {ID=3, Name = "убиыванию номеров"}
            };
            
            SortType.ItemsSource = sort_types;
            SortType.SelectedIndex = 0;
            UpdatePackageView();
        }

        private void UpdatePackageView()
        {
            var currentPackages = Delivery_DBEntities.GetContext().Packages.ToList();

            currentPackages = currentPackages.Where(p => p.Package_Number.ToLower().Contains(txtBox_Search.Text.ToLower())).ToList();

            if (CheckActual.IsChecked.Value)
                currentPackages = currentPackages.Where(p => p.isActive).ToList();

            if (SortType.SelectedIndex == 0)

            if (SortType.SelectedIndex == 1)
                currentPackages = currentPackages.OrderBy(p => p.Package_Number).ToList();

            if (SortType.SelectedIndex == 2)
                currentPackages = currentPackages.OrderByDescending(p => p.Package_Number).ToList();

            PackageInfo.ViewPackages.ItemsSource = currentPackages;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditPackageWindow addEditPackageWindow = new AddEditPackageWindow(null);
            addEditPackageWindow.ShowDialog();
        }

        private void btn_UpdateData_Click(object sender, RoutedEventArgs e)
        {
            List<Package> currentPackages = Delivery_DBEntities.GetContext().Packages.ToList();
            PackageInfo.ViewPackages.ItemsSource = currentPackages;
            CheckActual.IsChecked = true;
            SortType.SelectedItem = 1;
        }
       
        private void Window_Activated(object sender, EventArgs e)
        {
            List<Package> currentPackages = Delivery_DBEntities.GetContext().Packages.ToList();
            PackageInfo.ViewPackages.ItemsSource = currentPackages;

        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdatePackageView();
        }

        private void CheckActual_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdatePackageView();
        }

        private void txtBox_Search_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdatePackageView();
        }

        private void SortType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdatePackageView();
        }
    }
}
