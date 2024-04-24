using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Bcrypt = BCrypt.Net.BCrypt;

namespace PracticeAppWPF.Pages.AuthorizationPage
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        private string m_login;
        private Command m_loginCommand;

        public string Login
        {
            get => m_login;
            set
            {
                m_login = value;
                OnPropertyChanged("Login");
            }
        }
        
        public ICommand LoginCommand
        {
            get => m_loginCommand ?? (m_loginCommand = new Command(TryLogin));
        }


        private void TryLogin(object parameter)
        {
            var Password = (parameter as PasswordBox).Password;
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            var employ = Database.Staffs.
                FirstOrDefault(a => a.Login.Equals(Login));

            if (employ == null)
            {
                MessageBox.Show($"Сотрудник с логином {Login} не найден");
                return;
            }

            if (!Bcrypt.Verify(Password, employ.Password)) {
                MessageBox.Show($"Неверный пароль");
                return;
            }

            MainWindow.CurrentUser = employ;
            int? ordersFullPrice = Database.Trashes.Where(a => a.ID_User == employ.ID).Sum(a => (int?)a.Count * a.Flower.Cost);
            MainWindow.OrdersFullPrice = (int)(ordersFullPrice == null ? 0 : ordersFullPrice);
            MainWindow.NavigateToMenuPage();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
