namespace BusMeApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationPaymentCompleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "PaymentCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "PaymentCompleted");
        }
    }
}
