using DeliveryApp.DBEntities;
using DeliveryApp.Libs;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace DeliveryApp.UI.UC
{
    /// <summary>
    /// Логика взаимодействия для PackageInfoView.xaml
    /// </summary>
    public partial class PackageInfoView : UserControl
    {
        public PackageInfoView()
        {
            InitializeComponent();
        }

        private void btn_IssuePackage_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выдать посылку?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Package selectedItem = ViewPackages.SelectedItem as Package;
                if (selectedItem == null)
                {
                    MessageBox.Show("Посылка не выбрана", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (selectedItem.isActive == false)
                    {
                        MessageBox.Show("Посылка уже выдана", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        Issue.IssuePackage(selectedItem.Package_ID);
                        PackageInfo.ViewPackages.ItemsSource = Delivery_DBEntities.GetContext().Packages.ToList();
                        MessageBox.Show("Посылка выдана", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
            }
        }

        private void btn_EditData_Click(object sender, RoutedEventArgs e)
        {
            AddEditPackageWindow addEditPackageWindow = new AddEditPackageWindow((sender as Button).DataContext as Package);
            addEditPackageWindow.ShowDialog();
        }
    }
}
