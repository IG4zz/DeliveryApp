using DeliveryApp.DBEntities;
using System.Windows;


namespace DeliveryApp.UI
{
    /// <summary>
    /// Логика взаимодействия для ReceiptWindow.xaml
    /// </summary>
    public partial class ReceiptWindow : Window
    {
        public ReceiptWindow(Package package)
        {
            InitializeComponent();

            PackageNumberBox.Text = package.Package_Number;
            SenderBox.Text = package.Sender;
            RecepientBox.Text = package.Recipient;        
        }
    }
}
