using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdventureWorksSandbox.Models
{
    public class AdventureWorksSandboxContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<AdventureWorksSandbox.Models.AdventureWorksSandboxContext>());

        public AdventureWorksSandboxContext() : base("name=AdventureWorksSandboxContext2")
        {
        }
        
        public DbSet<AdventureWorksSandbox.Models.Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<AdventureWorksSandboxContext>(null);
            modelBuilder.Entity<BusinessEntity>().ToTable("BusinessEntity", "Person");
            modelBuilder.Entity<Person>().ToTable("Person", "Person").HasKey(p => p.BusinessEntityId);
        }

        public DbSet<AdventureWorksSandbox.Models.BusinessEntity> BusinessEntities { get; set; }
    }
}