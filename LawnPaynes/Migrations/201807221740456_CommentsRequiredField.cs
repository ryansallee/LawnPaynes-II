namespace LawnPaynes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsRequiredField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Comments", c => c.String(nullable: false, maxLength: 2000));
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "Comments", c => c.String(maxLength: 2000));
        }
    }
}
