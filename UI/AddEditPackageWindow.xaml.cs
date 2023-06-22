using DeliveryApp.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace DeliveryApp.UI
{
    /// <summary>
    /// Логика взаимодействия для AddEditPackageWindow.xaml
    /// </summary>
    public partial class AddEditPackageWindow : Window
    {
        private Package currentPackage = new Package();
        public AddEditPackageWindow(Package selectedPackage)
        {
          InitializeComponent();
          if (selectedPackage != null)
            {
                currentPackage = selectedPackage;
            }

            DataContext = currentPackage;
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
           StringBuilder errors = new StringBuilder();
               
           if (currentPackage.Package_ID == 0)
                Delivery_DBEntities.GetContext().Packages.Add(currentPackage);

           try
           {
              if (MessageBox.Show("Вы действительно хотите добавить новые данные?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes || !string.IsNullOrWhiteSpace(currentPackage.Package_Number)
                    || !string.IsNullOrWhiteSpace(currentPackage.Sender) || !string.IsNullOrWhiteSpace(currentPackage.Recipient))
              {
                    Delivery_DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные сохранены", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    PackageInfoWindow packageInfoWindow = new PackageInfoWindow();
                    if (currentPackage != null)
                    {
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                        packageInfoWindow.Show();                       
                    }
                }
            }

            catch(Exception)
           {
                MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
           }
        }
    }
}
