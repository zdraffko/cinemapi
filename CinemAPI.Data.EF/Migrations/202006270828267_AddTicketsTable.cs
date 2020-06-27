namespace CinemAPI.Data.EF
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProjectionStartDate = c.DateTime(nullable: false),
                        MovieName = c.String(nullable: false, maxLength: 200),
                        CinemaName = c.String(nullable: false, maxLength: 200),
                        RoomNumber = c.Int(nullable: false),
                        Row = c.Int(nullable: false),
                        Column = c.Int(nullable: false),
                        IsReserved = c.Boolean(nullable: false),
                        IsBought = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.ProjectionStartDate, t.MovieName, t.CinemaName, t.RoomNumber, t.Row, t.Column }, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tickets", new[] { "ProjectionStartDate", "MovieName", "CinemaName", "RoomNumber", "Row", "Column" });
            DropTable("dbo.Tickets");
        }
    }
}
