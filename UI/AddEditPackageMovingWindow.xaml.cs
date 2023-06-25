using DeliveryApp.DBEntities;
using System;
using System.Linq;
using System.Windows;

namespace DeliveryApp.UI
{
    /// <summary>
    /// Логика взаимодействия для AddEditPackageMovingWindow.xaml
    /// </summary>
    public partial class AddEditPackageMovingWindow : Window
    {
        private Package_Movings current_Moving = new Package_Movings();

        public AddEditPackageMovingWindow(Package_Movings selectedMoving)
        {
            InitializeComponent();
            var move_Types = Delivery_DBEntities.GetContext().Move_Types.ToList();
            var packages = Delivery_DBEntities.GetContext().Packages.ToList();

            if (selectedMoving != null)
            {
                current_Moving = selectedMoving;
                datetimePicker.Value = current_Moving.MoveDate;
            }

            DataContext = current_Moving;
            txtBox_PackageNumber.ItemsSource = packages;
            cmbBox_MoveType.ItemsSource = move_Types;
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_SaveChanges_Click(object sender, RoutedEventArgs e)
        {         
            if (current_Moving.Move_ID == 0)
            {
                current_Moving.MoveDate = (DateTime)datetimePicker.Value;
                Delivery_DBEntities.GetContext().Package_Movings.Add(current_Moving);
            }

            try
            {
                if (MessageBox.Show("Вы действительно хотите сохранить новые данные?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    current_Moving.MoveDate = (DateTime)datetimePicker.Value;
                    Delivery_DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные сохранены", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }

            catch (Exception)
            {
                MessageBox.Show("при сохранении данных произошла ошибка", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
