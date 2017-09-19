namespace Journly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InJournalChangeUserIdToUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Journals", "UserName", c => c.String(nullable: false));
            DropColumn("dbo.Journals", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Journals", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Journals", "UserName");
        }
    }
}
