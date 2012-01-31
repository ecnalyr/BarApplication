using System.Data.Entity;

namespace BarApp.Models
{
    public class BarAppEntities : DbContext
    {
        public DbSet<Drinks> Drink { get; set; }
        public DbSet<Establishments> Establishment { get; set; }
        public DbSet<Achievements> Achievement { get; set; }
        public DbSet<Parties> Party { get; set; }
        public DbSet<Persons> Person { get; set; }
        public DbSet<Profiles> Profile { get; set; }
        public DbSet<Promotions> Promotion { get; set; }
        public DbSet<Transactions> Transaction { get; set; }
    }

}