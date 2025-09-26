namespace DoYouOwnIt.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.CoverImageURL)
                .IsRequired(false); // or true if it should be required

            modelBuilder.Entity<ProductCategory>()
                .Property(p => p.Slug)
                .HasConversion<string>(); // Simple conversion if needed
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
