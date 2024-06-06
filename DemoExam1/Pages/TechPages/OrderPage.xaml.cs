using DemoExam1.DB;
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

namespace DemoExam1.Pages.TechPages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page, INotifyPropertyChanged
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
        public OrderPage()
        {
            InitializeComponent();
            DataContext = this;
            Orders = DB.ConferenceContext.Instance().Orders.ToList();
        }
        protected void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void EditOrderStatus(object sender, RoutedEventArgs e)
        {
            var findOrder = DB.ConferenceContext.Instance().Orders.FirstOrDefault( s => s.Orderid == SelectedOrder.Orderid);
            if (findOrder != null)
            {
                if(findOrder.Orderstatus == "готов")
                {
                    findOrder.Orderstatus = "готовится";
                    DB.ConferenceContext.Instance().SaveChanges();
                    Orders = DB.ConferenceContext.Instance().Orders.ToList();
                }
                else if(findOrder.Orderstatus == "готовится")
                {
                    findOrder.Orderstatus = "готов";
                    DB.ConferenceContext.Instance().SaveChanges();
                    Orders = DB.ConferenceContext.Instance().Orders.ToList();
                }
            }
        }
    }
}
