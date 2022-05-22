namespace IntersheepDeeplay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnot_AddJobPositionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(maxLength: 120),
                        Description2 = c.String(maxLength: 120),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Workers", "JobPositionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Divisions", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Workers", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Workers", "LastName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Workers", "JobPositionId");
            AddForeignKey("dbo.Workers", "JobPositionId", "dbo.JobPositions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "JobPositionId", "dbo.JobPositions");
            DropIndex("dbo.Workers", new[] { "JobPositionId" });
            AlterColumn("dbo.Workers", "LastName", c => c.String());
            AlterColumn("dbo.Workers", "FirstName", c => c.String());
            AlterColumn("dbo.Divisions", "Name", c => c.String());
            DropColumn("dbo.Workers", "JobPositionId");
            DropTable("dbo.JobPositions");
        }
    }
}
