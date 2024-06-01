using DemoExam1.Pages.ZavPodrazdelPages;
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
    public partial class ZavPodrazdelWin : Window, INotifyPropertyChanged
    {
        private Page currentPage;

        public Page CurrentPage 
        {
            get => currentPage;
            set
            {
                currentPage = value;
                SignalChanged();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public ZavPodrazdelWin()
        {
            InitializeComponent();
            DataContext = this;
            CurrentPage = new UsersPage();
        }

        protected void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void ToUsersPage(object sender, RoutedEventArgs e)
        {
            CurrentPage = new UsersPage();
        }

        private void ToOrdersPage(object sender, RoutedEventArgs e)
        {
            CurrentPage = new OrdersPage();
        }

        private void ToShiftPage(object sender, RoutedEventArgs e)
        {
            CurrentPage = new ShiftsPage();
        }

        private void ToCreateShiftPage(object sender, RoutedEventArgs e)
        {
            CurrentPage = new CreateShiftPage();
        }
    }
}
