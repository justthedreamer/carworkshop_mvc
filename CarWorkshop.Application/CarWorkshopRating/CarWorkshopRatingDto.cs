using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopRating
{
    public class CarWorkshopRatingDto
    {
        public int CarWorkshopId { get; set; }
        public int Rate { get; set; }
        public string? Description { get; set; }
        public string? CreatedByName { get; set; }
        public string CreatedById { get; set; }
        public string? CreatedAt { get; set; }
        public bool? IsEditiable { get; set; }
    }
}
