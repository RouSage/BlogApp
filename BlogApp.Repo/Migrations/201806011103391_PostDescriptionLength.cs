namespace BlogApp.Repo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostDescriptionLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
