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
                        Title = c.String(),
                        ShortDescription = c.String(),
                        Content = c.String(),
                        Author = c.String(),
                        Date = c.DateTime(nullable: false),
                        IsGuideMaterial = c.Boolean(nullable: false),
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
                        Comment = c.String(),
                        MediaLink = c.String(),
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
                        DateFinished = c.DateTime(nullable: false),
                        Player1ID = c.Int(nullable: false),
                        Player2ID = c.Int(nullable: false),
                        Bracket_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TournamentBrackets", t => t.Bracket_Id)
                .ForeignKey("dbo.Players", t => t.Player1ID)
                .ForeignKey("dbo.Players", t => t.Player2ID)
                .ForeignKey("dbo.TournamentGroups", t => t.TournamentGroupId)
                .Index(t => t.TournamentGroupId)
                .Index(t => t.Player1ID)
                .Index(t => t.Player2ID)
                .Index(t => t.Bracket_Id);
            
            CreateTable(
                "dbo.TournamentBrackets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        BracketTag = c.String(),
                        NextBracketTagWinner = c.String(),
                        NextBracketTagLoser = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuidKey = c.Guid(nullable: false),
                        AccountId = c.Int(),
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
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        TournamentId = c.Int(nullable: false),
                        TournamentGroupId = c.Int(nullable: false),
                        Disqualified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .ForeignKey("dbo.TournamentGroups", t => t.TournamentGroupId)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId)
                .Index(t => t.PlayerId)
                .Index(t => t.TournamentId)
                .Index(t => t.TournamentGroupId);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameRus = c.String(),
                        NameEng = c.String(),
                        MapVersion = c.String(),
                        Year = c.Int(nullable: false),
                        Season = c.Byte(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsInPlayoffStage = c.Boolean(nullable: false),
                        IsFinished = c.Boolean(nullable: false),
                        IsOfficial = c.Boolean(nullable: false),
                        IsSeasonal = c.Boolean(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        Description_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TournamentDescriptions", t => t.Description_Id)
                .Index(t => t.Description_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TournamentPlayers", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentGroups", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Tournaments", "Description_Id", "dbo.TournamentDescriptions");
            DropForeignKey("dbo.TournamentPlayers", "TournamentGroupId", "dbo.TournamentGroups");
            DropForeignKey("dbo.TournamentPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Matches", "TournamentGroupId", "dbo.TournamentGroups");
            DropForeignKey("dbo.Matches", "Player2ID", "dbo.Players");
            DropForeignKey("dbo.Matches", "Player1ID", "dbo.Players");
            DropForeignKey("dbo.PlayerStats", "Id", "dbo.Players");
            DropForeignKey("dbo.Games", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.Matches", "Bracket_Id", "dbo.TournamentBrackets");
            DropForeignKey("dbo.Games", "Hero2Id", "dbo.Heroes");
            DropForeignKey("dbo.Games", "Hero1Id", "dbo.Heroes");
            DropIndex("dbo.Tournaments", new[] { "Description_Id" });
            DropIndex("dbo.TournamentPlayers", new[] { "TournamentGroupId" });
            DropIndex("dbo.TournamentPlayers", new[] { "TournamentId" });
            DropIndex("dbo.TournamentPlayers", new[] { "PlayerId" });
            DropIndex("dbo.TournamentGroups", new[] { "TournamentId" });
            DropIndex("dbo.PlayerStats", new[] { "Id" });
            DropIndex("dbo.Matches", new[] { "Bracket_Id" });
            DropIndex("dbo.Matches", new[] { "Player2ID" });
            DropIndex("dbo.Matches", new[] { "Player1ID" });
            DropIndex("dbo.Matches", new[] { "TournamentGroupId" });
            DropIndex("dbo.Games", new[] { "Hero2Id" });
            DropIndex("dbo.Games", new[] { "Hero1Id" });
            DropIndex("dbo.Games", new[] { "MatchId" });
            DropTable("dbo.Tournaments");
            DropTable("dbo.TournamentPlayers");
            DropTable("dbo.TournamentGroups");
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
