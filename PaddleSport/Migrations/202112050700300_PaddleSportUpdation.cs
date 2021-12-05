namespace PaddleSport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaddleSportUpdation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationId = c.Int(),
                        Timings = c.Int(nullable: false),
                        NoOfHours = c.Int(nullable: false),
                        CourtId = c.Int(),
                        NoOfPlayers = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courts", t => t.CourtId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.LocationId)
                .Index(t => t.CourtId);
            
            CreateTable(
                "dbo.Courts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationId = c.Int(),
                        PricePerHour = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Subject = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.Int(nullable: false),
                        TransactionId = c.Int(nullable: false),
                        total = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Bookings", "CourtId", "dbo.Courts");
            DropForeignKey("dbo.Courts", "LocationId", "dbo.Locations");
            DropIndex("dbo.Courts", new[] { "LocationId" });
            DropIndex("dbo.Bookings", new[] { "CourtId" });
            DropIndex("dbo.Bookings", new[] { "LocationId" });
            DropTable("dbo.PaymentInfoes");
            DropTable("dbo.ContactInfoes");
            DropTable("dbo.Locations");
            DropTable("dbo.Courts");
            DropTable("dbo.Bookings");
        }
    }
}
