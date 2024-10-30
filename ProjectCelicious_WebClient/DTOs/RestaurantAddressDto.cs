namespace ProjectCelicious_WebClient.DTOs
{
    public class RestaurantAddressDto
    {
        public int RestaurantAddressId { get; set; }
        public string? HouseNumber { get; set; }
        public string Street { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string? GoogleMapLink { get; set; }
    }
}
