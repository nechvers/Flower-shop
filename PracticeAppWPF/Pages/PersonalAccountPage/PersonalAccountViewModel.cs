using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Bcrypt = BCrypt.Net.BCrypt;

namespace PracticeAppWPF.Pages.PersonalAccountPage
{
    public class PersonalAccountViewModel : INotifyPropertyChanged
    {
        private Staff user => MainWindow.CurrentUser;
        private string m_password;
        private BitmapImage m_image;
        private Command m_saveCommand;
        private Command m_loadCommand;

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

        public string FIO => $"{user.Surname} {user.Name} {user.Patronymic}";

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

        public BitmapImage Image
        {
            get => m_image;
            set
            { 
                m_image = value;
                OnPropertyChanged("Image");
            }
        }

        public Command SaveCommand
        {
            get => m_saveCommand ?? (m_saveCommand = new Command(TrySave));
        }

        public Command LoadCommand
        {
            get => m_loadCommand ?? (m_loadCommand = new Command(ChooseImage));
        }

        public PersonalAccountViewModel()
        {
            string imageDir = string.Empty;
            if (string.IsNullOrEmpty(user.Photo))
                imageDir = "pack://application:,,,/Resources/Images/Icons/istockphoto-1300845620-612x612.jpg";
            else
                imageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", user.Photo);

            Image = new BitmapImage(new Uri(imageDir));
        }

        private void TrySave(object parameter)
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

            var avatar = Image.UriSource.ToString().Split('/');
            staff.Photo = avatar[avatar.Length - 1];

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

            MessageBox.Show("Готово");
        }

        private void ChooseImage(object parameter)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            op.ShowDialog();
            if (string.IsNullOrEmpty(op.FileName)) return;

            string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            if (!Directory.Exists(imagesDir)) 
                Directory.CreateDirectory(imagesDir);

            string filename = Path.Combine(imagesDir, $"{Guid.NewGuid()}.{op.FileName.Split('.')[1]}");
            File.Copy(op.FileName, filename, true);
            Image = new BitmapImage(new Uri(filename));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
