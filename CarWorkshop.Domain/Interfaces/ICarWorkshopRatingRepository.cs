using CarWorkshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Domain.Interfaces
{
    public interface ICarWorkshopRatingRepository
    {
        Task CreateNewRating(CarWorkshopRating rating);
        Task<IEnumerable<CarWorkshopRating>> GetCarworkshopRatings(Domain.Entities.CarWorkshop carworkshop );
        Task<double> GetAverage(Domain.Entities.CarWorkshop carWorkshop);

    }

}
