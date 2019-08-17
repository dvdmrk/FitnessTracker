using Microsoft.AspNetCore.Identity;
using System;
namespace RoutineCatalogue.Models.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public Role() : base() { }
        public Role(string roleName) : base(roleName) { }
        public Role(string roleName, string description, DateTime creationDate) : base(roleName)
        {
            this.Description = description;
            this.CreationDate = creationDate;
        }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}