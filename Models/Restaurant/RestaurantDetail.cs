using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterMVC.Models.Restaurant
{
    public class RestaurantDetail
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Location { get; set; }
        [Display(Name = "Average Score")]
        public double Score { get; set; }
        [Display(Name = "Food Score")]
        public double AverageFoodScore { get; set; }
        [Display(Name = "Cleanliness Score")]
        public double AverageCleanlinessScore { get; set; }
        [Display(Name = "Atmosphere Score")]
        public double AverageAtmosphereScore { get; set; }
    }
}