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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeAppWPF.Controls.Cards
{
    /// <summary>
    /// Логика взаимодействия для OrderCard.xaml
    /// </summary>
    public partial class OrderCard : UserControl
    {
        public OrderCard()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SourceProperty =
                 DependencyProperty.Register(
                       "Source",
                        typeof(Trash),
                        typeof(FlowerCard));

        public Trash Source
        {
            get => (Trash)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
