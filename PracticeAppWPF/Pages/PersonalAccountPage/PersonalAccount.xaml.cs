using System.Windows.Controls;

namespace PracticeAppWPF.Pages.PersonalAccountPage
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccount.xaml
    /// </summary>
    public partial class PersonalAccount : Page
    {
        private static PersonalAccount s_instance;
        public static PersonalAccount Instance => s_instance ?? (s_instance = new PersonalAccount());

        public PersonalAccount()
        {
            InitializeComponent();
            DataContext = new PersonalAccountViewModel();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.NavigateToMenuPage();
        }
    }
}
