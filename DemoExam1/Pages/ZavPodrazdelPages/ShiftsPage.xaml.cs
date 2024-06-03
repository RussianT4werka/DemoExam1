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
    /// Логика взаимодействия для ShiftsPage.xaml
    /// </summary>
    public partial class ShiftsPage : Page, INotifyPropertyChanged
    {
        private Shift selectedShift;
        private List<Userlist> userList;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Page CurrentPage { get; set; }
        public List<Shift> Shifts { get; set; }
        public List<Userlist> UserList 
        {
            get => userList;
            set
            {
                userList = value;
                SignalChanged();
            }
        }
        public Shift SelectedShift 
        {
            get => selectedShift;
            set
            {
                selectedShift = value;
                if(SelectedShift != null)
                {
                    try
                    {
                        UserList = DB.ConferenceContext.Instance().Userlists.Include( s => s.User).Where( s => s.Shiftid == SelectedShift.Shiftid ).ToList();
                    }
                    catch
                    {
                        MessageBox.Show("Ой всё");
                    }
                }
            }
        }
        protected void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public ShiftsPage()
        {
            InitializeComponent();
            DataContext = this;
            try
            {
                Shifts = DB.ConferenceContext.Instance().Shifts.ToList();
            }
            catch
            {
                MessageBox.Show("Ой всё");
            }
        }
    }
}
