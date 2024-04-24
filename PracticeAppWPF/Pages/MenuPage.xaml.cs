using System.Windows;
using System.Windows.Controls;

namespace PracticeAppWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private static MenuPage s_instance;
        public static MenuPage Instance => s_instance ?? (s_instance = new MenuPage());
        public MenuPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateToPersonalAccountPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateToMainСategoriesPage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow.CurrentUser = null;
            MainWindow.Candidate = null;
            MainWindow.OrdersFullPrice = 0;
            MainWindow.NavigateToAuthorizationPage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateToAdminPanelPage();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.CurrentUser.Role == 0)
            {
                AdminButton.Visibility = Visibility.Visible;
                return;
            }
            AdminButton.Visibility = Visibility.Hidden;

        }
    }
}
