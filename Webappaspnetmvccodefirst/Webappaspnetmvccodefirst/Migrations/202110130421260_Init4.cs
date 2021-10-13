namespace Webappaspnetmvccodefirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Description", c => c.String(nullable: false));
        }
    }
}
