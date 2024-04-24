using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Runtime.CompilerServices;
using System.Windows;

namespace PracticeAppWPF.Pages.DetailedRegistrationPage
{
    internal class DetailedRegistrationViewModel : INotifyPropertyChanged
    {
        private string m_street;
        private string m_home;
        private string m_room;
        private string m_entrance;
        private string m_floor;
        private Command m_registerCommand;

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

        public Command RegisterCommand
        {
            get => m_registerCommand ?? (m_registerCommand = new Command(TryRegister));
        }
        public void TryRegister(object parameter)
        {
        
            if (string.IsNullOrEmpty(Street) || string.IsNullOrEmpty(Home)
                || string.IsNullOrEmpty(Room) || string.IsNullOrEmpty(Entrance)
                || string.IsNullOrEmpty(Floor))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }

            var candidate = MainWindow.Candidate;
            candidate.Street = Street;
            candidate.Home = Home;
            candidate.Room = Room;
            candidate.Entrance = Entrance;
            candidate.Floor = Floor;

            var currentUser = Database.Staffs.Add(candidate);
            MainWindow.CurrentUser = currentUser;

            try
            {
                Database.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage);
                    }
                }
            }

            MainWindow.NavigateToMenuPage();

        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
