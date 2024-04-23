using System;
using System.Windows;
using System.Windows.Controls;

namespace PracticeAppWPF.Controls.Cards
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class FlowerCard : UserControl
    {
        public FlowerCard()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SourceProperty =
                  DependencyProperty.Register(
                        "Source",
                         typeof(Flower),
                         typeof(FlowerCard));
   
        public static readonly DependencyProperty CountProperty =
                  DependencyProperty.Register(
                        "Count",
                         typeof(int),
                         typeof(FlowerCard));

        public Flower Source
        {
            get => (Flower)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        public bool IsToTrashButtonVisible
        {
            set => ToTrash.Visibility =  value ? Visibility.Visible : Visibility.Hidden;
        }

        public event Action Clicked;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Count += 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Count == 0) return;
            Count -= 1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Clicked?.Invoke();
        }
    }
}
