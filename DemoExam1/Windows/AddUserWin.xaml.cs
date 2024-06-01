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
using System.Windows.Shapes;

namespace DemoExam1.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddUserWin.xaml
    /// </summary>
    public partial class AddUserWin : Window, INotifyPropertyChanged
    {
        private string lastname;
        private string firstname;
        private string middlename;
        private Userrole selectedRole;
        private string login;
        private string password;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Lastname
        {
            get => lastname; set{
            lastname = value; SignalChanged(); }
        }
        public string Firstname 
        { 
            get => firstname; set{
            firstname = value; SignalChanged(); }
        }
        public string Middlename
        {
            get => middlename; set{
            middlename = value; SignalChanged(); }
        }
        public List<Userrole> Roles { get; set; }
        public Userrole SelectedRole 
        { 
            get => selectedRole; set{
            selectedRole = value; SignalChanged(); } 
        }
        public string Login 
        { 
            get => login; set{
            login = value; SignalChanged(); }
        }
        public string Password 
        { 
            get => password; set{
            password = value; SignalChanged();}
        }
        public AddUserWin()
        {
            InitializeComponent();
            DataContext = this;
            try
            {
                Roles = DB.ConferenceContext.Instance().Userroles.ToList();
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
        /// <summary>
        /// Метод добавления нового сотрудника
        /// </summary>
        private void AddUser(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Lastname) && !string.IsNullOrEmpty(Firstname) && !string.IsNullOrEmpty(Middlename) && SelectedRole != null && !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password))
                {
                    User newUser = new User() { Login = Login, Password = Password, Status = null, Lastname = Lastname, Firstname = Firstname, Middlename = Middlename, Userroleid = SelectedRole.Userroleid };
                    DB.ConferenceContext.Instance().Add(newUser);
                    DB.ConferenceContext.Instance().SaveChanges();
                    MessageBox.Show("Сотрудник успешно добавлен");
                    Login = null;
                    Password = null;
                    Lastname = null;
                    Firstname = null;
                    Middlename = null;
                    SelectedRole = null;
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены!");
                }
            }
            catch
            {
                MessageBox.Show("Произошла непредвиденная ошибка!");
            }
        }
    }
}
