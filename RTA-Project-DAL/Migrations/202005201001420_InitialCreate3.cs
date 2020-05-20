namespace RTA_Project_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "GuidKey", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "GuidKey", c => c.Guid(nullable: false));
        }
    }
}
