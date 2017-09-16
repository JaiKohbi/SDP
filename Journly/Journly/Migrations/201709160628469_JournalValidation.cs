namespace Journly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JournalValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Journals", "Title", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Journals", "Title", c => c.String());
        }
    }
}
