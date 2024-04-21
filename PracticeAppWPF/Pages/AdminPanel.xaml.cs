using System.Windows.Controls;

namespace PracticeAppWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Page
    {
        private static AdminPanel s_instance;
        public static AdminPanel Instance => s_instance ?? (s_instance = new AdminPanel());

        public AdminPanel()
        {
            InitializeComponent();
        }
    }
}
