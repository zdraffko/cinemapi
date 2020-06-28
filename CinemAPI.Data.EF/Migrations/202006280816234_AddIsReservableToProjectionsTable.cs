namespace CinemAPI.Data.EF
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsReservableToProjectionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projections", "IsReservable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projections", "IsReservable");
        }
    }
}
