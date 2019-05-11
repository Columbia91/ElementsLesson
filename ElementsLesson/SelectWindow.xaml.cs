using System.Windows;

namespace ElementsLesson
{
    /// <summary>
    /// Логика взаимодействия для SelectWindow.xaml
    /// </summary>
    public partial class SelectWindow : Window
    {
        public SelectWindow()
        {
            InitializeComponent();
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegWindow window = new RegWindow();
            window.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
