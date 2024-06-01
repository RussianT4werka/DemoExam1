using DemoExam1.DB;
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
    /// Логика взаимодействия для CreateShift.xaml
    /// </summary>
    public partial class CreateShiftPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public DateTime SelectedDateStart {  get; set; }
        public DateTime SelectedDateEnd { get; set; }
        public List<User> UsersTechAndOrg { get; set; }
        public List<Userlist> UsersForShift 
        { 
            get => usersForShift;
            set
            {
                usersForShift = value;
                SignalChanged();
            }
        }
        public User SelectedUser { get; set; }
        private Shift lastShift;
        private List<Userlist> usersForShift;


        public CreateShiftPage()
        {
            InitializeComponent();
            DataContext = this;
            UsersTechAndOrg = DB.ConferenceContext.Instance().Users.Where( s => s.Userrole.Namerole == "Организатор" || s.Userrole.Namerole == "Техник" && s.Status != "уволен").Include( s => s.Userrole).ToList();
        }
        protected void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void CreateShift(object sender, RoutedEventArgs e)
        {
            Shift newShift = new Shift() { Datestart = SelectedDateStart, Dateend = SelectedDateEnd };
            DB.ConferenceContext.Instance().Add(newShift);
            DB.ConferenceContext.Instance().SaveChanges();
            lastShift = DB.ConferenceContext.Instance().Shifts.ToList().Last();
        }

        private void AssignToShift(object sender, RoutedEventArgs e)
        {
            Userlist newUsetList = new Userlist() {Shiftid = lastShift.Shiftid, Userid = SelectedUser.Userid };
            DB.ConferenceContext.Instance().Add(newUsetList);
            DB.ConferenceContext.Instance().SaveChanges();
            UsersForShift = DB.ConferenceContext.Instance().Userlists.Include( s => s.User).ThenInclude( s => s.Userrole).Where( s => s.Shiftid == lastShift.Shiftid).ToList();
        }

    }
}
