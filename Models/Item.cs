using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace auction.Models
{
    public class Item : BaseEntity
    {
        public int ItemId { get; set; }
        public string Name { get; set; }

        public double StartBid { get; set; }
        
        public string Description { get; set; }
        
        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    
        public User Creator { get; set; }

        public double HighestBid { get; set; }

        public User HighestBidder { get; set; }

        public List<Auction> Auctions { get; set; }

        public Item(){
            Auctions = new List<Auction>();
        }
    }
}
