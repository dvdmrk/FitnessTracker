using System;
namespace RoutineCatalogue.Models.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
    }
    public class AuditableEntity : BaseEntity
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public User CreateBy { get; set; }
        public User UpdateBy { get; set; }
    }
    public class NamedAuditableEntity : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}