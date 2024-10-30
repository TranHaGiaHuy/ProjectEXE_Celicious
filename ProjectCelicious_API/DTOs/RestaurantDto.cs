using BusinessObjects.Models;

namespace ProjectCelicious_API.DTOs
{
    public class RestaurantDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; } = default; // Thiết lập giá trị mặc định
        public string Logo { get; set; } = null!;
        public string Background { get; set; } = null!;
        public RestaurentStatus Status { get; set; }
        public int? RestaurantCategoryId { get; set; }

        // Thêm thông tin về RestaurantCategory
        public string? CategoryName { get; set; } // Tên danh mục

        // Thêm thông tin về RestaurantAddress
        public string Address { get; set; } = null!; // Địa chỉ
        public string District { get; set; } = null!;
        public string Province { get; set; } = null!; // Thành phố
    }


}
