namespace BlogApp.Repo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryFrequence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Frequence", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Frequence");
        }
    }
}
