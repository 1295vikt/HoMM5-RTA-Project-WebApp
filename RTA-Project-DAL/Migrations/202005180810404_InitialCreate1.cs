namespace RTA_Project_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "AuthorId", c => c.String());
            DropColumn("dbo.Articles", "Author");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Author", c => c.String());
            DropColumn("dbo.Articles", "AuthorId");
        }
    }
}
