namespace LawnPaynes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedServiceandCustomerLocationValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerLocations", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Services", "ServiceName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Services", "ServiceName", c => c.String());
            AlterColumn("dbo.CustomerLocations", "Address", c => c.String());
        }
    }
}
