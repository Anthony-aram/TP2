using BO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Tp1Module6.Data
{
    public class Tp1Module6Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Tp1Module6Context() : base("name=Tp1Module6Context")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Supprime les "s" à la fin de chaque table
            // modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            // modelBuilder.Entity<Samourai>().HasOptional(a => a.Arme);
            modelBuilder.Entity<Samourai>().HasMany(a => a.ArtMartiaux).WithMany();
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<BO.Samourai> Samourais { get; set; }

        public System.Data.Entity.DbSet<BO.Arme> Armes { get; set; }

        public System.Data.Entity.DbSet<BO.ArtMartial> ArtMartials { get; set; }
    }
}
