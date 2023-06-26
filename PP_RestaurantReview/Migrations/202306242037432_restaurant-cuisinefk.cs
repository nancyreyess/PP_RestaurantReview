namespace PP_RestaurantReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restaurantcuisinefk : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Restaurants", "CuisineId");
            AddForeignKey("dbo.Restaurants", "CuisineId", "dbo.Cuisines", "CuisineId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurants", "CuisineId", "dbo.Cuisines");
            DropIndex("dbo.Restaurants", new[] { "CuisineId" });
        }
    }
}
