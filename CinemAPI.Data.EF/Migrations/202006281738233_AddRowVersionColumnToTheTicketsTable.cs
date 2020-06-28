namespace CinemAPI.Data.EF
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRowVersionColumnToTheTicketsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "RowVersion");
        }
    }
}
