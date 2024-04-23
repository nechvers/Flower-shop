using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Bcrypt = BCrypt.Net.BCrypt;

namespace PracticeAppWPF.Pages.RegistrationPage
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private string m_name;
        private string m_surname;
        private string m_patronymic;
        private string m_passport;
        private string m_phone;
        private string m_email;
        private string m_login;
        private Command m_registerCommand;

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
        public string Passport
        {
            get => m_passport;
            set
            {
                m_passport = value;
                OnPropertyChanged("Passport");
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
        public string Login
        {
            get => m_login;
            set
            {
                m_login = value;
                OnPropertyChanged("Login");
            }
        }


        public Command RegisterCommand
        {
            get => m_registerCommand ?? (m_registerCommand = new Command(TryRegister));
        }


        public void TryRegister(object parameter)
        {
            object[] boxes = parameter as object[];
            string Password = (boxes[0] as PasswordBox).Password;
            string PasswordRepeat = (boxes[1] as PasswordBox).Password;

            string pattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";
            if (string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Name)
                || string.IsNullOrEmpty(Patronymic) || string.IsNullOrEmpty(Passport)
                || string.IsNullOrEmpty(Phone)
                || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Login)
                || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(PasswordRepeat))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }
            else if (!Regex.IsMatch(Password, "^(?=.*\\d)(?=.*[a-zA-Z]).{6,}$"))
            {
                MessageBox.Show("Введите пароль, состоящий из латиницы, имеющий хотя бы одну цифру и с минимальной длиной - 6");
                return;
            }
            else if (Password != PasswordRepeat)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            else if (!Regex.IsMatch(Email, pattern))
            {
                MessageBox.Show("Введенный Email, не соответсвует правильному email");
                return;

            }

            MainWindow.NavigateToDetailedRegistrationPage();

            Staff staff = new Staff()
            {
                Login = Login,
                Name = Name,
                Surname = Surname,
                Patronymic = Patronymic,
                Passport = Passport,
                NumberPhone = Phone,
                Email = Email,
                Role = 1, // User role
                Password = Bcrypt.HashPassword(Password, 4),
            };

            MainWindow.Candidate = staff;


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
