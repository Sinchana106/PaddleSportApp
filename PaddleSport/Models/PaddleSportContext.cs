using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PaddleSport.Models
{
    public class PaddleSportContext : DbContext
    {
       
            public DbSet<Location> location { get; set; }
            public DbSet<Court> court { get; set; }
            public DbSet<ContactInfo> contact { get; set; }
            public DbSet<PaymentInfo> payment { get; set; }
            public DbSet<Booking> booking { get; set; }
           
    }
    }
