namespace Bloomfiy_final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderDateToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "CustomerName", c => c.String());
            AddColumn("dbo.Order", "Address", c => c.String());
            AddColumn("dbo.Order", "City", c => c.String());
            AddColumn("dbo.Order", "Phone", c => c.String());
            AddColumn("dbo.Order", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Date");
            DropColumn("dbo.Order", "Phone");
            DropColumn("dbo.Order", "City");
            DropColumn("dbo.Order", "Address");
            DropColumn("dbo.Order", "CustomerName");
        }
    }
}
