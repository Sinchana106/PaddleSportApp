using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PaddleSport.Models
{
    public class Court
    {
        public int Id { get; set; }
        [ForeignKey("Loc")]
        public int? LocationId { get; set; }
        public Location Loc { get; set; }

        public string Name { get; set; }
        public float PricePerHour { get; set; }

    }
}