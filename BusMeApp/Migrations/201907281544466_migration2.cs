namespace BusMeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BusRoutes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BusRoutes", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.BusRoutes", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BusRoutes", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.BusRoutes", "ApplicationUser_Id");
            AddForeignKey("dbo.BusRoutes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
