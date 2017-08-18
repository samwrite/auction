using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace auction.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }

        public string First { get; set; }
        
        public string Last { get; set; }
       
        public string Username { get; set; }

        public string Password { get; set; }

        public double Balance { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    
        public List<Auction> Items { get; set; }
        public User()
        {
            Items = new List<Auction>();
            Balance = 1000;
        }
    }
}
