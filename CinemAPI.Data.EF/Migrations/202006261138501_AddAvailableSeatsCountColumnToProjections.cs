namespace CinemAPI.Data.EF
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddAvailableSeatsCountColumnToProjections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projections", "AvailableSeatsCount", c => c.Int(nullable: false));

            Sql("alter table Projections add constraint CHK_Projections_AvailableSeatsCount check (AvailableSeatsCount>=0)");
        }

        public override void Down()
        {
            DropColumn("dbo.Projections", "AvailableSeatsCount");

            Sql("alter table Projections drop constraint CHK_Projections_AvailableSeatsCount");
        }
    }
}
