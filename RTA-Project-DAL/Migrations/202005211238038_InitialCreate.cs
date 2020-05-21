namespace RTA_Project_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LangId = c.Byte(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        AuthorId = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        ReportingPlayerId = c.Int(nullable: false),
                        DateSubmitted = c.DateTime(nullable: false),
                        ReportingPlayerWon = c.Boolean(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        IsTechnicalWin = c.Boolean(nullable: false),
                        Faction1Id = c.Byte(nullable: false),
                        Faction2Id = c.Byte(nullable: false),
                        Hero1Id = c.Int(nullable: false),
                        Hero2Id = c.Int(nullable: false),
                        Player1Comment = c.String(),
                        Player2Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Heroes", t => t.Hero1Id)
                .ForeignKey("dbo.Heroes", t => t.Hero2Id)
                .ForeignKey("dbo.Matches", t => t.MatchId)
                .Index(t => t.MatchId)
                .Index(t => t.Hero1Id)
                .Index(t => t.Hero2Id);
            
            CreateTable(
                "dbo.Heroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FactionId = c.Byte(nullable: false),
                        NameRus = c.String(),
                        NameEng = c.String(),
                        IsRemovedFromHeroPool = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentGroupId = c.Int(nullable: false),
                        NumberOfGames = c.Int(nullable: false),
                        IsBestOfFormat = c.Boolean(nullable: false),
                        IsFinished = c.Boolean(nullable: false),
                        IsCancelled = c.Boolean(nullable: false),
                        IsTechnicalResult = c.Boolean(nullable: false),
                        IsSpecialMatch = c.Boolean(nullable: false),
                        DateFinished = c.DateTime(nullable: false),
                        Player1ID = c.Int(nullable: false),
                        Player2ID = c.Int(nullable: false),
                        Player1Score = c.Int(nullable: false),
                        Player2Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Player1ID)
                .ForeignKey("dbo.Players", t => t.Player2ID)
                .ForeignKey("dbo.TournamentGroups", t => t.TournamentGroupId)
                .Index(t => t.TournamentGroupId)
                .Index(t => t.Player1ID)
                .Index(t => t.Player2ID);
            
            CreateTable(
                "dbo.TournamentBrackets",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        BracketTag = c.String(),
                        NextBracketTagWinner = c.String(),
                        NextBracketTagLoser = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Matches", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuidKey = c.String(),
                        AccountId = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlayerStats",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RatingClass = c.String(),
                        RatingPointsCurrent = c.Int(nullable: false),
                        RatingPointsMax = c.Int(nullable: false),
                        GoldMedals = c.Int(nullable: false),
                        SilverMedals = c.Int(nullable: false),
                        BronzeMedals = c.Int(nullable: false),
                        TournamentExperience = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.TournamentDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameRus = c.String(),
                        NameEng = c.String(),
                        MapVersionId = c.String(maxLength: 128),
                        Year = c.Int(nullable: false),
                        Season = c.Byte(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsInSecondStage = c.Boolean(nullable: false),
                        IsFinished = c.Boolean(nullable: false),
                        IsOfficial = c.Boolean(nullable: false),
                        IsSeasonal = c.Boolean(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        HostsId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Maps", t => t.MapVersionId)
                .Index(t => t.MapVersionId);
            
            CreateTable(
                "dbo.Maps",
                c => new
                    {
                        Version = c.String(nullable: false, maxLength: 128),
                        ChangelogRus = c.String(),
                        ChangelogEng = c.String(),
                        DownloadLinkRus = c.String(),
                        DownloadLinkEng = c.String(),
                    })
                .PrimaryKey(t => t.Version);
            
            CreateTable(
                "dbo.TournamentGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        GroupFormatId = c.Byte(nullable: false),
                        NameRus = c.String(),
                        NameEng = c.String(),
                        IsFinished = c.Boolean(nullable: false),
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
                        IsDisqualified = c.Boolean(nullable: false),
                        GroupScore = c.Int(nullable: false),
                        Coefficient = c.Single(),
                        TournamentGroup_Id = c.Int(),
                        Tournament_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .ForeignKey("dbo.TournamentGroups", t => t.TournamentGroup_Id)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id)
                .Index(t => t.PlayerId)
                .Index(t => t.TournamentGroup_Id)
                .Index(t => t.Tournament_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TournamentDescriptions", "Id", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentPlayers", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentGroups", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentPlayers", "TournamentGroup_Id", "dbo.TournamentGroups");
            DropForeignKey("dbo.TournamentPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Matches", "TournamentGroupId", "dbo.TournamentGroups");
            DropForeignKey("dbo.Tournaments", "MapVersionId", "dbo.Maps");
            DropForeignKey("dbo.Matches", "Player2ID", "dbo.Players");
            DropForeignKey("dbo.Matches", "Player1ID", "dbo.Players");
            DropForeignKey("dbo.PlayerStats", "Id", "dbo.Players");
            DropForeignKey("dbo.Games", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.TournamentBrackets", "Id", "dbo.Matches");
            DropForeignKey("dbo.Games", "Hero2Id", "dbo.Heroes");
            DropForeignKey("dbo.Games", "Hero1Id", "dbo.Heroes");
            DropIndex("dbo.TournamentPlayers", new[] { "Tournament_Id" });
            DropIndex("dbo.TournamentPlayers", new[] { "TournamentGroup_Id" });
            DropIndex("dbo.TournamentPlayers", new[] { "PlayerId" });
            DropIndex("dbo.TournamentGroups", new[] { "TournamentId" });
            DropIndex("dbo.Tournaments", new[] { "MapVersionId" });
            DropIndex("dbo.TournamentDescriptions", new[] { "Id" });
            DropIndex("dbo.PlayerStats", new[] { "Id" });
            DropIndex("dbo.TournamentBrackets", new[] { "Id" });
            DropIndex("dbo.Matches", new[] { "Player2ID" });
            DropIndex("dbo.Matches", new[] { "Player1ID" });
            DropIndex("dbo.Matches", new[] { "TournamentGroupId" });
            DropIndex("dbo.Games", new[] { "Hero2Id" });
            DropIndex("dbo.Games", new[] { "Hero1Id" });
            DropIndex("dbo.Games", new[] { "MatchId" });
            DropTable("dbo.TournamentPlayers");
            DropTable("dbo.TournamentGroups");
            DropTable("dbo.Maps");
            DropTable("dbo.Tournaments");
            DropTable("dbo.TournamentDescriptions");
            DropTable("dbo.PlayerStats");
            DropTable("dbo.Players");
            DropTable("dbo.TournamentBrackets");
            DropTable("dbo.Matches");
            DropTable("dbo.Heroes");
            DropTable("dbo.Games");
            DropTable("dbo.Articles");
        }
    }
}
