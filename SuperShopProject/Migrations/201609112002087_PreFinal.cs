namespace SuperShopProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreFinal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserItems",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        ItemId = c.Guid(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ItemId })
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserItems", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserItems", "ItemId", "dbo.Items");
            DropIndex("dbo.UserItems", new[] { "ItemId" });
            DropIndex("dbo.UserItems", new[] { "UserId" });
            DropTable("dbo.UserItems");
            DropTable("dbo.Items");
        }
    }
}
