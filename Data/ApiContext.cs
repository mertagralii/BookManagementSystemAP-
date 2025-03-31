using BookManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Data
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQL Server bağlantı dizesini belirtiyoruz (Database ismini hangi isimde açılmasını istiyorsan Database kısmına yaz CodeFirst ile oluşacak zaten hepsi)
            optionsBuilder.UseSqlServer("Server=MERT;Database=ApiBookDb;Integrated Security=true;TrustServerCertificate=True");
        }

        public DbSet<Book> Books { get; set; } // Veritabanında Books adlı tablo oluşturulacak
    }
}

    