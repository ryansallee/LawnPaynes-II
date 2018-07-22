namespace LawnPaynes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerPhoneRegExUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerLocations", "Address", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Customers", "Comments", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Services", "ServiceName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Services", "ServiceName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Comments", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerLocations", "Address", c => c.String(nullable: false));
        }
    }
}
