namespace LawnPaynes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Email");
        }
    }
}
