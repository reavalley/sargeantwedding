namespace BensWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attending : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Rsvps", "Attending_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Rsvps", "Attending_Id");
            AddForeignKey("dbo.Rsvps", "Attending_Id", "dbo.Attendings", "Id", cascadeDelete: true);
            DropColumn("dbo.Rsvps", "Attending");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rsvps", "Attending", c => c.Int(nullable: false));
            DropForeignKey("dbo.Rsvps", "Attending_Id", "dbo.Attendings");
            DropIndex("dbo.Rsvps", new[] { "Attending_Id" });
            DropColumn("dbo.Rsvps", "Attending_Id");
            DropTable("dbo.Attendings");
        }
    }
}
