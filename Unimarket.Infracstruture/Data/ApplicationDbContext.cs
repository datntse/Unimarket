using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;

namespace Unimarket.Infracstruture.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<UserPackage> UserPackage { get; set; }
        public DbSet<UserWallet> UserWallet { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemCategory> ItemCategory { get; set; }
        public DbSet<ItemDiscount> ItemDiscount { get; set; }
        public DbSet<ItemImage> ItemImage { get; set; }
        public DbSet<ItemInfo> ItemInfo { get; set; }
        public DbSet<ItemReview> ItemReview { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostCategory> PostCategory { get; set; }
        public DbSet<PostImage> PostImage { get; set; }
        public DbSet<TransitionHistory> TransitionHistory { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<UserNotification> UserNotification { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }

    }
}
