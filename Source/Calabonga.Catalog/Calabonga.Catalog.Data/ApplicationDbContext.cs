using Calabonga.Catalog.Data.Base;
using Calabonga.Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Catalog.Data
{
    /// <summary>
    /// Database for application
    /// </summary>
    public class ApplicationDbContext : DbContextBase, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        #region Bussiness Entities

        /// <inheritdoc />
        public DbSet<Category> Categories { get; set; }

        /// <inheritdoc />
        public DbSet<Product> Products { get; set; }

        /// <inheritdoc />
        public DbSet<Review> Reviews { get; set; }

        /// <inheritdoc />
        public DbSet<Tag> Tags { get; set; }

        #endregion

        #region System

        /// <inheritdoc />
        public DbSet<Log> Logs { get; set; }

        public DbSet<ApplicationUserProfile> Profiles { get; set; }

        #endregion
    }
}