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
                    })
                .PrimaryKey(t => t.CarId);
            
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Clients");
            DropTable("dbo.Cars");
            DropTable("dbo.CarRents");
        }
    }
}
