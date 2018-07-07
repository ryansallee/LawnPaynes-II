namespace LawnPaynes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerIdToCustomerCustomerId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Customers");
            DropColumn("dbo.Customers", "Id");
            AddColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Customers", "CustomerId");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Customers");
            DropColumn("dbo.Customers", "CustomerId");
            AddPrimaryKey("dbo.Customers", "Id");
        }
    }
}
