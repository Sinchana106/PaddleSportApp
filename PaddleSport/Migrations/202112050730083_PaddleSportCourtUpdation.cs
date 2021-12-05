namespace PaddleSport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaddleSportCourtUpdation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courts", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courts", "Name");
        }
    }
}
