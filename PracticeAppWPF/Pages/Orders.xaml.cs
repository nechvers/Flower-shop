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
        private practiceEntities context;
        public Orders()
        {
            InitializeComponent();
            BuyButton.Content = $"Оформить ({MainWindow.OrdersFullPrice})";
            MainWindow.OrdersFullPriceChanged += OnOrdersFullPriceChanged;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            context = new practiceEntities();
            orders.Children.Clear();

            foreach (var order in context.Trashes.Where(a => a.ID_User == MainWindow.CurrentUser.ID))
            {
                var instance = new FlowerCard() { Source = order.Flower };
                instance.Count = order.Count;
                instance.IsToTrashButtonVisible = false;
                instance.CountChanged += () => OnCountChanged(instance, order);
                orders.Children.Add(instance);
            }

            
        }

        private void OnCountChanged(FlowerCard card, Trash order)
        {
            if (card.Count == 0)
            {
                var result = MessageBox.Show("Вы действительно хотите удалить товар из корзины?", "", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    orders.Children.Remove(card);
                    context.Trashes.Remove(order);
                    _changed.Remove((card, order));
                    context.SaveChanges();

                    CalculateCost();
                    return;
                }

                card.Count = order.Count;
                CalculateCost();
                return;
            }
           
            _changed.Add((card, order));
            CalculateCost();


        }

        private void CalculateCost()
        {
            var sum = 0;
            foreach (FlowerCard trash in orders.Children)
            {
                sum += trash.Price;
            }
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
                if (trash.Item1.Count < 1) continue;
                trash.Item2.Count = trash.Item1.Count;
                context.Trashes.AddOrUpdate(trash.Item2);
                context.SaveChanges();
            }
            context.Dispose();
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
