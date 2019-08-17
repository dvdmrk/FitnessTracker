using Microsoft.AspNetCore.Identity;
using System;
namespace RoutineCatalogue.Models.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User() : base() { }
        public Role Role { get; set; }
    }
}