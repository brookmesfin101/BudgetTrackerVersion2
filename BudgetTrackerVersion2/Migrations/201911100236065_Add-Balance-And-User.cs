namespace BudgetTrackerVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBalanceAndUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BalanceModels",
                c => new
                    {
                        BalanceID = c.Int(nullable: false),
                        Balance = c.Int(nullable: false),
                        BalanceDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BalanceID)
                .ForeignKey("dbo.UserModels", t => t.BalanceID)
                .Index(t => t.BalanceID);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BalanceModels", new[] { "BalanceID" });
            DropForeignKey("dbo.BalanceModels", "BalanceID", "dbo.UserModels");
            DropTable("dbo.UserModels");
            DropTable("dbo.BalanceModels");
        }
    }
}
