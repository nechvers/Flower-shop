using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
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
        private Staff m_selected;

        public AdminPanel()
        {
            InitializeComponent();
            SelectDivision.ItemsSource = Database.Divisions.ToList();
            SelectPost.ItemsSource = Database.Posts.ToList();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectPersonal.ItemsSource = Database.Staffs.ToList();
        }

        private void SelectPersonal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_selected = (sender as ComboBox).SelectedItem as Staff;
            Surname.Text = m_selected.Surname;
            Name.Text = m_selected.Name;
            Patronymic.Text = m_selected.Patronymic;
            Passport.Text = m_selected.Passport;
            Patronymic.Text = m_selected.Patronymic;
            Email.Text = m_selected.Email;
            Phone.Text = m_selected.NumberPhone;
            Login.Text = m_selected.Login;
            SelectDivision.SelectedItem = m_selected.Division1;
            SelectPost.SelectedItem = m_selected.Post1;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.NavigateToMenuPage();
        }

        private void SaveEditDat_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            m_selected.Surname = Surname.Text;
            m_selected.Name = Name.Text;
            m_selected.Patronymic = Patronymic.Text;
            m_selected.Passport = Passport.Text;
            m_selected.Patronymic = Patronymic.Text;
            m_selected.Email = Email.Text;
            m_selected.NumberPhone = Phone.Text;
            m_selected.Login = Login.Text;
            m_selected.Division = (SelectDivision.SelectedItem as Division).ID;
            m_selected.Post = (SelectPost.SelectedItem as Post).ID;
            Database.Staffs.AddOrUpdate(m_selected);
            Database.SaveChanges();
            MessageBox.Show("Готово");
        }
    }
}
