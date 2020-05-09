namespace RTA_Project_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Articles", "ShortDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "ShortDescription", c => c.String());
        }
    }
}
