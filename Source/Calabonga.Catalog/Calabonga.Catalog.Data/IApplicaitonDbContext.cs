using Calabonga.Catalog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Calabonga.Catalog.Data
{
    /// <summary>
    /// Abstraction for Database (EntityFramework)
    /// </summary>
    public interface IApplicationDbContext
    {

        #region Bussiness Enitities

        DbSet<Category> Categories { get; set; }

        DbSet<Product> Products { get; set; }
        
        #endregion

        #region System

        DbSet<Log> Logs { get; set; }

        DbSet<ApplicationUser> Users { get; set; }

        DbSet<ApplicationUserProfile> Profiles { get; set; }

        DatabaseFacade Database { get; }

        ChangeTracker ChangeTracker { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbQuery<TQuery> Query<TQuery>() where TQuery : class;

        int SaveChanges();

        #endregion
    }
}