using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project20180804.Core.Framework;
using Project20180804.Core.Model;

namespace Project20180804.Core.DAL
{
    public class MySqlDbContext : DbContext
    {
        private readonly IOptions<AppSettings> _appSetting;

        public MySqlDbContext(IOptions<AppSettings> appSetting)
        {
            _appSetting = appSetting;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_appSetting.Value.MySqlConnectionString, mysqlOptions =>
            {
               
            });

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductXAttachment> ProductXAttachment { get; set; }
        public DbSet<Video> Video { get; set; }
    }
}
