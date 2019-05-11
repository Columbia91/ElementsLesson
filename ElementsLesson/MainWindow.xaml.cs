using DataAccess;
using Services;
using System.Windows;

namespace ElementsLesson
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignInButtonClick(object sender, RoutedEventArgs e)
        {
            var login = loginTextBox.Text;
            var password = passwordBox.Password;

            if(string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(login) || 
                string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            UsersRepository repository = new UsersRepository();
            var user = repository.CheckForAvailability("Login", login);

            if (user != null && DataEncryptor.IsValidPassword(password, user.Password))
            {
                MessageBox.Show("Добро пожаловать!", "Удачный вход", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Пшел вон, шал!", "Неудачный вход", MessageBoxButton.OK, MessageBoxImage.Error);
                loginTextBox.Clear();
                passwordBox.Clear();
            }
        }
    }
}
