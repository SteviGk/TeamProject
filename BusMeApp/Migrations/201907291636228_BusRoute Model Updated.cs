namespace BusMeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BusRouteModelUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BusRoutes", "FromCityId", "dbo.Cities");
            DropForeignKey("dbo.BusRoutes", "ToCityId", "dbo.Cities");
            DropIndex("dbo.BusRoutes", new[] { "FromCityId" });
            DropIndex("dbo.BusRoutes", new[] { "ToCityId" });
            AddColumn("dbo.BusRoutes", "From_Id", c => c.Int());
            AddColumn("dbo.BusRoutes", "To_Id", c => c.Int());
            AlterColumn("dbo.BusRoutes", "FromCityId", c => c.String(nullable: false));
            AlterColumn("dbo.BusRoutes", "ToCityId", c => c.String(nullable: false));
            CreateIndex("dbo.BusRoutes", "From_Id");
            CreateIndex("dbo.BusRoutes", "To_Id");
            AddForeignKey("dbo.BusRoutes", "From_Id", "dbo.Cities", "Id");
            AddForeignKey("dbo.BusRoutes", "To_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BusRoutes", "To_Id", "dbo.Cities");
            DropForeignKey("dbo.BusRoutes", "From_Id", "dbo.Cities");
            DropIndex("dbo.BusRoutes", new[] { "To_Id" });
            DropIndex("dbo.BusRoutes", new[] { "From_Id" });
            AlterColumn("dbo.BusRoutes", "ToCityId", c => c.Int(nullable: false));
            AlterColumn("dbo.BusRoutes", "FromCityId", c => c.Int(nullable: false));
            DropColumn("dbo.BusRoutes", "To_Id");
            DropColumn("dbo.BusRoutes", "From_Id");
            CreateIndex("dbo.BusRoutes", "ToCityId");
            CreateIndex("dbo.BusRoutes", "FromCityId");
            AddForeignKey("dbo.BusRoutes", "ToCityId", "dbo.Cities", "Id");
            AddForeignKey("dbo.BusRoutes", "FromCityId", "dbo.Cities", "Id");
        }
    }
}
