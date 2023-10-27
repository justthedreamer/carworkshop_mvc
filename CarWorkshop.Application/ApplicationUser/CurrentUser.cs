using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.ApplicationUser
{
    public class CurrentUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get;set; }
        
        public CurrentUser(string id, string email, IEnumerable<string> roles, string username)
        {
            Id = id;
            Email = email;
            Roles = roles;
            UserName = username;
        }

        public bool IsInRole(string role) => Roles.Contains(role);
        public bool IsEnableToCreate() => !Roles.Contains("Banned");
    }
}
