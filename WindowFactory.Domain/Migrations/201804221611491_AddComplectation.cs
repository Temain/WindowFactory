namespace WindowFactory.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComplectation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sale", "Complectation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sale", "Complectation");
        }
    }
}
