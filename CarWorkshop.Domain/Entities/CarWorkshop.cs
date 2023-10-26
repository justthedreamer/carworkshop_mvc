using Microsoft.AspNetCore.Identity;
using Shared.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Domain.Entities
{
    public class CarWorkshop
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public CarWoskshopContactDetails ContactDetails { get; set; } = default!;

        public string? About { get; set; }
        public string? CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }
        public string? EncodedName { get; private set; } = default!;

        public List<CarWorkshopService> Services { get; set; } = new();
        public List<CarWorkshopRating>? Ratings { get; set; } = new ();

        public void EncodeName() => EncodedName = Name.ToLower().Replace(" ","-");

        
    }
}
