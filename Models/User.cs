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
            ErrorMessage = "Логин должен начинаться на букву, а также может содержать цифры")]
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
            ErrorMessage = "Почта имеет некорректный адрес.")]
        public string Email { get; set; }

        //********************************************
        [Required]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$",
            ErrorMessage = "Номер некорректный.")]
        public string Phone { get; set; }
    }
}
