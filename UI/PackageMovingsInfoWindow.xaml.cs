using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DeliveryApp.DBEntities;
using DeliveryApp.Libs;

namespace DeliveryApp.UI
{
    /// <summary>
    /// Логика взаимодействия для PackageMovingsInfoWindow.xaml
    /// </summary>
    public partial class PackageMovingsInfoWindow : Window
    {
        public PackageMovingsInfoWindow()
        {
            InitializeComponent();
            DGridPackageMovings.ItemsSource = Delivery_DBEntities.GetContext().Package_Movings.ToList();

            var package_Numbers = Delivery_DBEntities.GetContext().Packages.ToList();

            package_Numbers.Insert(0, new Package
            {
                Package_Number = "Все посылки"
            });

            var move_Types = Delivery_DBEntities.GetContext().Move_Types.ToList();
                  
            move_Types.Insert(0, new Move_Types
            {
                MoveType_Name = "Все виды действий"
            });

            cmbBox_PackageNumbers.ItemsSource = package_Numbers;
            cmbBox_PackageNumbers.SelectedIndex = 0;

            cmbBox_moveType.ItemsSource = move_Types;
            cmbBox_moveType.SelectedIndex = 0;
        }

        private void UpdatePackageMovings()
        {
            var currentMovings = Delivery_DBEntities.GetContext().Package_Movings.ToList();

            if (cmbBox_PackageNumbers.SelectedIndex > 0)
                currentMovings = currentMovings.Where(p => p.Package_ID.Equals(cmbBox_PackageNumbers.SelectedIndex)).ToList();

            if (cmbBox_moveType.SelectedIndex > 0)
                currentMovings = currentMovings.Where(p => p.MoveType_ID.Equals(cmbBox_moveType.SelectedIndex)).ToList();

            DGridPackageMovings.ItemsSource = currentMovings;
        }


        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.textBox_LogInName.Text = textBox_LogInName.Text;
            mainWindow.Show();
            this.Close();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditPackageMovingWindow addEditPackageMovingWindow = new AddEditPackageMovingWindow(null);
            addEditPackageMovingWindow.ShowDialog();           
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            var movingForDelete = DGridPackageMovings.SelectedItem;
            if (movingForDelete == null)
            {
                MessageBox.Show($"Не выбрана запись для удаления", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (MessageBox.Show($"Вы действительно хотите удалить выбранную запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Delivery_DBEntities.GetContext().Package_Movings.Remove((Package_Movings)movingForDelete);
                        Delivery_DBEntities.GetContext().SaveChanges();
                        DGridPackageMovings.ItemsSource = Delivery_DBEntities.GetContext().Packages.ToList();

                        MessageBox.Show("Данные удалены!", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    catch (Exception)
                    {
                        MessageBox.Show("Произошла ошибка при удалении \nДанные не были удалены", "Статус",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            Package_Movings selectedMoving = DGridPackageMovings.SelectedItem as Package_Movings;
            if(selectedMoving == null)
            {
                MessageBox.Show("Запись для редактирования не выбрана", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            AddEditPackageMovingWindow addEditPackageMovingWindow = new AddEditPackageMovingWindow(DGridPackageMovings.SelectedItem as Package_Movings);
            addEditPackageMovingWindow.ShowDialog();
        }

        private void btn_EditData_Click(object sender, RoutedEventArgs e)
        {
            AddEditPackageMovingWindow addEditPackageMovingWindow = new AddEditPackageMovingWindow((sender as Button).DataContext as Package_Movings);
            addEditPackageMovingWindow.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            DGridPackageMovings.ItemsSource = Delivery_DBEntities.GetContext().Package_Movings.ToList();
        }

        private void cmbBox_PackageNumbers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePackageMovings();
        }

        private void cmbBox_moveType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePackageMovings();
        }

        private void btn_toPDF_Click(object sender, RoutedEventArgs e)
        {
            ExportToPDF.ExportToPdf(DGridPackageMovings, "Test.pdf");

        }
    }
}
