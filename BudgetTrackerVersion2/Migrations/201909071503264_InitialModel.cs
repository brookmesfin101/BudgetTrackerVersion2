namespace BudgetTrackerVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncomeModels",
                c => new
                    {
                        IncomeId = c.Int(nullable: false, identity: true),
                        IncomeType = c.Int(nullable: false),
                        IncomeDescription = c.String(),
                        IncomeAmount = c.Int(nullable: false),
                        IncomeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IncomeId);
            
            CreateTable(
                "dbo.ExpenseModels",
                c => new
                    {
                        ExpenseId = c.Int(nullable: false, identity: true),
                        ExpenseType = c.Int(nullable: false),
                        ExpenseDescription = c.String(),
                        ExpenseAmount = c.Int(nullable: false),
                        ExpenseDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ExpenseId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExpenseModels");
            DropTable("dbo.IncomeModels");
        }
    }
}
