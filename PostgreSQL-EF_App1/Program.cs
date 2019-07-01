using System;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL_EF_App1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime, endTime;
            ApplicationContext db = new ApplicationContext();
            DbSet<User> users = db.Users;
            startTime = DateTime.Now;
            Console.WriteLine("Original Users list:");
            foreach (User u in users)
            {
                Console.WriteLine($"Id = {u.Id}. Name:{u.Name}. Login: {u.Login}");
            }
            endTime = DateTime.Now;
            Console.WriteLine($"Время выполнения запроса: {((endTime-startTime)).TotalMilliseconds}");

            Console.ReadLine();

            User user1 = new User { Name = "Novice", Login = "NoviceLogin", Pwd = "NovicePwd" };
            db.Users.Add(user1);
            db.SaveChanges();

            startTime = DateTime.Now;
            Console.WriteLine("Updated Users list:");
            foreach (User u in users)
            {
                Console.WriteLine($"Id = {u.Id}. Name:{u.Name}. Login: {u.Login}");
            }
            endTime = DateTime.Now;
            Console.WriteLine($"Время выполнения запроса: {((endTime - startTime)).TotalMilliseconds}");

            Console.ReadLine();

        }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Test1DB;Username=postgres;Password=73014492");
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }

    }
}
