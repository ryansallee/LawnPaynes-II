namespace LawnPaynes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCustomerLocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerLocations",
                c => new
                    {
                        CustomerLocationId = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerLocationId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerLocations", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CustomerLocations", new[] { "CustomerId" });
            DropTable("dbo.CustomerLocations");
        }
    }
}
