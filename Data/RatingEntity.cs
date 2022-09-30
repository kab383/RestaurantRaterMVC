using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterMVC.Data
{
    public class RatingEntity
    {
        // Key - EntityFramework assumes this based on naming convention.
        [Key]
        public int Id { get; set; }
        // Foreign Key - EntityFramework assumes this based on naming convention.
        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }
        public double FoodScore { get; set; }
        public double CleanlinessScore { get; set; }
        public double AtmosphereScore { get; set; }
        public virtual RestaurantEntity Restaurant { get; set; }
    }
}