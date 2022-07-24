using Microsoft.EntityFrameworkCore;

namespace OCASAPI.Infrastructure.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}
    }
}