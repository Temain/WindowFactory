namespace WindowFactory.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWindowTables2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOfHouse",
                c => new
                    {
                        TypeOfHouseId = c.Int(nullable: false, identity: true),
                        TypeOfHouseName = c.String(),
                    })
                .PrimaryKey(t => t.TypeOfHouseId);
            
            AddColumn("dbo.Sale", "FirstWidth", c => c.Int(nullable: false));
            AddColumn("dbo.Sale", "SecondWidth", c => c.Int(nullable: false));
            AddColumn("dbo.Sale", "ThirdWidth", c => c.Int(nullable: false));
            AddColumn("dbo.Sale", "FirstHeight", c => c.Int(nullable: false));
            AddColumn("dbo.Sale", "SecondHeight", c => c.Int(nullable: false));
            AddColumn("dbo.Sale", "WindowInstallation", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sale", "SlopeFinishing", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sale", "TypeOfHouseId", c => c.Int());
            AddColumn("dbo.Sale", "Cost", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.Sale", "TypeOfHouseId");
            AddForeignKey("dbo.Sale", "TypeOfHouseId", "dbo.TypeOfHouse", "TypeOfHouseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sale", "TypeOfHouseId", "dbo.TypeOfHouse");
            DropIndex("dbo.Sale", new[] { "TypeOfHouseId" });
            DropColumn("dbo.Sale", "Cost");
            DropColumn("dbo.Sale", "TypeOfHouseId");
            DropColumn("dbo.Sale", "SlopeFinishing");
            DropColumn("dbo.Sale", "WindowInstallation");
            DropColumn("dbo.Sale", "SecondHeight");
            DropColumn("dbo.Sale", "FirstHeight");
            DropColumn("dbo.Sale", "ThirdWidth");
            DropColumn("dbo.Sale", "SecondWidth");
            DropColumn("dbo.Sale", "FirstWidth");
            DropTable("dbo.TypeOfHouse");
        }
    }
}
