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

namespace DemoExam1
{
    public partial class AuthPage : Page, INotifyPropertyChanged
    {
        private string login;
        private string password;

        public string Login 
        { 
            get => login;
            set
            {
                login = value;
                SignalChanged();
            }
        }
        public string Password 
        {
            get => password;
            set
            {
                password = value;
                SignalChanged();
            }
        }
        public AuthPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void Auth(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Login))
            {
                var findUser = DB.ConferenceContext.Instance().Users.Include( s => s.Userrole).FirstOrDefault( s => s.Login == Login && s.Password == Password );
                if(findUser != null)
                {
                    if(findUser.Userrole.Namerole == "Заведующий подразделением")
                    {
                        Login = null;
                        Password = null;

                        Window win = new Windows.ZavPodrazdelWin();
                        win.Show();
                    }
                    else if(findUser.Userrole.Namerole == "Организатор")
                    {
                        Login = null;
                        Password = null;

                        Window win = new Windows.OrganizatorWin();
                        win.Show();
                    }
                    else if(findUser.Userrole.Namerole == "Техник")
                    {
                        Login = null;
                        Password = null;

                        Window win = new Windows.TechnikWin();
                        win.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        }
    }
}
