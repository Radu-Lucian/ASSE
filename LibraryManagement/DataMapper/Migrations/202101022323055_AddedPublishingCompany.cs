namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPublishingCompany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PublishingCompanies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Publications", "PublishingCompany_Id", c => c.Int());
            AddColumn("dbo.Extensions", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Readers", "Address", c => c.String());
            CreateIndex("dbo.Publications", "PublishingCompany_Id");
            AddForeignKey("dbo.Publications", "PublishingCompany_Id", "dbo.PublishingCompanies", "Id");
            DropColumn("dbo.Publications", "Name");
            DropColumn("dbo.Extensions", "ExtraDays");
            DropColumn("dbo.Readers", "Adress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Readers", "Adress", c => c.String());
            AddColumn("dbo.Extensions", "ExtraDays", c => c.Int(nullable: false));
            AddColumn("dbo.Publications", "Name", c => c.String());
            DropForeignKey("dbo.Publications", "PublishingCompany_Id", "dbo.PublishingCompanies");
            DropIndex("dbo.Publications", new[] { "PublishingCompany_Id" });
            DropColumn("dbo.Readers", "Address");
            DropColumn("dbo.Extensions", "CreationDate");
            DropColumn("dbo.Publications", "PublishingCompany_Id");
            DropTable("dbo.PublishingCompanies");
        }
    }
}
