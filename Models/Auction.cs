using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace auction.Models
{
    public class Auction : BaseEntity
    {
        public int AuctionId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
       