namespace BusMeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatepostmodel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", new[] { "UserId" });
            RenameColumn(table: "dbo.Posts", name: "UserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Posts", "FromUserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Posts", "ToUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Posts", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "FromUserId");
            CreateIndex("dbo.Posts", "ToUserId");
            CreateIndex("dbo.Posts", "ApplicationUser_Id");
            AddForeignKey("dbo.Posts", "FromUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "ToUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ToUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "FromUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Posts", new[] { "ToUserId" });
            DropIndex("dbo.Posts", new[] { "FromUserId" });
            AlterColumn("dbo.Posts", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Posts", "ToUserId");
            DropColumn("dbo.Posts", "FromUserId");
            RenameColumn(table: "dbo.Posts", name: "ApplicationUser_Id", newName: "UserId");
            CreateIndex("dbo.Posts", "UserId");
        }
    }
}
