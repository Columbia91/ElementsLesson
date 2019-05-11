using Models;
using System.Linq;
using System;
using System.Windows;
using System.Data.Entity.Infrastructure;

namespace DataAccess
{
    public class UsersRepository
    {
        public User CheckForAvailability(string property, object value)
        {
            using(var context = new SecurityContext())
            {
                return context.Database.SqlQuery<User>
                    ($"Select * from Users Where {property} = '{value}'").FirstOrDefault();
            }
        }
        public void Insert(User user)
        {
            try
            {
                using (var context = new SecurityContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    MessageBox.Show("Успешное добавление", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DbUpdateException exception)
            {
                MessageBox.Show(exception.Message, exception.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
