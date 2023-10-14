using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Domain.Entities
{
    public class CarWorkshopService
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }

        public int CarWorkshopId { get; set; } = default!;
        public CarWorkshop CarWorkshop { get; set; }
    }
}
