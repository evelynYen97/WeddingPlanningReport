using Microsoft.EntityFrameworkCore;

namespace WeddingPlanningReport.Models
{
    public partial class WeddingPlanningContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot Config=new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(Config.GetConnectionString("WeddingPlanning"));
        }
    }
}
