namespace Bloomfiy_final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserDetails1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            DropColumn("dbo.AspNetUsers", "FullAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "FullAddress", c => c.String());
            DropColumn("dbo.AspNetUsers", "Address");
        }
    }
}
