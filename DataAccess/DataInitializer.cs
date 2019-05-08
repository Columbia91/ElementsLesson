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
                Password = DataEncryptor.HashPassword("123")
            });
        }
    }
}
