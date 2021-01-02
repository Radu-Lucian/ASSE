namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Domains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CoverType = c.Int(nullable: false),
                        NumberOfPages = c.Int(nullable: false),
                        PublicationDate = c.DateTime(nullable: false),
                        Book_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .Index(t => t.Book_Id);
            
            CreateTable(
                "dbo.Withdrawals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RentedDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Reader_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Readers", t => t.Reader_Id)
                .Index(t => t.Reader_Id);
            
            CreateTable(
                "dbo.Extensions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExtraDays = c.Int(nullable: false),
                        Withdrawal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Withdrawals", t => t.Withdrawal_Id)
                .Index(t => t.Withdrawal_Id);
            
            CreateTable(
                "dbo.Readers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Adress = c.String(),
                        PhoneNumber = c.String(),
                        EmailAddress = c.String(),
                        Gender = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publications", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Book_Id = c.Int(nullable: false),
                        Author_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_Id, t.Author_Id })
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Book_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.DomainBooks",
                c => new
                    {
                        Domain_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Domain_Id, t.Book_Id })
                .ForeignKey("dbo.Domains", t => t.Domain_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Domain_Id)
                .Index(t => t.Book_Id);
            
            CreateTable(
                "dbo.WithdrawalPublications",
                c => new
                    {
                        Withdrawal_Id = c.Int(nullable: false),
                        Publication_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Withdrawal_Id, t.Publication_Id })
                .ForeignKey("dbo.Withdrawals", t => t.Withdrawal_Id, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_Id, cascadeDelete: true)
                .Index(t => t.Withdrawal_Id)
                .Index(t => t.Publication_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "Id", "dbo.Publications");
            DropForeignKey("dbo.Withdrawals", "Reader_Id", "dbo.Readers");
            DropForeignKey("dbo.WithdrawalPublications", "Publication_Id", "dbo.Publications");
            DropForeignKey("dbo.WithdrawalPublications", "Withdrawal_Id", "dbo.Withdrawals");
            DropForeignKey("dbo.Extensions", "Withdrawal_Id", "dbo.Withdrawals");
            DropForeignKey("dbo.Publications", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.Domains", "Parent_Id", "dbo.Domains");
            DropForeignKey("dbo.DomainBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.DomainBooks", "Domain_Id", "dbo.Domains");
            DropForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_Id", "dbo.Books");
            DropIndex("dbo.WithdrawalPublications", new[] { "Publication_Id" });
            DropIndex("dbo.WithdrawalPublications", new[] { "Withdrawal_Id" });
            DropIndex("dbo.DomainBooks", new[] { "Book_Id" });
            DropIndex("dbo.DomainBooks", new[] { "Domain_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Author_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Book_Id" });
            DropIndex("dbo.Stocks", new[] { "Id" });
            DropIndex("dbo.Extensions", new[] { "Withdrawal_Id" });
            DropIndex("dbo.Withdrawals", new[] { "Reader_Id" });
            DropIndex("dbo.Publications", new[] { "Book_Id" });
            DropIndex("dbo.Domains", new[] { "Parent_Id" });
            DropTable("dbo.WithdrawalPublications");
            DropTable("dbo.DomainBooks");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.Stocks");
            DropTable("dbo.Readers");
            DropTable("dbo.Extensions");
            DropTable("dbo.Withdrawals");
            DropTable("dbo.Publications");
            DropTable("dbo.Domains");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
