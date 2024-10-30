using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DishCategory",
                columns: table => new
                {
                    DishCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishCategory", x => x.DishCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantAddress",
                columns: table => new
                {
                    RestaurantAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoogleMapLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantAddress", x => x.RestaurantAddressId);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantCategory",
                columns: table => new
                {
                    RestaurantCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantCategory", x => x.RestaurantCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RestaurantCategoryId = table.Column<int>(type: "int", nullable: true),
                    RestaurantAddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.RestaurantId);
                    table.ForeignKey(
                        name: "FK_Restaurant_RestaurantAddress_RestaurantAddressId",
                        column: x => x.RestaurantAddressId,
                        principalTable: "RestaurantAddress",
                        principalColumn: "RestaurantAddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Restaurant_RestaurantCategory_RestaurantCategoryId",
                        column: x => x.RestaurantCategoryId,
                        principalTable: "RestaurantCategory",
                        principalColumn: "RestaurantCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Account_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dish",
                columns: table => new
                {
                    DishId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    LinkToShoppe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DishCategoryId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dish", x => x.DishId);
                    table.ForeignKey(
                        name: "FK_Dish_DishCategory_DishCategoryId",
                        column: x => x.DishCategoryId,
                        principalTable: "DishCategory",
                        principalColumn: "DishCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dish_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantImages",
                columns: table => new
                {
                    ResImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantImages", x => x.ResImageID);
                    table.ForeignKey(
                        name: "FK_RestaurantImages_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Review_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DishImages",
                columns: table => new
                {
                    DishImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishImages", x => x.DishImageID);
                    table.ForeignKey(
                        name: "FK_DishImages_Dish_DishId",
                        column: x => x.DishId,
                        principalTable: "Dish",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DishCategory",
                columns: new[] { "DishCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Appetizers" },
                    { 2, "Main Courses" },
                    { 3, "Desserts" },
                    { 4, "Beverages" },
                    { 5, "Salads" }
                });

            migrationBuilder.InsertData(
                table: "RestaurantAddress",
                columns: new[] { "RestaurantAddressId", "District", "GoogleMapLink", "HouseNumber", "Province", "Street" },
                values: new object[,]
                {
                    { 1, "An Hoa", "https://maps.app.goo.gl/DaB7fy8yHryDc77P8", "43A", "Can Tho", "Nguyen Van Cu" },
                    { 2, "An Binh", "https://maps.app.goo.gl/etrpPa5GXwg31uem7", "22B", "An Giang", "Vo Van Kiet" },
                    { 3, "An Cu", "https://maps.app.goo.gl/W78UiYcpagZo1yGQ9", "54B", "Can Tho", "Hoang Hoa Tham" },
                    { 4, "An Binh", "https://maps.app.goo.gl/scbsaM31xNhDT3od8", "890NH", "Ho Chi Minh", "Dien Bien Phu" },
                    { 5, "An Thoi", "https://maps.app.goo.gl/e69hrTDrDkMkqkUa9", "53U", "Soc Trang", "Vo Van Linh" }
                });

            migrationBuilder.InsertData(
                table: "RestaurantCategory",
                columns: new[] { "RestaurantCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Food" },
                    { 2, "Drink" },
                    { 3, "Sweet" },
                    { 4, "Fast Food" },
                    { 5, "Vegitarien" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Address", "Avatar", "CreateDate", "FullName", "Gender", "Phone" },
                values: new object[,]
                {
                    { 1, "123 Nguyen Van Cu, Can Tho", "https://example.com/avatar1.jpg", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen Van A", "Male", 123456789 },
                    { 2, "456 Vo Van Kiet, An Giang", "https://example.com/avatar2.jpg", new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tran Thi B", "Female", 987654321 },
                    { 3, "789 Hoang Hoa Tham, Can Tho", "https://example.com/avatar3.jpg", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Le Van C", "Male", 112233445 },
                    { 4, "101 Dien Bien Phu, Ho Chi Minh", "https://example.com/avatar4.jpg", new DateTime(2023, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phan Thi D", "Female", 556677889 },
                    { 5, "202 Vo Van Linh, Soc Trang", "https://example.com/avatar5.jpg", new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hoang Van E", "Male", 667788990 }
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "Email", "Password", "Role", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", "AQAAAAIAAYagAAAAEA8f28EWjuoPHJCJ6ggrp2CpieVcHbFUN1saVkV6p/89/5zCwmmIfDtxT8rI96tdBw==", 0, 1, 1 },
                    { 2, "user@gmail.com", "AQAAAAIAAYagAAAAEILUAtMXHmLPHEbz+CxKaawhFqdz6A2qKqXfK+4GECdpFM8gl7hin+D3/MrfLGELBQ==", 1, 1, 2 },
                    { 3, "owner@gmail.com", "AQAAAAIAAYagAAAAEA8f28EWjuoPHJCJ6ggrp2CpieVcHbFUN1saVkV6p/89/5zCwmmIfDtxT8rI96tdBw==", 2, 0, 3 },
                    { 4, "guest@gmail.com", "AQAAAAIAAYagAAAAEA8f28EWjuoPHJCJ6ggrp2CpieVcHbFUN1saVkV6p/89/5zCwmmIfDtxT8rI96tdBw==", 1, 0, 4 }
                });

            migrationBuilder.InsertData(
                table: "Restaurant",
                columns: new[] { "RestaurantId", "Background", "Description", "EndTime", "Logo", "Phone", "RestaurantAddressId", "RestaurantCategoryId", "RestaurantName", "StartTime", "Status" },
                values: new object[,]
                {
                    { 1, "/images/homepage/meals/img-1.jpg", "Quan an ngon", new TimeOnly(20, 0, 0), "/images/homepage/meals/logo-1.jpg", "0572857285", 1, 2, "Nha Hang Sanh Loc", new TimeOnly(10, 0, 0), 0 },
                    { 2, "/images/homepage/meals/img-2.jpg", "crunch", new TimeOnly(21, 0, 0), "/images/homepage/meals/logo-2.jpg", "0576938761", 2, 1, "Ga Ran Five Start", new TimeOnly(11, 0, 0), 0 },
                    { 3, "/images/homepage/meals/img-3.jpg", "Banh pizza", new TimeOnly(22, 0, 0), "/images/homepage/meals/logo-3.jpg", "0395684691", 3, 4, "Domino Pizza", new TimeOnly(9, 0, 0), 0 },
                    { 4, "/images/homepage/meals/img-4.jpg", "Com me nau", new TimeOnly(23, 0, 0), "/images/homepage/meals/logo-4.jpg", "0923487318", 4, 3, "Chu Lun", new TimeOnly(10, 0, 0), 0 },
                    { 5, "/images/homepage/meals/img-5.jpg", "Com bo nau", new TimeOnly(22, 0, 0), "/images/homepage/meals/logo-5.jpg", "0924783451", 5, 1, "Com Nong", new TimeOnly(6, 0, 0), 0 }
                });

            migrationBuilder.InsertData(
                table: "Dish",
                columns: new[] { "DishId", "Description", "DishCategoryId", "DishName", "Image", "LinkToShoppe", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Crispy rolls with vegetables", 1, "Spring Rolls", "https://example.com/images/spring-rolls.jpg", "https://example.com/shoppe/spring-rolls", 5.9900000000000002, 1 },
                    { 2, "Juicy grilled chicken with herbs", 1, "Grilled Chicken", "https://example.com/images/grilled-chicken.jpg", "https://example.com/shoppe/grilled-chicken", 12.99, 1 },
                    { 3, "Rich chocolate cake with a creamy filling", 3, "Chocolate Cake", "https://example.com/images/chocolate-cake.jpg", "https://example.com/shoppe/chocolate-cake", 7.4900000000000002, 2 },
                    { 4, "Refreshing green tea with mint", 4, "Green Tea", "https://example.com/images/green-tea.jpg", "https://example.com/shoppe/green-tea", 2.9900000000000002, 3 },
                    { 5, "Classic Caesar salad with crisp lettuce and Parmesan cheese", 5, "Caesar Salad", "https://example.com/images/caesar-salad.jpg", "https://example.com/shoppe/caesar-salad", 6.4900000000000002, 3 }
                });

            migrationBuilder.InsertData(
                table: "RestaurantImages",
                columns: new[] { "ResImageID", "ImagePath", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "/images/homepage/meals/logo-1.jpg", 1 },
                    { 2, "/images/homepage/meals/logo-2.jpg", 1 },
                    { 3, "/images/homepage/meals/logo-3.jpg", 1 },
                    { 4, "/images/homepage/meals/logo-4.jpg", 2 },
                    { 5, "/images/homepage/meals/logo-5.jpg", 2 },
                    { 6, "/images/homepage/meals/logo-6.jpg", 2 },
                    { 7, "/images/homepage/meals/logo-7.jpg", 3 },
                    { 8, "/images/homepage/meals/logo-8.jpg", 3 }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "ReviewId", "CreateTime", "Description", "Image", "Rating", "RestaurantId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The food was amazing. Will visit again.", "https://example.com/image1.jpg", 4.5f, 1, 1 },
                    { 2, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Friendly staff and great service.", "https://example.com/image2.jpg", 4f, 2, 2 },
                    { 3, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "The pizza was really delicious and well made.", "https://example.com/image3.jpg", 5f, 3, 3 },
                    { 4, new DateTime(2024, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Food was okay, but the service needs improvement.", "https://example.com/image4.jpg", 3f, 4, 4 },
                    { 5, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Everything was perfect, highly recommended.", "https://example.com/image5.jpg", 5f, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "DishImages",
                columns: new[] { "DishImageID", "DishId", "ImagePath" },
                values: new object[,]
                {
                    { 1, 1, "/images/homepage/meals/img-1.jpg" },
                    { 2, 1, "/images/homepage/meals/img-3.jpg" },
                    { 3, 1, "/images/homepage/meals/img-1.jpg" },
                    { 4, 2, "/images/homepage/meals/img-2.jpg" },
                    { 5, 2, "/images/homepage/meals/img-1.jpg" },
                    { 6, 2, "/images/homepage/meals/img-3.jpg" },
                    { 7, 3, "/images/homepage/meals/img-2.jpg" },
                    { 8, 3, "/images/homepage/meals/img-1.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dish_DishCategoryId",
                table: "Dish",
                column: "DishCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Dish_RestaurantId",
                table: "Dish",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_DishImages_DishId",
                table: "DishImages",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_RestaurantAddressId",
                table: "Restaurant",
                column: "RestaurantAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_RestaurantCategoryId",
                table: "Restaurant",
                column: "RestaurantCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantImages_RestaurantId",
                table: "RestaurantImages",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_RestaurantId",
                table: "Review",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "DishImages");

            migrationBuilder.DropTable(
                name: "RestaurantImages");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Dish");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "DishCategory");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "RestaurantAddress");

            migrationBuilder.DropTable(
                name: "RestaurantCategory");
        }
    }
}
