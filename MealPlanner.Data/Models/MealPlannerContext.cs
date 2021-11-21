using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class MealPlannerContext : IdentityDbContext<ApplicationUser>
    {
        public MealPlannerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<MealAllergen> MealAllergens { get; set; }
        public DbSet<MealIngredient> MealIngredients { get; set; }
        public DbSet<MealImage> MealImages { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SoftMeal> SoftMeals { get; set; }
        public DbSet<SoftMealDetail> SoftMealDetails { get; set; }
        public DbSet<DeliveryLog> DeliveryLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.Entity<ApplicationUser>()
            .HasOne(a => a.Employee)
            .WithOne(e => e.User)
            .HasForeignKey<Employee>(e => e.UserId);

            modelBuilder.Entity<Company>()
            .HasMany(c => c.Employees)
            .WithOne(e => e.Company);

            modelBuilder.Entity<MealAllergen>().HasKey(ma => new { ma.MealId, ma.AllergenId });
            modelBuilder.Entity<MealAllergen>()
                .HasOne(ma => ma.Meal)
                .WithMany(m => m.MealAllergens)
                .HasForeignKey(ma => ma.MealId);
            modelBuilder.Entity<MealAllergen>()
                .HasOne(ma => ma.Allergen)
                .WithMany(a => a.MealAllergens)
                .HasForeignKey(ma => ma.AllergenId);

            modelBuilder.Entity<MealIngredient>().HasKey(mi => new { mi.MealId, mi.IngredientId });
            modelBuilder.Entity<MealIngredient>()
                .HasOne(mi => mi.Meal)
                .WithMany(m => m.MealIngredients)
                .HasForeignKey(mi => mi.MealId);
            modelBuilder.Entity<MealIngredient>()
                .HasOne(mi => mi.Ingredient)
                .WithMany(i => i.MealIngredients)
                .HasForeignKey(mi => mi.IngredientId);

            modelBuilder.Entity<Allergen>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<Ingredient>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<Meal>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(u => u.Rfid)
                .IsUnique();

            modelBuilder.Entity<Employee>()
            .HasMany(e => e.Orders)
            .WithOne(o => o.Employee);

            modelBuilder.Entity<Employee>()
            .HasMany(e => e.SoftMeals)
            .WithOne(o => o.Employee);

            modelBuilder.Entity<Employee>()
            .HasMany(e => e.DeliveryLogs)
            .WithOne(o => o.Employee);

            modelBuilder.Entity<Plan>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Plan);

            modelBuilder.Entity<Company>()
            .HasMany(c => c.Plans)
            .WithOne(p => p.Company);

            modelBuilder.Entity<Meal>()
            .HasMany(m => m.Plans)
            .WithOne(p => p.Meal);

            modelBuilder.Entity<Meal>()
            .HasOne(m => m.MealImage)
            .WithOne(mi => mi.Meal)
            .HasForeignKey<MealImage>(mi => mi.MealId);

            modelBuilder.Entity<SoftMealDetail>()
            .HasMany(e => e.SoftMeals)
            .WithOne(o => o.SoftMealDetail);

            modelBuilder.Entity<Plan>()
                .HasIndex(p => new { p.MealId, p.CompanyId, p.Shifts, p.Date })
                .IsUnique();
        }
    }
}
