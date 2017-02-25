namespace BensWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rsvp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rsvps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Attending = c.Int(nullable: false),
                        IsCamping = c.Boolean(nullable: false),
                        MenuOption_Id = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuOptions", t => t.MenuOption_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.MenuOption_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rsvps", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rsvps", "MenuOption_Id", "dbo.MenuOptions");
            DropIndex("dbo.Rsvps", new[] { "User_Id" });
            DropIndex("dbo.Rsvps", new[] { "MenuOption_Id" });
            DropTable("dbo.Rsvps");
            DropTable("dbo.MenuOptions");
        }
    }
}
