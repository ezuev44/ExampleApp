using System.ComponentModel.DataAnnotations;

namespace ProviderTwo.Context.Entities
{
    public class TwoPont
    {
        [Key]
        public int Id { get; set; }

        // Mandatory
        // Name of point, e.g. Moscow\Sochi
        public string Point { get; set; }

        // Mandatory
        // Date for point in Route, e.g. Point = Moscow, Date = 2023-01-01 15-00-00
        public DateTime Date { get; set; }


        public TwoPointType PointType { get; set; }

        public TwoRoute TwoRoute { get; set; }

        public int TwoRouteId { get; set; }
    }
}
