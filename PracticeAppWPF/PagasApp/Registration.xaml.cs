using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO;
using System.IO.Packaging;

namespace PracticeAppWPF.PagasApp
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public bool _validForm = false;
        public string _fullPathPhoto;
        public Registration()
        {
            InitializeComponent();
        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                string destinationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", System.IO.Path.GetFileName(imagePath));

                File.Copy(imagePath, destinationPath); // Изображение скопировано в папку "debug/Images"

                _fullPathPhoto = destinationPath;
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateForm();
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            passwordBox.Visibility = Visibility.Hidden;
            txtVisiblePasswordbox.Visibility = Visibility.Visible;
            txtVisiblePasswordbox.Text = passwordBox.Password;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = txtVisiblePasswordbox.Text;
            passwordBox.Visibility = Visibility.Visible;
            txtVisiblePasswordbox.Visibility = Visibility.Hidden;
        }

        private void ShowPasswordRepeat_Checked(object sender, RoutedEventArgs e)
        {
            PasswordRepeat.Visibility = Visibility.Hidden;
            txtVisiblePasswordRepeat.Visibility = Visibility.Visible;
            txtVisiblePasswordRepeat.Text = PasswordRepeat.Password;
        }

        private void ShowPasswordRepeat_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordRepeat.Password = txtVisiblePasswordRepeat.Text;
            PasswordRepeat.Visibility = Visibility.Visible;
            txtVisiblePasswordRepeat.Visibility = Visibility.Hidden;
        }

        //       -----Методы-----

        public void ValidateForm()
        {
            if (txtVisiblePasswordbox.Visibility == Visibility.Visible)
            {
                passwordBox.Password = txtVisiblePasswordbox.Text;
                txtVisiblePasswordbox.Visibility = Visibility.Hidden;
                passwordBox.Visibility = Visibility.Visible;
                ChekBoxPassword.IsChecked = false;
            }

            if (txtVisiblePasswordRepeat.Visibility == Visibility.Visible)
            {
                PasswordRepeat.Password = txtVisiblePasswordRepeat.Text;
                txtVisiblePasswordRepeat.Visibility = Visibility.Hidden;
                PasswordRepeat.Visibility = Visibility.Visible;
                ChekBoxPasswordRepeat.IsChecked = false;
            }

            string pattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";
            if (string.IsNullOrEmpty(Surname.Text) || string.IsNullOrEmpty(Name.Text)
                || string.IsNullOrEmpty(Patronymic.Text) || string.IsNullOrEmpty(Passport.Text)
                || string.IsNullOrEmpty(Phone.Text) || Post.SelectedItem == null || Division.SelectedItem == null
                || string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(Login.Text)
                || string.IsNullOrEmpty(passwordBox.Password) || string.IsNullOrEmpty(PasswordRepeat.Password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                _validForm = false;
            }
            else if (!Regex.IsMatch(passwordBox.Password, "^(?=.*\\d)(?=.*[a-zA-Z]).{6,}$"))
            {
                MessageBox.Show("Введите пароль, состоящий из латиницы, имеющий хотя бы одну цифру и с минимальной длиной - 6");
                PasswordRepeat.Password = "";
                passwordBox.Password = "";
                _validForm = false;
            }
            else if (passwordBox.Password != PasswordRepeat.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                _validForm = false;
            }
            else if (!Regex.IsMatch(Email.Text, pattern))
            {
                MessageBox.Show("Введенный Email, не соответсвует правильному email");
                _validForm = false;
                Email.Text = "";
            }
            else
            {
                _validForm = true;
            }
        }
    }
}
