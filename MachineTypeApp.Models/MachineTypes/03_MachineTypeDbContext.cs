using Microsoft.EntityFrameworkCore;

namespace MachineTypeApp.Models.MachineTypes
{
    public class MachineTypeDbContext : DbContext
    {
        // PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer
        // PM> Install-Package Microsoft.Data.SqlClient

        public MachineTypeDbContext()
        {

        }

        public MachineTypeDbContext(DbContextOptions<MachineTypeDbContext> options) 
            : base(options)
        {

        }

        public DbSet<MachineType>  MachineTypes { get; set; }
    }
}
