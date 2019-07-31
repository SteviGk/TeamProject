namespace BusMeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BusRoutes", new[] { "From_Id" });
            DropIndex("dbo.BusRoutes", new[] { "To_Id" });
            DropColumn("dbo.BusRoutes", "FromCityId");
            DropColumn("dbo.BusRoutes", "ToCityId");
            RenameColumn(table: "dbo.BusRoutes", name: "From_Id", newName: "FromCityId");
            RenameColumn(table: "dbo.BusRoutes", name: "To_Id", newName: "ToCityId");
            AlterColumn("dbo.BusRoutes", "FromCityId", c => c.Int(nullable: false));
            AlterColumn("dbo.BusRoutes", "ToCityId", c => c.Int(nullable: false));
            AlterColumn("dbo.BusRoutes", "FromCityId", c => c.Int(nullable: false));
            AlterColumn("dbo.BusRoutes", "ToCityId", c => c.Int(nullable: false));
            CreateIndex("dbo.BusRoutes", "FromCityId");
            CreateIndex("dbo.BusRoutes", "ToCityId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BusRoutes", new[] { "ToCityId" });
            DropIndex("dbo.BusRoutes", new[] { "FromCityId" });
            AlterColumn("dbo.BusRoutes", "ToCityId", c => c.Int());
            AlterColumn("dbo.BusRoutes", "FromCityId", c => c.Int());
            AlterColumn("dbo.BusRoutes", "ToCityId", c => c.String(nullable: false));
            AlterColumn("dbo.BusRoutes", "FromCityId", c => c.String(nullable: false));
            RenameColumn(table: "dbo.BusRoutes", name: "ToCityId", newName: "To_Id");
            RenameColumn(table: "dbo.BusRoutes", name: "FromCityId", newName: "From_Id");
            AddColumn("dbo.BusRoutes", "ToCityId", c => c.String(nullable: false));
            AddColumn("dbo.BusRoutes", "FromCityId", c => c.String(nullable: false));
            CreateIndex("dbo.BusRoutes", "To_Id");
            CreateIndex("dbo.BusRoutes", "From_Id");
        }
    }
}
