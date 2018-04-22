namespace WindowFactory.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWindowTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sale", "ProductId", "dbo.Product");
            DropIndex("dbo.Sale", new[] { "ProductId" });
            CreateTable(
                "dbo.WindowColor",
                c => new
                    {
                        WindowColorId = c.Int(nullable: false, identity: true),
                        WindowColorFirst = c.String(),
                        WindowColorSecond = c.String(),
                    })
                .PrimaryKey(t => t.WindowColorId);
            
            CreateTable(
                "dbo.WindowGlass",
                c => new
                    {
                        WindowGlassId = c.Int(nullable: false, identity: true),
                        WindowGlassName = c.String(),
                    })
                .PrimaryKey(t => t.WindowGlassId);
            
            CreateTable(
                "dbo.WindowGlazing",
                c => new
                    {
                        WindowGlazingId = c.Int(nullable: false, identity: true),
                        WindowGlazingName = c.String(),
                        WindowGlazingSize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WindowGlazingId);
            
            CreateTable(
                "dbo.WindowOpeningLimiter",
                c => new
                    {
                        WindowOpeningLimiterId = c.Int(nullable: false, identity: true),
                        WindowOpeningLimiterName = c.String(),
                    })
                .PrimaryKey(t => t.WindowOpeningLimiterId);
            
            CreateTable(
                "dbo.WindowProfile",
                c => new
                    {
                        WindowProfileId = c.Int(nullable: false, identity: true),
                        WindowProfileName = c.String(),
                    })
                .PrimaryKey(t => t.WindowProfileId);
            
            CreateTable(
                "dbo.WindowType",
                c => new
                    {
                        WindowTypeId = c.Int(nullable: false, identity: true),
                        WindowTypeName = c.String(),
                    })
                .PrimaryKey(t => t.WindowTypeId);
            
            AddColumn("dbo.Sale", "WindowTypeId", c => c.Int());
            AddColumn("dbo.Sale", "NumberOfFlaps", c => c.Int());
            AddColumn("dbo.Sale", "WindowProfileId", c => c.Int());
            AddColumn("dbo.Sale", "WindowColorId", c => c.Int());
            AddColumn("dbo.Sale", "WindowGlazingId", c => c.Int());
            AddColumn("dbo.Sale", "WindowGlassId", c => c.Int());
            AddColumn("dbo.Sale", "WindowOpeningLimiterId", c => c.Int());
            AddColumn("dbo.Sale", "Microvolving", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sale", "MosquitoNet", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sale", "WindowSill", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sale", "Drainage", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Sale", "WindowTypeId");
            CreateIndex("dbo.Sale", "WindowProfileId");
            CreateIndex("dbo.Sale", "WindowColorId");
            CreateIndex("dbo.Sale", "WindowGlazingId");
            CreateIndex("dbo.Sale", "WindowGlassId");
            CreateIndex("dbo.Sale", "WindowOpeningLimiterId");
            AddForeignKey("dbo.Sale", "WindowColorId", "dbo.WindowColor", "WindowColorId");
            AddForeignKey("dbo.Sale", "WindowGlassId", "dbo.WindowGlass", "WindowGlassId");
            AddForeignKey("dbo.Sale", "WindowGlazingId", "dbo.WindowGlazing", "WindowGlazingId");
            AddForeignKey("dbo.Sale", "WindowOpeningLimiterId", "dbo.WindowOpeningLimiter", "WindowOpeningLimiterId");
            AddForeignKey("dbo.Sale", "WindowProfileId", "dbo.WindowProfile", "WindowProfileId");
            AddForeignKey("dbo.Sale", "WindowTypeId", "dbo.WindowType", "WindowTypeId");
            DropColumn("dbo.Sale", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sale", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sale", "WindowTypeId", "dbo.WindowType");
            DropForeignKey("dbo.Sale", "WindowProfileId", "dbo.WindowProfile");
            DropForeignKey("dbo.Sale", "WindowOpeningLimiterId", "dbo.WindowOpeningLimiter");
            DropForeignKey("dbo.Sale", "WindowGlazingId", "dbo.WindowGlazing");
            DropForeignKey("dbo.Sale", "WindowGlassId", "dbo.WindowGlass");
            DropForeignKey("dbo.Sale", "WindowColorId", "dbo.WindowColor");
            DropIndex("dbo.Sale", new[] { "WindowOpeningLimiterId" });
            DropIndex("dbo.Sale", new[] { "WindowGlassId" });
            DropIndex("dbo.Sale", new[] { "WindowGlazingId" });
            DropIndex("dbo.Sale", new[] { "WindowColorId" });
            DropIndex("dbo.Sale", new[] { "WindowProfileId" });
            DropIndex("dbo.Sale", new[] { "WindowTypeId" });
            DropColumn("dbo.Sale", "Drainage");
            DropColumn("dbo.Sale", "WindowSill");
            DropColumn("dbo.Sale", "MosquitoNet");
            DropColumn("dbo.Sale", "Microvolving");
            DropColumn("dbo.Sale", "WindowOpeningLimiterId");
            DropColumn("dbo.Sale", "WindowGlassId");
            DropColumn("dbo.Sale", "WindowGlazingId");
            DropColumn("dbo.Sale", "WindowColorId");
            DropColumn("dbo.Sale", "WindowProfileId");
            DropColumn("dbo.Sale", "NumberOfFlaps");
            DropColumn("dbo.Sale", "WindowTypeId");
            DropTable("dbo.WindowType");
            DropTable("dbo.WindowProfile");
            DropTable("dbo.WindowOpeningLimiter");
            DropTable("dbo.WindowGlazing");
            DropTable("dbo.WindowGlass");
            DropTable("dbo.WindowColor");
            CreateIndex("dbo.Sale", "ProductId");
            AddForeignKey("dbo.Sale", "ProductId", "dbo.Product", "ProductId");
        }
    }
}
