namespace PP_RestaurantReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restaurant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false, identity: true),
                        RestaurantName = c.String(),
                        CuisineId = c.Int(nullable: false),
                        Description = c.String(),
                        Address = c.String(),
                        RestaurantLink = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Restaurants");
        }
    }
}
