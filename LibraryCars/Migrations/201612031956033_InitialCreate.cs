namespace LibraryCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarRents",
                c => new
                    {
                        CarRentId = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarRentId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        Model = c.String(),
                        Price = c.Int(nullable: false),
                        Rented = c.Boolean(nullable: false),
                        CarRent_CarRentId = c.Int(),
                    })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.CarRents", t => t.CarRent_CarRentId)
                .Index(t => t.CarRent_CarRentId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.ClientCarRents",
                c => new
                    {
                        Client_ClientId = c.Int(nullable: false),
                        CarRent_CarRentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Client_ClientId, t.CarRent_CarRentId })
                .ForeignKey("dbo.Clients", t => t.Client_ClientId, cascadeDelete: true)
                .ForeignKey("dbo.CarRents", t => t.CarRent_CarRentId, cascadeDelete: true)
                .Index(t => t.Client_ClientId)
                .Index(t => t.CarRent_CarRentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientCarRents", "CarRent_CarRentId", "dbo.CarRents");
            DropForeignKey("dbo.ClientCarRents", "Client_ClientId", "dbo.Clients");
            DropForeignKey("dbo.Cars", "CarRent_CarRentId", "dbo.CarRents");
            DropIndex("dbo.ClientCarRents", new[] { "CarRent_CarRentId" });
            DropIndex("dbo.ClientCarRents", new[] { "Client_ClientId" });
            DropIndex("dbo.Cars", new[] { "CarRent_CarRentId" });
            DropTable("dbo.ClientCarRents");
            DropTable("dbo.Clients");
            DropTable("dbo.Cars");
            DropTable("dbo.CarRents");
        }
    }
}
