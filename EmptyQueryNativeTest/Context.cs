using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmptyQueryNativeTest
{
    public class Context : DbContext
    {
        public DbSet<Model> Models { get; set; }
        public DbSet<Child> Children { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=test.db");

            optionsBuilder.ConfigureWarnings(builder => builder.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model>()
                .HasMany(model => model.Children)
                .WithOne(child => child.Parent)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public Model[] GetModels(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return this.Models.Include(model => model.Children).ToArray();
            }

            return this.Models.Include(model => model.Children)
                .Where(model => model.Children.Any(child => child.SomeString.Contains(search) || model.SomeString.Contains(search)))
                .ToArray();
        }
    }
}
