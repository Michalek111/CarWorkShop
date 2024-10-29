using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Application.ApplicationUser
{
    public class CurrentUser
    {
        public CurrentUser(string iD, string email, IEnumerable<string> roles)
        {
            ID = iD;
            Email = email;
            Roles = roles;
        }

        public string ID { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public bool IsInRole(string role) => Roles.Contains(role);

    }
}
