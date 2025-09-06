using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Infrastructure.Data
{
    public class NiaDbContextFactory : IDesignTimeDbContextFactory<NiaDbContext>
    {
        public NiaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NiaDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=aspnia_db;Username=postgres;Password=100676").UseSnakeCaseNamingConvention(); // or UseNpgsql, etc.



            return new NiaDbContext(optionsBuilder.Options);
        }
    }
}
