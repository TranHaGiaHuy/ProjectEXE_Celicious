using BusinessObjects.Models;
using DataAccess.DAOs;
using Repository.IRepository;


namespace Repository.Repository
{
   
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantDAO _restaurantDAO;

        public RestaurantRepository(RestaurantDAO restaurantDAO)
        {
            _restaurantDAO = restaurantDAO;
        }

        public async Task<List<Restaurant>> GetAlls() => await _restaurantDAO.GetAllRestaurantsAsync();
        public async Task<Restaurant?> GetById(int id) => await _restaurantDAO.GetRestaurantByIdAsync(id);
        public async Task Add(Restaurant restaurant) => await _restaurantDAO.AddRestaurantAsync(restaurant);
        public async Task Update(Restaurant restaurant) => await _restaurantDAO.UpdateRestaurantAsync(restaurant);
        public async Task Delete(int id) => await _restaurantDAO.DeleteRestaurantAsync(id);
        public async Task<List<Restaurant>> SearchByName(string? name) => await _restaurantDAO.SearchAsync(name);
    }

}
