using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DataContext>>()))
            {
                if (context == null || context.Users == null)
                {
                    throw new ArgumentNullException("Null DataContext");
                }

                if (context.Users.Any())
                {
                    return; // DB has been seeded
                }


                // new TransferTransactions
                context.Users.AddRange(
                    new User
                    {
                        UserName = "mojtaba",
                        Password = "110",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}