namespace LawnPaynes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServicesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            CreateTable(
                "dbo.ServiceCustomerLocations",
                c => new
                    {
                        Service_ServiceId = c.Int(nullable: false),
                        CustomerLocation_CustomerLocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_ServiceId, t.CustomerLocation_CustomerLocationId })
                .ForeignKey("dbo.Services", t => t.Service_ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.CustomerLocations", t => t.CustomerLocation_CustomerLocationId, cascadeDelete: true)
                .Index(t => t.Service_ServiceId)
                .Index(t => t.CustomerLocation_CustomerLocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceCustomerLocations", "CustomerLocation_CustomerLocationId", "dbo.CustomerLocations");
            DropForeignKey("dbo.ServiceCustomerLocations", "Service_ServiceId", "dbo.Services");
            DropIndex("dbo.ServiceCustomerLocations", new[] { "CustomerLocation_CustomerLocationId" });
            DropIndex("dbo.ServiceCustomerLocations", new[] { "Service_ServiceId" });
            DropTable("dbo.ServiceCustomerLocations");
            DropTable("dbo.Services");
        }
    }
}
