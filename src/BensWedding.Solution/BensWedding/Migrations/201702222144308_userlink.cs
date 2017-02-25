namespace BensWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userlink : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rsvps", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Rsvps", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Rsvps", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Rsvps", name: "UserId", newName: "User_Id");
        }
    }
}
