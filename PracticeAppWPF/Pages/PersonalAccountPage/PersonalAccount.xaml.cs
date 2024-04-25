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
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.NavigateToMenuPage();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new PersonalAccountViewModel();
        }
    }
}
