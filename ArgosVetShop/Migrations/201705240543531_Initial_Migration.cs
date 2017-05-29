namespace ArgosVetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppointmentModels",
                c => new
                    {
                        AppoinmentID = c.Int(nullable: false, identity: true),
                        PetID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Diagnostic = c.String(),
                        Observations = c.String(),
                    })
                .PrimaryKey(t => t.AppoinmentID)
                .ForeignKey("dbo.PetModels", t => t.PetID, cascadeDelete: true)
                .ForeignKey("dbo.ServiceModels", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.PetID)
                .Index(t => t.ServiceID);
            
            CreateTable(
                "dbo.PetModels",
                c => new
                    {
                        PetID = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(maxLength: 128),
                        Name = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        Color = c.String(),
                        Age = c.Int(nullable: false),
                        Type = c.String(),
                        Breed = c.String(),
                        PhotoURL = c.String(),
                    })
                .PrimaryKey(t => t.PetID)
                .ForeignKey("dbo.User", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.User",
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
                        FullName = c.String(),
                        IsEnabled = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.ServiceModels",
                c => new
                    {
                        ServiceID = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                        ServiceDescription = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ServiceID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "IdentityUser_Id", "dbo.User");
            DropForeignKey("dbo.UserLogin", "IdentityUser_Id", "dbo.User");
            DropForeignKey("dbo.UserClaim", "IdentityUser_Id", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.AppointmentModels", "ServiceID", "dbo.ServiceModels");
            DropForeignKey("dbo.AppointmentModels", "PetID", "dbo.PetModels");
            DropForeignKey("dbo.PetModels", "OwnerId", "dbo.User");
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.UserRole", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserLogin", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserClaim", new[] { "IdentityUser_Id" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.PetModels", new[] { "OwnerId" });
            DropIndex("dbo.AppointmentModels", new[] { "ServiceID" });
            DropIndex("dbo.AppointmentModels", new[] { "PetID" });
            DropTable("dbo.Role");
            DropTable("dbo.ServiceModels");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.PetModels");
            DropTable("dbo.AppointmentModels");
        }
    }
}
