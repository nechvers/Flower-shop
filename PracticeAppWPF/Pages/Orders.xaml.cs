using PracticeAppWPF.Controls.Cards;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PracticeAppWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Trash.xaml
    /// </summary>
    public partial class Orders : Page
    {
        private static Orders s_instance;
        public static Orders Instance => s_instance ?? (s_instance = new Orders());
        private readonly List<(FlowerCard, Trash)> _changed = new List<(FlowerCard, Trash)>();
        public Orders()
        {
            InitializeComponent();
            BuyButton.Content = $"Оформить ({MainWindow.OrdersFullPrice})";
            MainWindow.OrdersFullPriceChanged += OnOrdersFullPriceChanged;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            orders.Children.Clear();
            foreach (var order in Database.Trashes.Where(a => a.ID_User == MainWindow.CurrentUser.ID))
            {
                var instance = new FlowerCard() { Source = order.Flower };
                instance.Count = order.Count;
                instance.IsToTrashButtonVisible = false;
                instance.CountChanged += () => OnCountChanged(instance ,order);
                orders.Children.Add(instance);
            }
        }

        private void OnCountChanged(FlowerCard card, Trash order)
        {
            var sum = 0;
            foreach (FlowerCard trash in orders.Children)
            {
                sum += trash.Price;
            }
            _changed.Add((card, order));
            MainWindow.OrdersFullPrice = sum;
        }

        private void OnOrdersFullPriceChanged(int value)
        {
            BuyButton.Content = $"Оформить ({value})";
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            foreach (var trash in _changed)
            {
                trash.Item2.Count = trash.Item1.Count;
                Database.Trashes.AddOrUpdate(trash.Item2);
            }
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateToOrderBuyPage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateToMainСategoriesPage();
        }
    }
}
