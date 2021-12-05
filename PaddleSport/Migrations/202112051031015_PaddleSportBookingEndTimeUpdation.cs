namespace PaddleSport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaddleSportBookingEndTimeUpdation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "EndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "EndTime");
        }
    }
}
