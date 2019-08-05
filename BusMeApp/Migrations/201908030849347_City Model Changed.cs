namespace BusMeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityModelChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "CityName", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cities", "CityName", c => c.String(maxLength: 30));
        }
    }
}
