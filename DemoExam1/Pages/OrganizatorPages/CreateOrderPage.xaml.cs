using DemoExam1.DB;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoExam1.Pages.OrganizatorPages
{
    /// <summary>
    /// Логика взаимодействия для CreateOrderPage.xaml
    /// </summary>
    public partial class CreateOrderPage : Page, INotifyPropertyChanged
    {
        private User User;
        private string nameConference;
        private string equipment;
        private int amountGuest;

        public event PropertyChangedEventHandler? PropertyChanged;

        public DateTime DateCreationOrder {  get; set; } = DateTime.Now;
        public string PaymentStatus { get; set; } = "не принят";
        public string OrderStatus { get; set; } = "готовится";
        public string NameConference { get => nameConference; set
            {
                nameConference = value;
                SignalChanged();
            }
        }
        public string Equipment { get => equipment; set
            {
                equipment = value;
                SignalChanged();
            }
        }
        public int AmountGuest { get => amountGuest; set
            {
                amountGuest = value;
                SignalChanged();
            }
        }


        public CreateOrderPage(User user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
        }
        protected void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void CreateOrder(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameConference) && !string.IsNullOrEmpty(Equipment) && AmountGuest != null)
            {
                try
                {
                    DB.ConferenceContext.Instance().Orders.Add(new Order { Datecreation = DateCreationOrder, Orderstatus = OrderStatus, Paymentstatus = PaymentStatus, Nameconference = NameConference, Equipment = Equipment, Amountguests = AmountGuest });
                    DB.ConferenceContext.Instance().SaveChanges();
                    var lastOrder = DB.ConferenceContext.Instance().Orders.ToList().Last();
                    DB.ConferenceContext.Instance().Orderuserlists.Add(new Orderuserlist() { Userid = User.Userid, Orderid = lastOrder.Orderid });
                    DB.ConferenceContext.Instance().SaveChanges();
                    MessageBox.Show("Заказ успешно создан!");
                    NameConference = null;
                    Equipment = null;
                    AmountGuest = 0;
                }
                catch 
                {
                    MessageBox.Show("Случилась непредвиденная ошибка");
                }
            }
        }
    }
}
