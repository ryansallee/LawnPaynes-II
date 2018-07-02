namespace LawnPaynes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Customer", newName: "Customers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Customers", newName: "Customer");
        }
    }
}
