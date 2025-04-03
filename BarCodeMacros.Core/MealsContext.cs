using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BarCodeMacrosUI")]


namespace BarCodeMacros
{
    public class MealsContext : DbContext
    {
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<FoodProduct> FoodProducts { get; set; }

        public MealsContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=Meals.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodProduct>().OwnsOne(
                p => p.NutritionPer100g,
                n =>
                {
                    n.Property(n => n.EnergyKcal).HasColumnName("EnergyKcal");
                    n.Property(n => n.Proteins).HasColumnName("Proteins");
                    n.Property(n => n.Carbohydrates).HasColumnName("Carbohydrates");
                    n.Property(n => n.Fat).HasColumnName("Fat");
                    n.Property(n => n.Fiber).HasColumnName("Fiber");
                    n.Property(n => n.Salt).HasColumnName("Salt");
                });

            modelBuilder.Entity<Ingredient>()
                .HasOne(i => i.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}