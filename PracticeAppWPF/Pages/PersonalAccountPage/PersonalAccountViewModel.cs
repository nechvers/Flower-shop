using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Bcrypt = BCrypt.Net.BCrypt;

namespace PracticeAppWPF.Pages.PersonalAccountPage
{
    public class PersonalAccountViewModel : INotifyPropertyChanged
    {
        private Staff user => MainWindow.CurrentUser;
        private string m_password;
        private Command m_saveCommand;

        public string Name
        {
            get => user.Name;
            set
            {
                user.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Surname
        {
            get => user.Surname;
            set
            {
                user.Surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string Patronymic
        {
            get => user.Patronymic;
            set
            {
                user.Patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }
        public string Passport
        {
            get => user.Passport;
            set
            {
                user.Passport = value;
                OnPropertyChanged("Passport");
            }
        }
        public string Phone
        {
            get => user.NumberPhone;
            set
            {
                user.NumberPhone = value;
                OnPropertyChanged("Phone");
            }
        }
        public string Email
        {
            get => user.Email;
            set
            {
                user.Email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Login
        {
            get => user.Login;
            set
            {
                user.Login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get => m_password;
            set
            {
                m_password = value;
                OnPropertyChanged("Password");
            }
        }

        public Command SaveCommand
        {
            get => m_saveCommand ?? (m_saveCommand = new Command(TrySave));
        }


        public void TrySave(object parameter)
        {

            string pattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";
            if (string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Name)
                || string.IsNullOrEmpty(Patronymic) || string.IsNullOrEmpty(Passport)
                || string.IsNullOrEmpty(Phone)
                || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Login))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }

            string hashedPassword = user.Password;
            if (!string.IsNullOrEmpty(Password) && !Regex.IsMatch(Password, "^(?=.*\\d)(?=.*[a-zA-Z]).{6,}$"))
            {
                hashedPassword = Bcrypt.HashPassword(Password, 4);
            }

            if (!Regex.IsMatch(Email, pattern))
            {
                MessageBox.Show("Введенный Email, не соответсвует правильному email");
                return;

            }

            Staff staff = MainWindow.CurrentUser;

            staff.Login = Login;
            staff.Name = Name;
            staff.Surname = Surname;
            staff.Patronymic = Patronymic;
            staff.Passport = Passport;
            staff.NumberPhone = Phone;
            staff.Email = Email;
            staff.Password = hashedPassword;

            Database.Staffs.AddOrUpdate(staff);

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


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
