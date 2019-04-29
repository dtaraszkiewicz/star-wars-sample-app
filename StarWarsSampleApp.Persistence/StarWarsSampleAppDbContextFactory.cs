using Microsoft.EntityFrameworkCore;
using StarWarsSampleApp.Persistence.Infrastructure;

namespace StarWarsSampleApp.Persistence
{
    public class StarWarsSampleAppDbContextFactory : DesignTimeDbContextFactoryBase<StarWarsSampleAppDbContext>
    {
        protected override StarWarsSampleAppDbContext CreateNewInstance(DbContextOptions<StarWarsSampleAppDbContext> options)
        {
            return new StarWarsSampleAppDbContext(options);
        }
    }
}
