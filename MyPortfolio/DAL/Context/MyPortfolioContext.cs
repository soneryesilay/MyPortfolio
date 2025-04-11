using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Entities;
using MyPortolioUdemy.DAL.Entities;

namespace MyPortfolio.DAL.Context
{
    public class MyPortfolioContext : DbContext
    {
        // Constructor ekleyin
        public MyPortfolioContext(DbContextOptions<MyPortfolioContext> options) : base(options)
        {
        }

        // OnConfiguring metodunu kaldırabilirsiniz veya aşağıdaki gibi düzenleyebilirsiniz
        // Bu metot sadece DbContext doğrudan new ile oluşturulursa çalışır
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=SONER\\SQLEXPRESS;initial Catalog=MyPorfolioDb;integrated Security=true;");
            }
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<SiteSettings> SiteSettings { get; set; }
    }
}