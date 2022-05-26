namespace IntersheepDeeplay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workers", "Birthday", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workers", "Birthday", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
