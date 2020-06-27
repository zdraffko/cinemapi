namespace CinemAPI.Data.EF
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationshipBetweenProjectionsAndTickets : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Tickets", new[] { "ProjectionStartDate", "MovieName", "CinemaName", "RoomNumber", "Row", "Column" });
            AddColumn("dbo.Tickets", "ProjectionId", c => c.Long(nullable: false));
            CreateIndex("dbo.Tickets", new[] { "ProjectionId", "Row", "Column" }, unique: true);
            AddForeignKey("dbo.Tickets", "ProjectionId", "dbo.Projections", "Id", cascadeDelete: true);
            DropColumn("dbo.Tickets", "ProjectionStartDate");
            DropColumn("dbo.Tickets", "MovieName");
            DropColumn("dbo.Tickets", "CinemaName");
            DropColumn("dbo.Tickets", "RoomNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "RoomNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "CinemaName", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Tickets", "MovieName", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Tickets", "ProjectionStartDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Tickets", "ProjectionId", "dbo.Projections");
            DropIndex("dbo.Tickets", new[] { "ProjectionId", "Row", "Column" });
            DropColumn("dbo.Tickets", "ProjectionId");
            CreateIndex("dbo.Tickets", new[] { "ProjectionStartDate", "MovieName", "CinemaName", "RoomNumber", "Row", "Column" }, unique: true);
        }
    }
}
