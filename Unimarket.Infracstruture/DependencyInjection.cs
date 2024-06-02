using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Infracstruture.Data;
using Unimarket.Infracstruture.Repositories;
using Unimarket.Infracstruture.Services;

namespace Unimarket.Infracstruture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            #region entity
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();

            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemService, ItemService>();

            services.AddScoped<IItemImageRepository, ItemImageRepository>();
            services.AddScoped<IItemImageService, ItemImageService>();

            services.AddScoped<IItemCategoryRepository, ItemCategoryRepository>();
            services.AddScoped<IItemCategoryService, ItemCategoryService>();

            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IPackageService, PackageService>();

            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            #endregion
            return services;
        }
    }
}

