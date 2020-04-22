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
                        GuidKey = c.Guid(nullable: false),
                        AccountId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TournamentGroups", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentPlayers", "TournamentGroupId", "dbo.TournamentGroups");
            DropForeignKey("dbo.TournamentPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Matches", "TournamentGroupId", "dbo.TournamentGroups");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Matches", "Player2ID", "dbo.Players");
            DropForeignKey("dbo.Matches", "Player1ID", "dbo.Players");
            DropForeignKey("dbo.Games", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.Games", "Hero2Id", "dbo.Heroes");
            DropForeignKey("dbo.Games", "Hero1Id", "dbo.Heroes");
            DropForeignKey("dbo.Games", "Faction2Id", "dbo.Factions");
            DropForeignKey("dbo.Games", "Faction1Id", "dbo.Factions");
            DropForeignKey("dbo.Heroes", "FactionId", "dbo.Factions");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TournamentPlayers", new[] { "TournamentGroupId" });
            DropIndex("dbo.TournamentPlayers", new[] { "PlayerId" });
            DropIndex("dbo.TournamentGroups", new[] { "TournamentId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Matches", new[] { "Player2ID" });
            DropIndex("dbo.Matches", new[] { "Player1ID" });
            DropIndex("dbo.Matches", new[] { "TournamentGroupId" });
            DropIndex("dbo.Games", new[] { "Hero2Id" });
            DropIndex("dbo.Games", new[] { "Hero1Id" });
            DropIndex("dbo.Games", new[] { "Faction2Id" });
            DropIndex("dbo.Games", new[] { "Faction1Id" });
            DropIndex("dbo.Games", new[] { "MatchId" });
            DropIndex("dbo.Heroes", new[] { "FactionId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tournaments");
            DropTable("dbo.TournamentPlayers");
            DropTable("dbo.TournamentGroups");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Players");
            DropTable("dbo.Matches");
            DropTable("dbo.Games");
            DropTable("dbo.Heroes");
            DropTable("dbo.Factions");
        }
    }
}
