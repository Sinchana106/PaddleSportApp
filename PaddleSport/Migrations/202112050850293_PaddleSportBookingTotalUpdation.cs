namespace PaddleSport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaddleSportBookingTotalUpdation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "total", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "total");
        }
    }
}
