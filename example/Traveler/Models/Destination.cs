using System;
namespace Traveler.Models
{
    public class Destination
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public float Rating { get; set; }
        public int Votes { get; set; }

        public string RatingVotes => $"{Rating:0.0} ({Votes} votes)";
    }
}
