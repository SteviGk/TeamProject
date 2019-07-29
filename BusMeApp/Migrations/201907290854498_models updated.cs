namespace BusMeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelsupdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "CityName", c => c.String(maxLength: 30));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "IdentityCard", c => c.String(maxLength: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "IdentityCard", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AlterColumn("dbo.Cities", "CityName", c => c.String());
        }
    }
}
