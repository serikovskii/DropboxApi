namespace Dropbox_ControlWork
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }
        public DbSet<User> Users { get; set; }
    }
}