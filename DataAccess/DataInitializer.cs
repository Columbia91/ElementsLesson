using System.Data.Entity;
using Services;

namespace DataAccess
{
    public class DataInitializer : CreateDatabaseIfNotExists<SecurityContext>
    {
        protected override void Seed(SecurityContext context)
        {
            context.Users.Add(new Models.User
            {
                Login = "admin",
                Name = "Ben",
                Password = DataEncryptor.HashPassword("Qwerty@99"),
                Email = "alpha@mail.ru",
                Phone = "+77011012030"
            });
        }
    }
}
