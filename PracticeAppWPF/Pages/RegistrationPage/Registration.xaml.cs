using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace PracticeAppWPF.Pages.RegistrationPage
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private static Registration s_instance;
        public static Registration Instance => s_instance ?? (s_instance = new Registration());


        public bool _validForm = false;
        public string _fullPathPhoto;
        public Registration()
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel();
        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                string destinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", Path.GetFileName(imagePath));

                File.Copy(imagePath, destinationPath); // Изображение скопировано в папку "debug/Images"

                _fullPathPhoto = destinationPath;
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            
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

        private void TextBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*TextBox textBox = (TextBox)sender;
            string phoneNumber = textBox.Text;

            string digitsOnly = Regex.Replace(phoneNumber, @"[^\d]", "");

            if (digitsOnly.Length >= 11 && digitsOnly[0] == '8')
            {
                textBox.Text = "8" + digitsOnly.Substring(1, 10);
            }
            else if (digitsOnly.Length >= 11 && digitsOnly.StartsWith("7"))
            {
                textBox.Text = "+7" + digitsOnly.Substring(1, 10);
            }
            else
            {
                // Некорректный формат номера
                MessageBox.Show("Некорректный формат номера");
                TextBoxPhone.Text = " ";
            }*/
        }


        //       -----Методы-----

    }
}
