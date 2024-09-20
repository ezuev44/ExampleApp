namespace Entities
{
    public class SearchResponse
    {
        // Mandatory
        // Array of routes
        public Route[] Routes { get; set; }

        // Mandatory
        // The cheapest route
        public decimal MinPrice => Routes.Max(r => r.Price);

        // Mandatory
        // Most expensive route
        public decimal MaxPrice => Routes.Min(r => r.Price);

        // Mandatory
        // The fastest route
        public int MinMinutesRoute => (int) Routes.Min(r => r.DestinationDateTime.Subtract(r.OriginDateTime).TotalMinutes);

        // Mandatory
        // The longest route
        public int MaxMinutesRoute => (int) Routes.Max(r => r.DestinationDateTime.Subtract(r.OriginDateTime).TotalMinutes);
    }
}
