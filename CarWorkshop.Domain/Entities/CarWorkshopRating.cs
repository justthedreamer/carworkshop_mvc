using Microsoft.AspNetCore.Identity;
using Shared.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Domain.Entities
{
    public class CarWorkshopRating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }

        public int CarWorkshopId { get; set; } = default!;
        public CarWorkshop CarWorkshop { get; set; }
    }
}
