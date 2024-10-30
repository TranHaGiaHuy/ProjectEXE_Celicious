using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> GetAlls();
        Task<Restaurant?> GetById(int id);
        Task Add(Restaurant restaurant);
        Task Update(Restaurant restaurant);
        Task Delete(int id);
        Task<List<Restaurant>> SearchByName(string? name);
    }
}
