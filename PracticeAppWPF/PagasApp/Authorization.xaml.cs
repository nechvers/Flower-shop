using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticeAppWPF.PagasApp
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public bool _validForm = false;
        public Authorization()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateForm();
        }

        public void ValidateForm()
        {
            if (txtVisiblePasswordbox.Visibility == Visibility.Visible)
            {
                passwordBox.Password = txtVisiblePasswordbox.Text;
                txtVisiblePasswordbox.Visibility = Visibility.Hidden;
                passwordBox.Visibility = Visibility.Visible;
                ChekBoxPassword.IsChecked = false;
            }

            if (string.IsNullOrEmpty(Login.Text) || string.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Для входа в систему заполните все поля");
                _validForm = false;
            }
            //else if (passwordBox.Password != User.Password || Login.Text != User.Login)
            //{
            //    MessageBox.Show("Неверный логин или пароль");
            //    _validForm = false;
            //}
            else
            {
                _validForm = true;
            }
        }
    }
}
