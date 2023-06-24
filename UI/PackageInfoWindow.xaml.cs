using DeliveryApp.DBEntities;
using DeliveryApp.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
               new SortTypes {ID=2, Name = "Номеру посылки, возрастание"},
               new SortTypes {ID=3, Name = "Номеру посылки, убывание"}
            };

            SortType.ItemsSource = sort_types;
            SortType.SelectedIndex = 0;
            UpdatePackageView();
        }

        private void UpdatePackageView()
        {
            List<Package> currentPackages = Delivery_DBEntities.GetContext().Packages.ToList();

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
     
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.textBox_LogInName.Text = textBox_LogInName.Text;
            mainWindow.Show();
            this.Close();
        }
      
    }
}