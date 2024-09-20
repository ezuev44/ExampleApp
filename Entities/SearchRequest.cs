using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class SearchRequest
    {
        [Required]
        // Mandatory
        // Start point of route, e.g. Moscow 
        public string Origin { get; set; }

        [Required]
        // Mandatory
        // End point of route, e.g. Sochi
        public string Destination { get; set; }

        [Required]
        // Mandatory
        // Start date of route
        public DateTime OriginDateTime { get; set; }

        // Optional
        public SearchFilters? Filters { get; set; }
    }
}
