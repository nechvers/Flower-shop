
using PracticeAppWPF.Pages.OrderBuyPage;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;

namespace PracticeAppWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderBuy.xaml
    /// </summary>
    public partial class OrderBuy : Page
    {
        private static OrderBuy s_instance;
        public static OrderBuy Instance => s_instance ?? (s_instance = new OrderBuy());
        public Random Random = new Random();

        public OrderBuy()
        {
            InitializeComponent();
            BuyButton.Content = $"Оформить ({MainWindow.OrdersFullPrice})";
            MainWindow.OrdersFullPriceChanged += OnOrdersFullPriceChanged;
        }

        private void OnOrdersFullPriceChanged(int value)
        {
            BuyButton.Content = $"Оформить ({value})";
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new OrderBuyViewModel();
            Cost.Content = MainWindow.OrdersFullPrice.ToString();
            FinalCost.Content = MainWindow.OrdersFullPrice.ToString();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GenerateContract();
        }

        private void GenerateContract()
        {
            Word.Application wApp = new Word.Application();
            Word.Documents wDocs = wApp.Documents;

            Word.Document wDoc = wDocs.Open(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "template.docx"), ReadOnly: false);
            wDoc.Activate();

            Word.Bookmarks Bookmarks = wDoc.Bookmarks;

            var user = MainWindow.CurrentUser;
            var sum = MainWindow.OrdersFullPrice;
            var rndNumber = Random.Next(0, 100);

            Bookmarks["НомерЧека"].Range.Text = $"{rndNumber}";
            Bookmarks["Покупатель"].Range.Text = $"{user.Surname} {user.Name} {user.Patronymic}";
            Bookmarks["Сумма"].Range.Text = $"{sum} руб.";
            Bookmarks["Скидка"].Range.Text =  $"{0} руб.";
            Bookmarks["Итог"].Range.Text = $"{sum} руб.";
    

            wDoc.SaveAs2(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), $"Чек №{rndNumber}.docx"));
            wDoc.Close();
            wApp.Quit();
            MessageBox.Show("Готово");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.NavigateToOrdersPage();
        }
    }
}
