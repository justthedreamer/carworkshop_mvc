using CarWorkshop.Domain.Entities;
using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Repositories
{
    public class CarworkshopRatingRepository : ICarWorkshopRatingRepository
    {
        private readonly CarWorkshopDbContext _dbContext;

        public CarworkshopRatingRepository(CarWorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateNewRating(CarWorkshopRating rating)
        {
            _dbContext.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<CarWorkshopRating>> GetCarworkshopRatings(Domain.Entities.CarWorkshop carworkshop)
        {
            var ratings = await _dbContext.Ratings.Where(c => c.CarWorkshopId == carworkshop.Id).ToListAsync();

            var result = new List<CarWorkshopRating>();

            foreach(var rating in ratings)
            {
                result.Add(new()
                {
                    Id = rating.Id,
                    Rate = rating.Rate,
                    Description = rating.Description,
                    CreatedAt = rating.CreatedAt,
                    CreatedById = rating.CreatedById,
                    CreatedBy = _dbContext.Users.FirstOrDefault(u => u.Id == rating.CreatedById),
                    CarWorkshopId = rating.CarWorkshopId,
                    CarWorkshop = _dbContext.CarWorkshops.FirstOrDefault(c => c.Id == rating.CarWorkshopId),
                });
            }

            return result;
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
