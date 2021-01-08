namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPublicationStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Extensions", "ExtraDays", c => c.Int(nullable: false));
            AddColumn("dbo.Stocks", "InitialStock", c => c.Int(nullable: false));
            AddColumn("dbo.Stocks", "RentedStock", c => c.Int(nullable: false));
            AddColumn("dbo.Stocks", "NumberOfBooksForLecture", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stocks", "NumberOfBooksForLecture");
            DropColumn("dbo.Stocks", "RentedStock");
            DropColumn("dbo.Stocks", "InitialStock");
            DropColumn("dbo.Extensions", "ExtraDays");
        }
    }
}
