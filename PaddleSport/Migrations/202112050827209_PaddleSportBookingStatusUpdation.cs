namespace PaddleSport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaddleSportBookingStatusUpdation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bookings", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "Status", c => c.String());
        }
    }
}
