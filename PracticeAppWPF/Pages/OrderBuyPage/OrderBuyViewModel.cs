using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticeAppWPF.Pages.OrderBuyPage
{
    internal class OrderBuyViewModel : INotifyPropertyChanged
    {
        private string m_name;
        private string m_surname;
        private string m_patronymic;
        private string m_phone;
        private string m_email;
        private string m_street;
        private string m_home;
        private string m_room;
        private string m_entrance;
        private string m_floor;

        public string Street
        {
            get => m_street;
            set
            {
                m_street = value;
                OnPropertyChanged("Street");
            }
        }

        public string Home
        {
            get => m_home;
            set
            {
                m_home = value;
                OnPropertyChanged("Home");
            }
        }

        public string Room
        {
            get => m_room;
            set
            {
                m_room = value;
                OnPropertyChanged("Room");
            }
        }

        public string Entrance
        {
            get => m_entrance;
            set
            {
                m_entrance = value;
                OnPropertyChanged("Entrance");
            }
        }

        public string Floor
        {
            get => m_floor;
            set
            {
                m_floor = value;
                OnPropertyChanged("Floor");
            }
        }

        public string Name
        {
            get => m_name;
            set
            {
                m_name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Surname
        {
            get => m_surname;
            set
            {
                m_surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string Patronymic
        {
            get => m_patronymic;
            set
            {
                m_patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }
        public string Phone
        {
            get => m_phone;
            set
            {
                m_phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public string Email
        {
            get => m_email;
            set
            {
                m_email = value;
                OnPropertyChanged("Email");
            }
        }

        public OrderBuyViewModel() {
            var user = MainWindow.CurrentUser;
            Name = user.Name;
            Surname = user.Surname;
            Patronymic = user.Patronymic;
            Phone = user.NumberPhone;
            Email = user.Email;
            Street = user.Street;
            Home = user.Home;
            Room = user.Room;
            Entrance = user.Entrance;
            Floor = user.Floor;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
