using DemoExam1.DB;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DemoExam1.Pages.ZavPodrazdelPages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public List<Order> Orders { get; set; }
        public OrdersPage()
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
    }
}
