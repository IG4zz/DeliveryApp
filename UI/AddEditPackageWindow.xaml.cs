using DeliveryApp.DBEntities;
using System;
using System.Linq;
using System.Windows;


namespace DeliveryApp.UI
{
    /// <summary>
    /// Логика взаимодействия для AddEditPackageWindow.xaml
    /// </summary>
    public partial class AddEditPackageWindow : Window
    {
        private bool canAdd;
        private Package currentPackage = new Package();
        public AddEditPackageWindow(Package selectedPackage)
        {
            InitializeComponent();
            if (selectedPackage != null)
            {
                currentPackage = selectedPackage;
            }
            else
            {
                canAdd = true;
            }

            DataContext = currentPackage;
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (txtBox_PackageNumber.Text.Length > 10)
            {
                MessageBox.Show("Максимальная длинна номера посылки - 10", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Package package = Delivery_DBEntities.GetContext().Packages.FirstOrDefault(p => p.Package_Number == txtBox_PackageNumber.Text);

            if (canAdd)
            {
                if (package == null)
                {
                    currentPackage.isActive = true;
                    Delivery_DBEntities.GetContext().Packages.Add(currentPackage);
                }
                else
                {
                    MessageBox.Show("Посылка с таким номером уже сущетсвует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }           
            }

            try
            {
                if (MessageBox.Show("Вы действительно хотите сохранить новые данные?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes ||  package == null)
                {
                    Delivery_DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные сохранены", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    PackageInfoWindow packageInfoWindow = new PackageInfoWindow();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Посылка с таким номером уже сущетсвует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
