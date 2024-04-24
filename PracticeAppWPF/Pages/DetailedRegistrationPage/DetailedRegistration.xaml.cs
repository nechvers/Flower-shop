using System.Windows;
using System.Windows.Controls;

namespace PracticeAppWPF.Pages.DetailedRegistrationPage
{
    /// <summary>
    /// Логика взаимодействия для DetailedRegistration.xaml
    /// </summary>
    public partial class DetailedRegistration : Page
    {
        private static DetailedRegistration s_instance;
        public static DetailedRegistration Instance => s_instance ?? (s_instance = new DetailedRegistration());
        public DetailedRegistration()
        {
            InitializeComponent();
            DataContext = new DetailedRegistrationViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateToRegistrationPage();
        }
    }
}
