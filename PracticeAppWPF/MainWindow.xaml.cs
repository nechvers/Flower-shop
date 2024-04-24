using System.Windows;
using System.Windows.Controls;
using PracticeAppWPF.Pages.DetailedRegistrationPage;
using PracticeAppWPF.Pages.AuthorizationPage;
using PracticeAppWPF.Pages.PersonalAccountPage;
using PracticeAppWPF.Pages.RegistrationPage;
using PracticeAppWPF.Pages;
using System;

namespace PracticeAppWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Frame s_mainFrame;
        public static Staff CurrentUser { get; set; }
        public static Staff Candidate { get; set; }
        public static int OrdersFullPrice
        {
            get => _ordersFullPrice;
            set
            {
                _ordersFullPrice = value;
                OrdersFullPriceChanged?.Invoke(value);
            }
        }
        public static Action<int> OrdersFullPriceChanged;
        private static int _ordersFullPrice = 0;

        public MainWindow()
        {
            InitializeComponent();
            s_mainFrame = MainFrame;

            NavigateToAuthorizationPage();
        }

        public static void NavigateToRegistrationPage()
        {
            s_mainFrame.Navigate(Registration.Instance);
        }
        public static void NavigateToMenuPage()
        {
            s_mainFrame.Navigate(MenuPage.Instance);
        }

        public static void NavigateToOrderBuyPage()
        {
            s_mainFrame.Navigate(OrderBuy.Instance);
        }

        public static void NavigateToOrdersPage()
        {
            s_mainFrame.Navigate(Orders.Instance);
        }

        public static void NavigateToMainСategoriesPage()
        {
            s_mainFrame.Navigate(MainСategories.Instance);
        }

        public static void NavigateToDetailedRegistrationPage()
        {
            s_mainFrame.Navigate(DetailedRegistration.Instance);
        }

        public static void NavigateToAuthorizationPage()
        {
            s_mainFrame.Navigate(Authorization.Instance);
        }

        public static void NavigateToAdminPanelPage()
        {
            s_mainFrame.Navigate(AdminPanel.Instance);
        }

        public static void NavigateToPersonalAccountPage()
        {
            s_mainFrame.Navigate(PersonalAccount.Instance);
        }
    }
}
