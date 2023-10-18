using Microsoft.EntityFrameworkCore;
using WebApiCast.Entities;

namespace WebApiCast
{
    public class DataContext : DbContext
    {
        public DataContext() : base(GetOptions("Data Source=MAURICIO\\SQLEXPRESS;Initial Catalog=WebApiCastDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True")) 
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public DbSet<Conta> Contas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContaMap());
        }
    }
}
