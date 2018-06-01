namespace BlogApp.Repo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Description");
        }
    }
}
