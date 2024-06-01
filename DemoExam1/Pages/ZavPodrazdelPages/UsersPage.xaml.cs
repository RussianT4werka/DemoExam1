using DemoExam1.DB;
using DemoExam1.Windows;
using Microsoft.EntityFrameworkCore;
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

namespace DemoExam1.Pages.ZavPodrazdelPages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page, INotifyPropertyChanged
    {
        private List<User> users;
        private User selectedUser;

        public event PropertyChangedEventHandler? PropertyChanged;
        public List<User> Users 
        { 
            get => users;
            set
            {
                users = value;
                SignalChanged();
            }
        }
        public User SelectedUser 
        {
            get => selectedUser;
            set 
            {
                selectedUser = value;
                if (SelectedUser != null)
                {
                    Fire.Visibility = Visibility.Visible;
                }
            }
        }
        public UsersPage()
        {
            InitializeComponent();
            DataContext = this;
            try
            {
                Users = DB.ConferenceContext.Instance().Users.Include(s => s.Userrole).ToList();
            }
            catch
            {
                MessageBox.Show("Ошибка связи с БД");
            }
            Fire.Visibility = Visibility.Hidden;
        }

        protected void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        /// <summary>
        /// Метод назачения статуса "уволен" сотруднику
        /// </summary>
        private void FireUser(object sender, RoutedEventArgs e)
        {
            if (SelectedUser != null)
            {
                var findUser = DB.ConferenceContext.Instance().Users.Find(SelectedUser.Userid);
                if (findUser != null)
                {
                    findUser.Status = "уволен";
                    DB.ConferenceContext.Instance().SaveChanges();
                    Users = DB.ConferenceContext.Instance().Users.Include(s => s.Userrole).ToList();
                }
            }
            
        }
        /// <summary>
        /// Метод перехода в окно добавления сотрудника
        /// </summary>
        private void AddUser(object sender, RoutedEventArgs e)
        {
            Window window = new AddUserWin();
            window.Show();
        }
    }
}
