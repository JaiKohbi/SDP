namespace Journly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationOnJournalEntryClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JournalEntries", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.JournalEntries", "EntryBody", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JournalEntries", "EntryBody", c => c.String());
            AlterColumn("dbo.JournalEntries", "Title", c => c.String());
        }
    }
}
