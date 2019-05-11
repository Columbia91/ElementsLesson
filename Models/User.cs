using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        //********************************************
        [Required]
        [StringLength(24, MinimumLength = 3, ErrorMessage = "Логин должен состоять мин. из 3 символов, макс. 24")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_\.]{2,24}$",
            ErrorMessage = "Логин допускает латинские буквы и может содержать цифры, начинается только на букву")]
        public string Login { get; set; }

        //********************************************
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Имя должно состоять мин. из 1 символа, макс. 50")]
        public string Name { get; set; }

        //********************************************
        public string LastName { get; set; }
        //********************************************
        [Required]
        public string Password { get; set; }
        
        //********************************************
        [Required]
        [RegularExpression(@"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$",
            ErrorMessage = "Почта имеет не корректный адрес. Пример: mailbox@yahoo.com")]
        public string Email { get; set; }

        //********************************************
        [Required]
        [RegularExpression(@"^\+?[7]\d{10}$",
            ErrorMessage = "Телефонный номер не корректный. Пример: +77011010203")]
        public string Phone { get; set; }

        public User()
        {

        }
        public User(string login, string name, string lastName, string password, string email, string phone)
        {
            Login = login; Name = name; LastName = lastName;
            Password = password; Email = email; Phone = phone;
        }
    }
}
