using Iklian.Core;
using Microsoft.EntityFrameworkCore;

namespace Iklian.Data
{
    public class IklianDbContext : DbContext
    {
        public IklianDbContext(DbContextOptions<IklianDbContext> options) : base(options)
        {

        }

        public DbSet<UrlAlias> UrlAliases { get; set; }
    }
}
