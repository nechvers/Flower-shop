using System.Windows;
using System.Windows.Controls;

namespace PracticeAppWPF.Pages.AuthorizationPage
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        private static Authorization s_instance;
        public static Authorization Instance => s_instance ?? (s_instance = new Authorization());
        public bool validForm = false;
        public Authorization()
        {
            InitializeComponent();
            DataContext = new AuthorizationViewModel();
            
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateToRegistrationPage();
        }

        private void txtVisiblePasswordbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordBox.Password = txtVisiblePasswordbox.Text;
        }

        private void ChekBoxPassword_Checked(object sender, RoutedEventArgs e)
        {
            passwordBox.Visibility = Visibility.Hidden;
            txtVisiblePasswordbox.Visibility = Visibility.Visible;
            txtVisiblePasswordbox.Text = passwordBox.Password;
        }

        private void ChekBoxPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            txtVisiblePasswordbox.Visibility = Visibility.Hidden;
            passwordBox.Visibility = Visibility.Visible;
        }
    }
}
