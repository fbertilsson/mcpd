using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace HistoricEntitiesCodeFirst
{
    public class HistoricEventContext : DbContext
    {
        public HistoricEventContext()
        {
            // Do nothing.
        }

        public HistoricEventContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            // Do nothing.
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Tag>().HasMany(m => m.Children).WithMany();
            //modelBuilder.Entity<Tag>().HasMany<Tag>(m => m.Children).WithMany();
            modelBuilder.Entity<Tag>().HasOptional<Tag>(t => t.Parent).WithMany(p => p.Children);
            //modelBuilder.Entity<Tag>().HasMany(tag => tag.Children).WithRequired(child => child.Parent);


            modelBuilder.Entity<HistoricEvent>().HasMany<Tag>(historicEvent => historicEvent.Tags).WithMany(t => t.HistoricEvents);
            //modelBuilder.Entity<Tag>().HasOptional(tag => tag.Parent).WithMany();
            //modelBuilder.Entity<Tag>().HasOptional(tag => tag.Parent).WithMany(parent => parent.Children);

            //modelBuilder.Entity<HistoricEvent>().HasOptional<TimeRef>(e => e.TimeReference);
        }


        public DbSet<HistoricEvent> HistoricEvents { get; set; }
        public DbSet<Tag> Tags { get; set; }
        //public DbSet<TimeRef> TimeRefs { get; set; }

    }
}
