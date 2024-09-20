using System.ComponentModel.DataAnnotations;

namespace ProviderTwo.Context.Entities
{
    public class TwoRoute
    {
        [Key]
        public int Id { get; set; }

        public List<TwoPont> TwoPonts { get; set; }

        // Mandatory
        // Price of route
        public decimal Price { get; set; }

        // Mandatory
        // Timelimit. After it expires, route became not actual
        public DateTime TimeLimit { get; set; }
    }
}