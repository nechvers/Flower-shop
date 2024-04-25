using PracticeAppWPF.Controls.Cards;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PracticeAppWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainСategories.xaml
    /// </summary>
    public partial class MainСategories : Page
    {
        private static MainСategories s_instance;
        public static MainСategories Instance => s_instance ?? (s_instance = new MainСategories());
        public MainСategories()
        {
            InitializeComponent();
            OrdersButton.Content = $"Корзина ({MainWindow.OrdersFullPrice})";
            MainWindow.OrdersFullPriceChanged += OnOrdersFullPriceChanged;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            foreach (Flower flower in Database.Flowers)
            {
                var instance = new FlowerCard() { Source = flower };
                instance.Clicked += () => OnCardClicked(instance);
                FlowerCards.Children.Add(instance);
            }

        }

        private void OnOrdersFullPriceChanged(int value)
        {
            OrdersButton.Content = $"Корзина ({value})";
        }

        private void OnCardClicked(FlowerCard card)
        {

            if (card.Count == 0) return;

            var result = Database.Trashes.Where(a => a.ID_User == MainWindow.CurrentUser.ID && a.ID_Flower == card.Source.ID).FirstOrDefault();

            if (result == null)
            {
                result = new Trash()
                {
                    ID_User = MainWindow.CurrentUser.ID,
                    ID_Flower = card.Source.ID,
                    Count = card.Count
                };
            }
            else
            {
                result.Count += card.Count;
            }

            card.Count = 0;
            Database.Trashes.AddOrUpdate(result);
            Database.SaveChanges();
            MainWindow.OrdersFullPrice = Database.Trashes
                .Where(a => a.ID_User == MainWindow.CurrentUser.ID)
                .Sum(a => a.Count * a.Flower.Cost);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateToOrdersPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateToMenuPage();

        }
    }
}
