namespace LawnPaynes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCompositePrimaryKey : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ServiceCustomerLocations", newName: "ServiceCustomerLocation1");
            CreateTable(
                "dbo.ServiceCustomerLocations",
                c => new
                    {
                        CustomerLocationId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerLocationId, t.ServiceId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ServiceCustomerLocations");
            RenameTable(name: "dbo.ServiceCustomerLocation1", newName: "ServiceCustomerLocations");
        }
    }
}
