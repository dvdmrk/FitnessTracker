namespace RoutineCatalogue.Models.Entities
{
    public class Set : AuditableEntity
    {
        public Exercise Exercise { get; set; }
        public Routine Routine { get; set; }
        public int? Repitions { get; set; }
        public double Weight { get; set; }
        public int Order { get; set; }
    }
}