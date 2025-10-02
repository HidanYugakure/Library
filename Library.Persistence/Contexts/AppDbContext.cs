using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Contexts;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=.;database=FidanDb;Trusted_Connection=True;integrated security=true;encrypt=true");
        base.OnConfiguring(optionsBuilder);
    }


    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ReservedItem> ReservedItems { get; set; }
    protected override void OnModelCreating(ModelBuilder b)
    {

        b.Entity<Author>(e =>
        {
            e.Property(x => x.Name).IsRequired().HasMaxLength(128);
            e.Property(x => x.Surname).HasMaxLength(128);
        });


        b.Entity<Book>(e =>
        {
            e.Property(x => x.Name).IsRequired().HasMaxLength(256);

            e.HasOne(x => x.Author)
             .WithMany(a => a.Books)
             .HasForeignKey(x => x.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        b.Entity<User>(e =>
        {
            e.Property(x => x.FirstName).IsRequired().HasMaxLength(64);
            e.Property(x => x.LastName).IsRequired().HasMaxLength(64);
            e.Property(x => x.FinCode).IsRequired().HasMaxLength(16);
            e.HasIndex(x => x.FinCode).IsUnique();
            e.Property(x => x.Password).IsRequired().HasMaxLength(64);
        });


        b.Entity<ReservedItem>(e =>
        {
            e.Property(x => x.StartDate).IsRequired();
            e.Property(x => x.EndDate).IsRequired();

            e.HasOne(x => x.Book)
             .WithMany(bk => bk.ReservedItems)
             .HasForeignKey(x => x.BookId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.User)
             .WithMany(u => u.Reservations)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Restrict);


            e.HasIndex(x => new { x.BookId, x.StartDate, x.EndDate });
            e.HasIndex(x => new { x.UserId, x.Status });
        });
    }
}
