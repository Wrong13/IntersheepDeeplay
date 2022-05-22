namespace IntersheepDeeplay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDivisionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Workers", "DivisionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Workers", "DivisionId");
            AddForeignKey("dbo.Workers", "DivisionId", "dbo.Divisions", "Id", cascadeDelete: true);
            DropColumn("dbo.Workers", "Division");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workers", "Division", c => c.String());
            DropForeignKey("dbo.Workers", "DivisionId", "dbo.Divisions");
            DropIndex("dbo.Workers", new[] { "DivisionId" });
            DropColumn("dbo.Workers", "DivisionId");
            DropTable("dbo.Divisions");
        }
    }
}
