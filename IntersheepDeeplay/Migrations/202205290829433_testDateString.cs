namespace IntersheepDeeplay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testDateString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workers", "Birthday", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workers", "Birthday", c => c.DateTime(nullable: false));
        }
    }
}
