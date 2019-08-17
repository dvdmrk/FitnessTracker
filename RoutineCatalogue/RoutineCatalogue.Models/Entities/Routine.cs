using System.Collections.Generic;
namespace RoutineCatalogue.Models.Entities
{
    public class Routine : NamedAuditableEntity
    {
        public ICollection<Set> Sets { get; set; }
    }
}
