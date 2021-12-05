namespace PaddleSport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaddleSportBookingUpdation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "Timings", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "Timings", c => c.Int(nullable: false));
        }
    }
}
