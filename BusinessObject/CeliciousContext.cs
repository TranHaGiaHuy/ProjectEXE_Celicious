using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace BusinessObjects
{
    public class CeliciousContext : DbContext
    {
        public CeliciousContext(DbContextOptions<CeliciousContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"..\BusinessObjects"))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            //Temporally use solid path
            /*Console.WriteLine($"Using directory:"+ Path.GetFullPath(@"..\BusinessObjects"));
            Console.WriteLine($"Using connection string: {configuration.GetConnectionString("ASM1Connection")}");*/
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CeliciousContext"));
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<RestaurantAddress> RestaurantAddresses { get; set; }
        public virtual DbSet<RestaurantCategory> RestaurantCategories { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<DishCategory> DishCategories { get; set; }
        public virtual DbSet<DishImage> DishImages { get; set; }
        public virtual DbSet<RestaurantImage> RestaurantImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Review>().ToTable("Review");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurant");
            modelBuilder.Entity<RestaurantAddress>().ToTable("RestaurantAddress");
            modelBuilder.Entity<RestaurantCategory>().ToTable("RestaurantCategory");
            modelBuilder.Entity<Dish>().ToTable("Dish");
            modelBuilder.Entity<DishCategory>().ToTable("DishCategory");


            /*==================================================================================================================*/
            modelBuilder.Entity<User>().HasData(
                    new User { FullName = "Nguyen Van A", Phone = 123456789, CreateDate = new DateTime(2023, 6, 1), Gender = "Male", Avatar = "https://example.com/avatar1.jpg", Address = "123 Nguyen Van Cu, Can Tho" },
                    new User { FullName = "Tran Thi B", Phone = 987654321, CreateDate = new DateTime(2023, 6, 15), Gender = "Female", Avatar = "https://example.com/avatar2.jpg", Address = "456 Vo Van Kiet, An Giang" },
                    new User { FullName = "Le Van C", Phone = 112233445, CreateDate = new DateTime(2023, 7, 1), Gender = "Male", Avatar = "https://example.com/avatar3.jpg", Address = "789 Hoang Hoa Tham, Can Tho" },
                    new User { FullName = "Phan Thi D", Phone = 556677889, CreateDate = new DateTime(2023, 7, 20), Gender = "Female", Avatar = "https://example.com/avatar4.jpg", Address = "101 Dien Bien Phu, Ho Chi Minh" },
                    new User { FullName = "Hoang Van E", Phone = 667788990, CreateDate = new DateTime(2023, 8, 5), Gender = "Male", Avatar = "https://example.com/avatar5.jpg", Address = "202 Vo Van Linh, Soc Trang" }
            );
            modelBuilder.Entity<Account>().HasData(
                new Account { UserId = 1, Username = "admin", Password = "admin123", Role = 1, Status = 1 },
                    new Account { UserId = 2, Username = "user", Password = "user123", Role = 2, Status = 1 },
                    new Account { UserId = 3, Username = "guest", Password = "guest123", Role = 3, Status = 0 },
                    new Account { UserId = 4, Username = "guest", Password = "guest123", Role = 3, Status = 0 },
                    new Account { UserId = 5, Username = "guest", Password = "guest123", Role = 3, Status = 0 }
            );
            modelBuilder.Entity<RestaurantCategory>().HasData(
                    new RestaurantCategory { Name = "Food" },
                    new RestaurantCategory { Name = "Drink" },
                    new RestaurantCategory { Name = "Sweet" },
                    new RestaurantCategory { Name = "Fast Food" },
                    new RestaurantCategory { Name = "Vegitarien" }
            );

            modelBuilder.Entity<RestaurantAddress>().HasData(
                    new RestaurantAddress { HouseNumber = "43A", Street = "Nguyen Van Cu", District = "An Hoa", Province = "Can Tho", GoogleMapLink = "https://maps.app.goo.gl/DaB7fy8yHryDc77P8" },
                    new RestaurantAddress { HouseNumber = "22B", Street = "Vo Van Kiet", District = "An Binh", Province = "An Giang", GoogleMapLink = "https://maps.app.goo.gl/etrpPa5GXwg31uem7" },
                    new RestaurantAddress { HouseNumber = "54B", Street = "Hoang Hoa Tham", District = "An Cu", Province = "Can Tho", GoogleMapLink = "https://maps.app.goo.gl/W78UiYcpagZo1yGQ9" },
                    new RestaurantAddress { HouseNumber = "890NH", Street = "Dien Bien Phu", District = "An Binh", Province = "Ho Chi Minh", GoogleMapLink = "https://maps.app.goo.gl/scbsaM31xNhDT3od8" },
                    new RestaurantAddress { HouseNumber = "53U", Street = "Vo Van Linh", District = "An Thoi", Province = "Soc Trang", GoogleMapLink = "https://maps.app.goo.gl/e69hrTDrDkMkqkUa9" }

            );
            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant { Logo = "/images/homepage/meals/logo-1.jpg", Background = "/images/homepage/meals/img-1.jpg", RestaurantAddressId = 1, RestaurantName = "Nha Hang Sanh Loc", Phone = "0572857285", Description = "Quan an ngon", StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(20, 0), RestaurantCategoryId = 2 },
                     new Restaurant { Logo = "/images/homepage/meals/logo-2.jpg", Background = "/images/homepage/meals/img-2.jpg", RestaurantAddressId = 2, RestaurantName = "Ga Ran Five Start", Phone = "0576938761", Description = "crunch", StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(21, 0), RestaurantCategoryId = 1 },
                     new Restaurant { Logo = "/images/homepage/meals/logo-3.jpg", Background = "/images/homepage/meals/img-3.jpg", RestaurantAddressId = 3, RestaurantName = "Domino Pizza", Phone = "0395684691", Description = "Banh pizza", StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(22, 0), RestaurantCategoryId = 4 },
                     new Restaurant { Logo = "/images/homepage/meals/logo-4.jpg", Background = "/images/homepage/meals/img-4.jpg", RestaurantAddressId = 4, RestaurantName = "Chu Lun", Phone = "0923487318", Description = "Com me nau", StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(23, 0), RestaurantCategoryId = 3 },
                     new Restaurant { Logo = "/images/homepage/meals/logo-5.jpg", Background = "/images/homepage/meals/img-5.jpg", RestaurantAddressId = 5, RestaurantName = "Com Nong", Phone = "0924783451", Description = "Com bo nau", StartTime = new TimeOnly(6, 0), EndTime = new TimeOnly(22, 0), RestaurantCategoryId = 1 }

            );
            modelBuilder.Entity<Review>().HasData(
                  new Review { UserId = 1, RestaurantId = 1, Description = "The food was amazing. Will visit again.", Image = "https://example.com/image1.jpg", Rating = 4.5f, CreateTime = new DateTime(2024, 1, 10) },
                         new Review { UserId = 2, RestaurantId = 2, Description = "Friendly staff and great service.", Image = "https://example.com/image2.jpg", Rating = 4.0f, CreateTime = new DateTime(2024, 2, 11) },
                         new Review { UserId = 3, RestaurantId = 3, Description = "The pizza was really delicious and well made.", Image = "https://example.com/image3.jpg", Rating = 5.0f, CreateTime = new DateTime(2024, 3, 12) },
                         new Review { UserId = 4, RestaurantId = 4, Description = "Food was okay, but the service needs improvement.", Image = "https://example.com/image4.jpg", Rating = 3.0f, CreateTime = new DateTime(2024, 4, 13) },
                         new Review { UserId = 5, RestaurantId = 5, Description = "Everything was perfect, highly recommended.", Image = "https://example.com/image5.jpg", Rating = 5.0f, CreateTime = new DateTime(2024, 5, 14) }

            );
            modelBuilder.Entity<DishCategory>().HasData(
                new DishCategory { Name = "Appetizers" },
                    new DishCategory { Name = "Main Courses" },
                    new DishCategory { Name = "Desserts" },
                    new DishCategory { Name = "Beverages" },
                    new DishCategory { Name = "Salads" }
            );
            modelBuilder.Entity<Dish>().HasData(
                    new Dish { RestaurantId = 1, DishCategoryId = 1, DishName = "Spring Rolls", Description = "Crispy rolls with vegetables", Price = 5.99, LinkToShoppe = "https://example.com/shoppe/spring-rolls", Image = "https://example.com/images/spring-rolls.jpg" },
                    new Dish { RestaurantId = 1, DishCategoryId = 1, DishName = "Grilled Chicken", Description = "Juicy grilled chicken with herbs", Price = 12.99, LinkToShoppe = "https://example.com/shoppe/grilled-chicken", Image = "https://example.com/images/grilled-chicken.jpg" },
                    new Dish { RestaurantId = 2, DishCategoryId = 3, DishName = "Chocolate Cake", Description = "Rich chocolate cake with a creamy filling", Price = 7.49, LinkToShoppe = "https://example.com/shoppe/chocolate-cake", Image = "https://example.com/images/chocolate-cake.jpg" },
                    new Dish { RestaurantId = 3, DishCategoryId = 4, DishName = "Green Tea", Description = "Refreshing green tea with mint", Price = 2.99, LinkToShoppe = "https://example.com/shoppe/green-tea", Image = "https://example.com/images/green-tea.jpg" },
                    new Dish { RestaurantId = 3, DishCategoryId = 5, DishName = "Caesar Salad", Description = "Classic Caesar salad with crisp lettuce and Parmesan cheese", Price = 6.49, LinkToShoppe = "https://example.com/shoppe/caesar-salad", Image = "https://example.com/images/caesar-salad.jpg" }

            );
            modelBuilder.Entity<DishImage>().HasData(
                    new DishImage { DishId = 1, ImagePath = "/images/homepage/meals/img-1.jpg" },
                    new DishImage { DishId = 1, ImagePath = "/images/homepage/meals/img-3.jpg" },
                    new DishImage { DishId = 1, ImagePath = "/images/homepage/meals/img-1.jpg" },
                    new DishImage { DishId = 2, ImagePath = "/images/homepage/meals/img-2.jpg" },
                    new DishImage { DishId = 2, ImagePath = "/images/homepage/meals/img-1.jpg" },
                    new DishImage { DishId = 2, ImagePath = "/images/homepage/meals/img-3.jpg" },
                    new DishImage { DishId = 3, ImagePath = "/images/homepage/meals/img-2.jpg" },
                    new DishImage { DishId = 3, ImagePath = "/images/homepage/meals/img-1.jpg" }
            );
            modelBuilder.Entity<RestaurantImage>().HasData(
                    new RestaurantImage { RestaurantId = 1, ImagePath = "/images/homepage/meals/logo-1.jpg" },
                    new RestaurantImage { RestaurantId = 1, ImagePath = "/images/homepage/meals/logo-2.jpg" },
                    new RestaurantImage { RestaurantId = 1, ImagePath = "/images/homepage/meals/logo-3.jpg" },
                    new RestaurantImage { RestaurantId = 2, ImagePath = "/images/homepage/meals/logo-4.jpg" },
                    new RestaurantImage { RestaurantId = 2, ImagePath = "/images/homepage/meals/logo-5.jpg" },
                    new RestaurantImage { RestaurantId = 2, ImagePath = "/images/homepage/meals/logo-6.jpg" },
                    new RestaurantImage { RestaurantId = 3, ImagePath = "/images/homepage/meals/logo-7.jpg" },
                    new RestaurantImage { RestaurantId = 3, ImagePath = "/images/homepage/meals/logo-8.jpg" }

            );
        }
    }
}
