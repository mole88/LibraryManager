using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Model
{
    public class LibraryDbContext : DbContext
    {
        private string DBConnectionString;
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<LibraryTransaction> Transactions { get; set; }
        public LibraryDbContext(string connectionString)
        {
            DBConnectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(DBConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateAuthors(modelBuilder);
            CreateBooks(modelBuilder);
            CreateVisitors(modelBuilder);
            CreateTransactions(modelBuilder);
        }
        private void CreateBooks(ModelBuilder mb)
        {
            mb.Entity<Book>(entity =>
            {
                entity.ToTable("books");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id");

                entity.Property(b => b.Name)
                      .HasColumnName("name")
                      .IsRequired();

                entity.Property(b => b.Year)
                      .HasColumnName("year")
                      .IsRequired();

                entity.Property(b => b.AuthorId)
                    .HasColumnName("author_id")
                    .IsRequired();

                entity.Property(b => b.IsAvailable)
                    .HasColumnName("is_available")
                    .IsRequired();

                entity.Property(t => t.CreationDate)
                      .HasColumnName("creation_date")
                      .IsRequired()
                      .HasConversion(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc).ToLocalTime());

                entity.HasOne(b => b.BookAuthor)
                  .WithMany(a => a.Books)
                  .HasForeignKey(b => b.AuthorId);
            });
        }
        private void CreateAuthors(ModelBuilder mb)
        {
            mb.Entity<Author>(entity =>
            {
                entity.ToTable("authors");

                entity.HasKey(a => a.Id);

                entity.Property(a => a.Id)
                    .HasColumnName("id");

                entity.Property(a => a.FullName)
                    .HasColumnName("fullname")
                    .IsRequired();

                entity.Property(t => t.CreationDate)
                      .HasColumnName("creation_date")
                      .IsRequired()
                      .HasConversion(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc).ToLocalTime());
            });
        }
        private void CreateVisitors(ModelBuilder mb)
        {
            mb.Entity<Visitor>(entity =>
            {
                entity.ToTable("visitors");

                entity.HasKey(v => v.Id);
                entity.Property(v => v.Id)
                    .HasColumnName("id");

                entity.Property(v => v.FullName)
                    .HasColumnName("fullname")
                    .IsRequired();

                entity.Property(v => v.Age)
                    .HasColumnName("age")
                    .IsRequired();

                entity.Property(v => v.PhoneNumber)
                    .HasColumnName("phone_number")
                    .IsRequired();

                entity.Property(t => t.CreationDate)
                      .HasColumnName("creation_date")
                      .IsRequired()
                      .HasConversion(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc).ToLocalTime());
            });
        }
        private void CreateTransactions(ModelBuilder mb)
        {
            mb.Entity<LibraryTransaction>(entity =>
            {
                entity.ToTable("transactions");

                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id)
                      .HasColumnName("id");

                entity.Property(t => t.DateTaken)
                      .HasColumnName("date_taken")
                      .IsRequired()
                      .HasConversion(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc).ToLocalTime());

                entity.Property(t => t.DueDate)
                      .HasColumnName("due_date")
                      .IsRequired()
                      .HasConversion(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc).ToLocalTime());

                entity.Property(t => t.ReturnDate)
                    .HasColumnName("date_return")
                    .HasConversion(
                       v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null,
                       v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc).ToLocalTime() : (DateTime?)null);

                entity.Property(t => t.BookId)
                      .HasColumnName("book_id")
                      .IsRequired();

                entity.Property(t => t.VisitorId)
                      .HasColumnName("visitor_id")
                      .IsRequired();

                entity.HasOne(t => t.Visitor)
                  .WithMany(v => v.Transactions)
                  .HasForeignKey(t => t.VisitorId);

                entity.HasOne(t => t.Book)
                  .WithMany(b => b.Transactions)
                  .HasForeignKey(t => t.BookId);
            });
        }
    }
}
