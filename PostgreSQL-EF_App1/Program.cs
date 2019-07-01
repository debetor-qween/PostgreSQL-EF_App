using System;
using Microsoft.EntityFrameworkCore;

// Приложение для изучения технологии доступа к БД PostgreSQL через EF
namespace PostgreSQL_EF_App1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime, endTime;
            ApplicationContext db = new ApplicationContext();

            startTime = DateTime.Now;
            User user1 = new User { Name = "Novice", Login = "NoviceLogin", Pwd = "NovicePwd" };
            db.Users.Add(user1);
            db.SaveChanges();
            endTime = DateTime.Now;
            Console.WriteLine($"Время выполнения INSERT: {((endTime - startTime)).TotalMilliseconds}");


            DbSet<User> users = db.Users;
            startTime = DateTime.Now;
            Console.WriteLine("\nUsers list:");
            foreach (User u in users)
            {
                Console.WriteLine($"Id = {u.Id}. Name:{u.Name}. Login: {u.Login}");
            }
            endTime = DateTime.Now;
            Console.WriteLine($"Время выполнения запроса из таблицы User: {((endTime-startTime)).TotalMilliseconds}");

            // Console.ReadLine();

            DbSet<Apartment> apartments = db.Apartments;
            startTime = DateTime.Now;
            Console.WriteLine("\nOriginal Apartments list:");
            foreach (Apartment a in apartments)
            {
                Console.WriteLine($"Id = {a.Id}. Name:{a.Name}. Parent: {(a.Parent_id)}");
            }
            endTime = DateTime.Now;
            Console.WriteLine($"Время выполнения запроса из таблицы Apartment: {((endTime - startTime)).TotalMilliseconds}");

            Console.ReadLine();


        }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Apartment> Apartments { get; set; }

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

    public class Apartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent_id { get; set; }
    }

}
