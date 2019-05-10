using Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Name = nameTextBox.Text;
            user.LastName = lastNameTextBox.Text;
            user.Login = loginTextBox.Text;
            user.Password = passwordBox.Password;
            string confirmPassword = confirmPasswordBox.Password;
            user.Email = emailTextBox.Text;
            user.Phone = phoneTextBox.Text;

            Label[] labels = new Label[]
            {
                nameLabel, loginLabel, passwordLabel, confirmPasswordLabel, emailLabel, phoneLabel
            };

            string[] properties = 
                { "Имя", "Логин", "Пароль", "Пароли", "Почта", "Номер",
                    "Name", "Login", "Password", "Passwords", "Email", "Phone"};
            
            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);

            string passwordPattern = @"(?=^.{6,32}$)((?=.*\d)(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
            
            if (user.Password != confirmPassword || confirmPassword.Length == 0)
                results.Add(new ValidationResult("Пароли не совпадают"));
            if (user.Password.Length < 6 || user.Password.Length > 32)
                results.Add(new ValidationResult("Пароль должен состоять мин. из 6 символов и макс. из 32 символов"));
            else if (!(Regex.IsMatch(user.Password, passwordPattern)))
                results.Add(new ValidationResult("Пароль должен содержать цифровой и спец символы, а также буквы верхнего, нижнего регистра"));
            
            if (!Validator.TryValidateObject(user, context, results, true) || results.Count > 0)
            {
                for (int i = 0; i < labels.Length; i++)
                {
                    foreach (var result in results)
                    {
                        var converter = new System.Windows.Media.BrushConverter();

                        if (result.ErrorMessage.Contains(properties[i]) ||
                            result.ErrorMessage.Contains(properties[i + labels.Length]))
                        {
                            labels[i].Content = result.ErrorMessage;
                            var brush = (Brush)converter.ConvertFromString("Red");
                            labels[i].Background = brush;
                            break;
                        }
                        else
                        {
                            var brush = (Brush)converter.ConvertFromString("Green");
                            labels[i].Background = brush;
                            labels[i].Content = "Успешно";
                        }
                    }
                }
            }
            else
            {

            }
        }
        
        private void lastNameTextBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("Green");
            lastNameLabel.Background = brush;
            lastNameLabel.Content = "Не обязателен к заполнению";
        }

        private void lastNameTextBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#FF46B69D");
            lastNameLabel.Background = brush;
            lastNameLabel.Content = "";
        }
    }
}
