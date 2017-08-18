using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace auction.Models
{
    public class RegItem : BaseEntity
    {
      
        [Required(ErrorMessage = "Product Name Required")]
        [MinLength(3, ErrorMessage="The Product Name must be at least {0} characters long")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Starting Bid Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        [Display(Name = "Starting Bid")]
        public double StartBid { get; set; }
       
        [Required(ErrorMessage = "Description Required")]
        [MinLength(10, ErrorMessage="The {0} must be at least {0} characters long")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "End Date Required")]
        [MyDate(ErrorMessage ="Date should be in the future")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        
        
    }
}