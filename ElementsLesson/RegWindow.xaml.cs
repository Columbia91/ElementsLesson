using DataAccess;
using Models;
using Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace ElementsLesson
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(
                loginTextBox.Text, nameTextBox.Text, lastNameTextBox.Text,
                passwordBox.Password, emailTextBox.Text, phoneTextBox.Text);
            string confirmPassword = confirmPasswordBox.Password;

            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);

            SortedList<string, object> verifiableProperties = new SortedList<string, object> {
                { "Login", user.Login },
                { "Email", user.Email },
                { "Phone", user.Phone }
            };

            UsersRepository repository = new UsersRepository();
            foreach (var property in verifiableProperties)
            {
                if (repository.CheckForAvailability(property.Key, property.Value) != null)
                    results.Add(new ValidationResult(property.Key + " уже занят(-а)"));
            }

            Label[] labels = new Label[]
            {
                nameLabel, loginLabel, passwordLabel, confirmPasswordLabel, emailLabel, phoneLabel
            };

            TextBox[] textBoxes = new TextBox[]
            {
                nameToolTextBox, loginToolTextBox, passwordToolTextBox, confirmPasswordToolTextBox, emailToolTextBox, phoneToolTextBox
            };

            string[] properties = 
                { "Имя", "Логин", "Пароль", "Пароли", "Почта", "Телефонный номер",
                    "Name", "Login", "Password", "Passwords", "Email", "Phone"};
            
            string passwordPattern = @"(?=^.{6,32}$)((?=.*\d)(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
            
            if (user.Password != confirmPassword || confirmPassword.Length == 0)
                results.Add(new ValidationResult("Пароли не совпадают"));
            if (user.Password.Length < 6 || user.Password.Length > 32)
                results.Add(new ValidationResult("Пароль должен состоять мин. из 6 символов и макс. из 32 символов"));
            else if (!(Regex.IsMatch(user.Password, passwordPattern)))
                results.Add(new ValidationResult("Пароль должен содержать цифровой и спец символы, а также буквы верхнего, нижнего регистра"));

            var converter = new System.Windows.Media.BrushConverter();

            if (!Validator.TryValidateObject(user, context, results, true) || results.Count > 0)
            {
                for (int i = 0; i < labels.Length; i++)
                {
                    foreach (var result in results)
                    {
                        if (result.ErrorMessage.Contains(properties[i]) ||
                            result.ErrorMessage.Contains(properties[i + labels.Length]))
                        {
                            textBoxes[i].Text = result.ErrorMessage;
                            labels[i].Content = result.ErrorMessage;
                            var brush = (Brush)converter.ConvertFromString("Red");
                            labels[i].Background = brush;
                            break;
                        }
                        else
                        {
                            var brush = (Brush)converter.ConvertFromString("LightBlue");
                            labels[i].Background = brush;
                            textBoxes[i].Text = "Одобрено";
                            labels[i].Content = "Одобрено";
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < labels.Length; i++)
                {
                    var brush = (Brush)converter.ConvertFromString("Green");
                    labels[i].Background = brush;
                    labels[i].Content = "Успешно";
                    if (lastNameTextBox.Text.Length == 0)
                        user.LastName = null;
                    user.Password = DataEncryptor.HashPassword(confirmPassword);
                    repository.Insert(user);
                    Close();
                }
            }
        }
        
        private void lastNameTextBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("Green");
            lastNameLabel.Background = brush;
            lastNameLabel.Opacity = 0.5;
            lastNameLabel.Content = "Не обязателен к заполнению";
        }

        private void lastNameTextBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("LightBlue");
            lastNameLabel.Background = brush;
            lastNameLabel.Opacity = 0;
            lastNameLabel.Content = "";
        }
    }
}
