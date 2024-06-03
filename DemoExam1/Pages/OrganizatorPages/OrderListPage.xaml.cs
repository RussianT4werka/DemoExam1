using DemoExam1.DB;
using DemoExam1.Windows;
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
    /// Логика взаимодействия для OrderListPage.xaml
    /// </summary>
    public partial class OrderListPage : Page, INotifyPropertyChanged
    {
        private List<Order> orders;

        public event PropertyChangedEventHandler? PropertyChanged;
        public List<Order> Orders 
        { 
            get => orders;
            set
            {
                orders = value;
                SignalChanged();
            }
        }
        public Order SelectedOrder { get; set; }
        public OrderListPage()
        {
            InitializeComponent();
            DataContext = this;
            try
            {
                Orders = DB.ConferenceContext.Instance().Orders.ToList();
            }
            catch
            {
                MessageBox.Show("Ошибка связи с БД");
            }
        }
        protected void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void EditStatus(object sender, RoutedEventArgs e)
        {
            try
            {
                var findSelctedOrder = DB.ConferenceContext.Instance().Orders.FirstOrDefault( s => s.Orderid == SelectedOrder.Orderid);
                if(findSelctedOrder.Paymentstatus == "принят")
                {
                    findSelctedOrder.Paymentstatus = "не принят";
                    DB.ConferenceContext.Instance().SaveChanges();
                    Orders = DB.ConferenceContext.Instance().Orders.ToList();
                }
                else
                {
                    findSelctedOrder.Paymentstatus = "принят";
                    DB.ConferenceContext.Instance().SaveChanges();
                    Orders = DB.ConferenceContext.Instance().Orders.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка связи с БД");
            }
        }

        
    }
}
