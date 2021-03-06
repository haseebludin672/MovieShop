using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    { //injection dbcontext options
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {

        }
        //stablishing the relation ship using DBset , 
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
       



        //fluent API CONSTRAINS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
            modelBuilder.Entity<Role>(ConfigureRole);
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> modelbuilder)
        {
            modelbuilder.ToTable("UserRole");
            modelbuilder.HasKey(ur =>new { ur.UserId, ur.RoleId });
            modelbuilder.HasOne(ur => ur.User).WithMany(ur => ur.UserRoles).HasForeignKey(ur => ur.UserId);
            modelbuilder.HasOne(ur => ur.Role).WithMany(ur => ur.UserRoles).HasForeignKey(ur => ur.RoleId);

        }

        private void ConfigureRole(EntityTypeBuilder<Role> modelbuilder)
        {
            modelbuilder.ToTable("Role");
            modelbuilder.HasKey(ro=>ro.Id);
            modelbuilder.Property(ro => ro.Name).HasMaxLength(20);
        }

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> modelbuilder)
        {
            modelbuilder.ToTable("Favorite");
            modelbuilder.HasKey(f => f.Id);

            modelbuilder.HasOne(f => f.Movie).WithMany(f => f.Favorites).HasForeignKey(f => f.MovieId);
            modelbuilder.HasOne(f=> f.User).WithMany(f=> f.Favorite).HasForeignKey(f => f.UserId);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> modelbuilder)
        {
            modelbuilder.ToTable("Review");
            modelbuilder.HasKey(r => new { r.MovieId, r.UserId });
            modelbuilder.Property(r => r.Rating).HasPrecision(3, 2);
            modelbuilder.HasOne(r => r.Movie).WithMany(r => r.Reviews).HasForeignKey(r => r.MovieId);
            modelbuilder.HasOne(r => r.User).WithMany(r => r.Reviews).HasForeignKey(r => r.UserId);
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> modelbuilder)
        {
            modelbuilder.ToTable("Purchase");
            modelbuilder.HasKey(p => p.Id);
            modelbuilder.Property(p => p.TotalPrice).HasPrecision(18, 2);
            modelbuilder.Property(p => p.PurchaseDateTime).HasPrecision(7);

            modelbuilder.HasOne(p => p.Movie).WithMany(p => p.Purchases).HasForeignKey(p => p.MovieId);
            modelbuilder.HasOne(p => p.User).WithMany(p => p.Purchases).HasForeignKey(p => p.UserId);

        }
        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.FirstName).HasMaxLength(128);
            builder.Property(u => u.LastName).HasMaxLength(128);
            builder.Property(u => u.HashedPassword).HasMaxLength(1024);
            builder.Property(u => u.PhoneNumber).HasMaxLength(16);
            builder.Property(u => u.Salt).HasMaxLength(1024);
            builder.Property(u => u.ProfilePictureUrl).HasMaxLength(4096);
            builder.Property(u => u.IsLocked).HasDefaultValue(false);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> modelbuilder)
        {
            modelbuilder.ToTable("MovieCast");
            modelbuilder.HasKey(mc => new {mc.CastId, mc.MovieId, mc.Character });
            modelbuilder.HasOne(mc => mc.Movie).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.MovieId);
            modelbuilder.HasOne(mc => mc.Cast).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.CastId);
        }   

        private void ConfigureCast(EntityTypeBuilder<Cast> modelBuilder)
        {
            modelBuilder.ToTable("Cast");
            modelBuilder.HasKey(c => c.Id);

            modelBuilder.Property(c => c.Name).HasMaxLength(128);
            modelBuilder.Property(c => c.Gender).HasMaxLength(64);
            modelBuilder.Property(c => c.ProfilePath).HasMaxLength(2084);
            modelBuilder.Property(c => c.tmdbUrl).HasMaxLength(2084);


        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> modelBuilder)
        {
            modelBuilder.ToTable("MovieGenre");
            modelBuilder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.HasOne(m => m.Movie).WithMany(m => m.Genres).HasForeignKey(m => m.MovieId);
            modelBuilder.HasOne(m => m.Genre).WithMany(m => m.Movies).HasForeignKey(m => m.GenreId);
        }
    //    modelBuilder.ToTable("MovieGenre");
    //        modelBuilder.HasKey(mg => new { mg.MovieId, mg.GenreId
    //});
    //        modelBuilder.HasOne(m => m.Movie).WithMany(m => m.Genres).HasForeignKey(m => m.MovieId);
    //modelBuilder.HasOne(m => m.Genre).WithMany(m => m.Movies).HasForeignKey(m => m.GenreId);

    private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t=>t.TrailerUrl).HasMaxLength(2048);
            builder.Property(t=>t.Name).HasMaxLength(256);

        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {//specify the rules for movie entity
            //using fluent API
            builder.ToTable("Movie");
            builder.HasKey(m=>m.Id);

            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);

            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);

            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            // Ignore means here that dont create the column in DB just create the property for UI
            builder.Ignore(m => m.Rating);
        }
    }
}
