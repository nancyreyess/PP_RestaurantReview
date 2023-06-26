namespace PP_RestaurantReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cuisine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cuisines",
                c => new
                    {
                        CuisineId = c.Int(nullable: false, identity: true),
                        CuisineType = c.String(),
                    })
                .PrimaryKey(t => t.CuisineId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cuisines");
        }
    }
}
