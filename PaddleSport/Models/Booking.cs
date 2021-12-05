using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PaddleSport.Models
{
    public class Booking
    {
        public int Id { get; set; }
        [ForeignKey("Loc")]
        public int? LocationId { get; set; }
        public Location Loc { get; set; }
        public DateTime Timings { get; set; }

        public int NoOfHours { get; set; }
        public DateTime EndTime { get; set; }


        [ForeignKey("CourtFk")]
        public int? CourtId { get; set; }
        public Court CourtFk { get; set; }
        public int NoOfPlayers { get; set; }

        public float? total { get; set; }

    }
}