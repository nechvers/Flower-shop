using System.Windows;
using System.Windows.Controls;

namespace PracticeAppWPF.Controls.Card
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

        public static readonly DependencyProperty ImageSourceProperty =
                   DependencyProperty.Register(
                         "ImageSource",
                          typeof(string),
                          typeof(FlowerCard));

        public static readonly DependencyProperty CostProperty =
                  DependencyProperty.Register(
                        "Cost",
                         typeof(string),
                         typeof(FlowerCard));

        public static readonly DependencyProperty HeaderProperty =
                  DependencyProperty.Register(
                        "Header",
                         typeof(string),
                         typeof(FlowerCard));

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public string Cost
        {
            get => (string)GetValue(CostProperty);
            set => SetValue(CostProperty, value);
        }

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
    }
}
