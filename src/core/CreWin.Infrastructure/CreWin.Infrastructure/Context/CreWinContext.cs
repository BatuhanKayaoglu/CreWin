using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CreWin.Entity.Models;
using CreWin.Entity.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CreWin.Infrastructure.Context
{
    public class CreWinContext : IdentityDbContext<AppUser, AppRole, string>
    {
        private readonly IConfiguration _configuration;
        public const string DEFAULT_SCHEMA = "dbo";

        public CreWinContext()
        {

        }

        public CreWinContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = "Server=DESKTOP-3MBBMR2\\SQLEXPRESS;Initial Catalog=CreWin;MultipleActiveResultSets=True;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connStr);
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder); // We need to add this line because we inherit from IdentityDbContext. If we are not using IdentityDbContext we do not need to add this line.
        }


        //THIS WILL WORK BEFORE ALL MY SAVE CHANGES CALLS.
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        private void PrepareAddedEntites(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.CreateDate == DateTime.MinValue)
                    entity.CreateDate = DateTime.Now;
            }
        }

    }
}
