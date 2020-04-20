namespace RTA_Project_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Factions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameRus = c.String(),
                        NameEng = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Heroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FactionId = c.Int(nullable: false),
                        NameRus = c.String(),
                        NameEng = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Factions", t => t.FactionId)
                .Index(t => t.FactionId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        ReportingPlayerId = c.Int(nullable: false),
                        DateSubmitted = c.DateTime(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        ReportingPlayerWon = c.Boolean(nullable: false),
                        IsTechnicalWin = c.Boolean(nullable: false),
                        Faction1Id = c.Int(nullable: false),
                        Faction2Id = c.Int(nullable: false),
                        Hero1Id = c.Int(nullable: false),
                        Hero2Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Factions", t => t.Faction1Id)
                .ForeignKey("dbo.Factions", t => t.Faction2Id)
                .ForeignKey("dbo.Heroes", t => t.Hero1Id)
                .ForeignKey("dbo.Heroes", t => t.Hero2Id)
                .ForeignKey("dbo.Matches", t => t.MatchId)
                .Index(t => t.MatchId)
                .Index(t => t.Faction1Id)
                .Index(t => t.Faction2Id)
                .Index(t => t.Hero1Id)
                .Index(t => t.Hero2Id);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentGroupId = c.Int(nullable: false),
                        IsFinished = c.Boolean(nullable: false),
                        DateFinished = c.DateTime(nullable: false),
                        Player1ID = c.Int(nullable: false),
                        Player2ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Player1ID)
                .ForeignKey("dbo.Players", t => t.Player2ID)
                .ForeignKey("dbo.TournamentGroups", t => t.TournamentGroupId)
                .Index(t => t.TournamentGroupId)
                .Index(t => t.Player1ID)
                .Index(t => t.Player2ID);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TournamentGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        NameRus = c.String(),
                        NameEng = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId)
                .Index(t => t.TournamentId);
            
            CreateTable(
                "dbo.TournamentPlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        TournamentGroupId = c.Int(nullable: false),
                        Disqualified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .ForeignKey("dbo.TournamentGroups", t => t.TournamentGroupId)
                .Index(t => t.PlayerId)
                .Index(t => t.TournamentGroupId);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameRus = c.String(),
                        NameEng = c.String(),
                        Year = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsFinished = c.Boolean(nullable: false),
                        IsOfficial = c.Boolean(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TournamentGroups", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentPlayers", "TournamentGroupId", "dbo.TournamentGroups");
            DropForeignKey("dbo.TournamentPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Matches", "TournamentGroupId", "dbo.TournamentGroups");
            DropForeignKey("dbo.Matches", "Player2ID", "dbo.Players");
            DropForeignKey("dbo.Matches", "Player1ID", "dbo.Players");
            DropForeignKey("dbo.Games", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.Games", "Hero2Id", "dbo.Heroes");
            DropForeignKey("dbo.Games", "Hero1Id", "dbo.Heroes");
            DropForeignKey("dbo.Games", "Faction2Id", "dbo.Factions");
            DropForeignKey("dbo.Games", "Faction1Id", "dbo.Factions");
            DropForeignKey("dbo.Heroes", "FactionId", "dbo.Factions");
            DropIndex("dbo.TournamentPlayers", new[] { "TournamentGroupId" });
            DropIndex("dbo.TournamentPlayers", new[] { "PlayerId" });
            DropIndex("dbo.TournamentGroups", new[] { "TournamentId" });
            DropIndex("dbo.Matches", new[] { "Player2ID" });
            DropIndex("dbo.Matches", new[] { "Player1ID" });
            DropIndex("dbo.Matches", new[] { "TournamentGroupId" });
            DropIndex("dbo.Games", new[] { "Hero2Id" });
            DropIndex("dbo.Games", new[] { "Hero1Id" });
            DropIndex("dbo.Games", new[] { "Faction2Id" });
            DropIndex("dbo.Games", new[] { "Faction1Id" });
            DropIndex("dbo.Games", new[] { "MatchId" });
            DropIndex("dbo.Heroes", new[] { "FactionId" });
            DropTable("dbo.Tournaments");
            DropTable("dbo.TournamentPlayers");
            DropTable("dbo.TournamentGroups");
            DropTable("dbo.Players");
            DropTable("dbo.Matches");
            DropTable("dbo.Games");
            DropTable("dbo.Heroes");
            DropTable("dbo.Factions");
        }
    }
}
