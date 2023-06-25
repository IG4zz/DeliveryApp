using DeliveryApp.DBEntities;
using System.Windows;


namespace DeliveryApp.UI
{
    /// <summary>
    /// Логика взаимодействия для ReceiptWindow.xaml
    /// </summary>
    public partial class ReceiptWindow : Window
    {
        public string _packageNumber;
        public string _sender;
        public string _recepient;
        public ReceiptWindow(Package package)
        {
            InitializeComponent();

            _packageNumber = package.Package_Number;
            _sender = package.Sender;
            _recepient = package.Recipient;

            PackageNumberBox.Text = package.Package_Number;
            SenderBox.Text = package.Sender;
            RecepientBox.Text = package.Recipient;        
        }
    }
}
