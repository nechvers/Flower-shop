using PracticeAppWPF.Controls.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticeAppWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Trash.xaml
    /// </summary>
    public partial class Orders : Page
    {
        private static Orders s_instance;
        public static Orders Instance => s_instance ?? (s_instance = new Orders());
        public Orders()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            orders.Children.Clear();
            foreach (var order in Database.Trashes)
            {
                var instance = new FlowerCard() { Source = order.Flower };
                instance.Count = order.Count;
                instance.IsToTrashButtonVisible = false;
                orders.Children.Add(instance);
            }
            BuyButton.Content = $"Оформить ({Database.Trashes.Sum(a => a.Count * a.Flower.Cost)})"; ;
        }
    }
}
