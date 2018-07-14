namespace LawnPaynes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerLocationMaptoServicesJunctionTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ServiceCustomerLocations");
            CreateTable(
                "dbo.ServiceCustomerLocations",
                c => new
                    {
                        CustomerLocationId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerLocationId, t.ServiceId })
                .ForeignKey("dbo.CustomerLocations", t => t.CustomerLocationId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.CustomerLocationId)
                .Index(t => t.ServiceId);
            
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceCustomerLocations",
                c => new
                    {
                        Service_ServiceId = c.Int(nullable: false),
                        CustomerLocation_CustomerLocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_ServiceId, t.CustomerLocation_CustomerLocationId });
            
            DropForeignKey("dbo.ServiceCustomerLocations", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ServiceCustomerLocations", "CustomerLocationId", "dbo.CustomerLocations");
            DropIndex("dbo.ServiceCustomerLocations", new[] { "ServiceId" });
            DropIndex("dbo.ServiceCustomerLocations", new[] { "CustomerLocationId" });
            DropTable("dbo.ServiceCustomerLocations");
            CreateIndex("dbo.ServiceCustomerLocations", "CustomerLocation_CustomerLocationId");
            CreateIndex("dbo.ServiceCustomerLocations", "Service_ServiceId");
            AddForeignKey("dbo.ServiceCustomerLocations", "CustomerLocation_CustomerLocationId", "dbo.CustomerLocations", "CustomerLocationId", cascadeDelete: true);
            AddForeignKey("dbo.ServiceCustomerLocations", "Service_ServiceId", "dbo.Services", "ServiceId", cascadeDelete: true);
        }
    }
}
