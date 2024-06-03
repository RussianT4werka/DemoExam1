using DemoExam1.DB;
using DemoExam1.Pages.OrganizatorPages;
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
using System.Windows.Shapes;

namespace DemoExam1.Windows
{
    /// <summary>
    /// Логика взаимодействия для OrganizatorWin.xaml
    /// </summary>
    public partial class OrganizatorWin : Window, INotifyPropertyChanged
    {
        private Page currentPage;
        private User User;

        public event PropertyChangedEventHandler? PropertyChanged;
        public Page CurrentPage 
        {
            get => currentPage;
            set
            {
                currentPage = value;
                SignalChanged();
            }
        }
        public OrganizatorWin(User user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
            CurrentPage = new OrderListPage();
        }
        protected void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void ToCreateOrderPage(object sender, RoutedEventArgs e)
        {
            CurrentPage = new CreateOrderPage(User);
        }

        private void ToListOrderPage(object sender, RoutedEventArgs e)
        {
            CurrentPage = new OrderListPage();
        }
    }
}
