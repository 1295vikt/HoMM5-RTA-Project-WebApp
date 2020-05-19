namespace RTA_Project_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TournamentPlayer1", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentPlayer1", "Player_Id", "dbo.Players");
            DropIndex("dbo.TournamentPlayer1", new[] { "Tournament_Id" });
            DropIndex("dbo.TournamentPlayer1", new[] { "Player_Id" });
            AddColumn("dbo.Matches", "IsSpecialMatch", c => c.Boolean(nullable: false));
            AddColumn("dbo.Matches", "Player1Score", c => c.Int(nullable: false));
            AddColumn("dbo.Matches", "Player2Score", c => c.Int(nullable: false));
            AddColumn("dbo.Tournaments", "HostsId", c => c.String());
            AddColumn("dbo.TournamentPlayers", "IsDisqualified", c => c.Boolean(nullable: false));
            AddColumn("dbo.TournamentPlayers", "GroupScore", c => c.Int(nullable: false));
            AddColumn("dbo.TournamentPlayers", "Coefficient", c => c.Single());
            DropColumn("dbo.TournamentPlayers", "Disqualified");
            DropTable("dbo.TournamentPlayer1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TournamentPlayer1",
                c => new
                    {
                        Tournament_Id = c.Int(nullable: false),
                        Player_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tournament_Id, t.Player_Id });
            
            AddColumn("dbo.TournamentPlayers", "Disqualified", c => c.Boolean(nullable: false));
            DropColumn("dbo.TournamentPlayers", "Coefficient");
            DropColumn("dbo.TournamentPlayers", "GroupScore");
            DropColumn("dbo.TournamentPlayers", "IsDisqualified");
            DropColumn("dbo.Tournaments", "HostsId");
            DropColumn("dbo.Matches", "Player2Score");
            DropColumn("dbo.Matches", "Player1Score");
            DropColumn("dbo.Matches", "IsSpecialMatch");
            CreateIndex("dbo.TournamentPlayer1", "Player_Id");
            CreateIndex("dbo.TournamentPlayer1", "Tournament_Id");
            AddForeignKey("dbo.TournamentPlayer1", "Player_Id", "dbo.Players", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TournamentPlayer1", "Tournament_Id", "dbo.Tournaments", "Id", cascadeDelete: true);
        }
    }
}
