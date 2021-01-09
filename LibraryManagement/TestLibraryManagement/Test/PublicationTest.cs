// <copyright file="PublicationTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Publication Test class. </summary>
namespace TestLibraryManagement.Test
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using DataMapper.Repository;
    using DataMapper.Repository.DataBaseContext;
    using DomainModel.Model;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Service;

    /// <summary>
    /// Defines test class PublicationTest.
    /// </summary>
    [TestFixture]
    public class PublicationTest
    {
        /// <summary>
        /// Gets or sets the publication service.
        /// </summary>
        /// <value>The publication service.</value>
        private PublicationService PublicationService { get; set; }

        /// <summary>
        /// Gets or sets the library context mock.
        /// </summary>
        /// <value>The library context mock.</value>
        private Mock<LibraryManagementContext> LibraryContextMock { get; set; }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var mockSet = new Mock<DbSet<Publication>>();
            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Publication>()).Returns(mockSet.Object);
            this.PublicationService = new PublicationService(new PublicationRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddNullPublication.
        /// </summary>
        [Test]
        public void TestAddNullPublication()
        {
            Publication nullPublication = null;

            Assert.Throws<ArgumentNullException>(() => this.PublicationService.Create(nullPublication));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidCoverType.
        /// </summary>
        [Test]
        public void TestAddInvalidCoverType()
        {
            var publication = new Publication
            {
                CoverType = Cover.Invalid
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationCoverTypeInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidCoverType.
        /// </summary>
        /// <param name="validCoverType">Type of the valid cover.</param>
        [TestCase(Cover.HardCover)]
        [TestCase(Cover.PaperBack)]
        public void TestAddValidCoverType(Cover validCoverType)
        {
            var publication = new Publication
            {
                CoverType = validCoverType
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationCoverTypeInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNumberOfPagesLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddNumberOfPagesLowerBoundary()
        {
            var publication = new Publication
            {
                NumberOfPages = -1
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationNumberOfPagesInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNumberOfPagesUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddNumberOfPagesUpperBoundary()
        {
            var publication = new Publication
            {
                NumberOfPages = int.MaxValue
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationNumberOfPagesInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidNumberOfPages.
        /// </summary>
        [Test]
        public void TestAddValidNumberOfPages()
        {
            var publication = new Publication
            {
                NumberOfPages = 594
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationNumberOfPagesInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidPublicationDate.
        /// </summary>
        [Test]
        public void TestAddInvalidPublicationDate()
        {
            var publication = new Publication
            {
                PublicationDate = DateTime.Today.AddDays(1)
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateDateTime");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidPublicationDate.
        /// </summary>
        [Test]
        public void TestAddValidPublicationDate()
        {
            var publication = new Publication
            {
                PublicationDate = DateTime.Today.AddDays(-1)
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateDateTime");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullBook.
        /// </summary>
        [Test]
        public void TestAddNullBook()
        {
            var publication = new Publication
            {
                Book = null
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationBookNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidBook.
        /// </summary>
        [Test]
        public void TestAddValidBook()
        {
            var publication = new Publication
            {
                Book = new Book() { Name = "Origin" }
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationBookNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullStock.
        /// </summary>
        [Test]
        public void TestAddNullStock()
        {
            var publication = new Publication
            {
                Stock = null
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationStockNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidStock.
        /// </summary>
        [Test]
        public void TestAddValidStock()
        {
            var publication = new Publication
            {
                Stock = new Stock() { InitialStock = 5 }
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationStockNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullPublishingCompany.
        /// </summary>
        [Test]
        public void TestAddNullPublishingCompany()
        {
            var publication = new Publication
            {
                PublishingCompany = null
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationPublishingCompanyNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidPublishingCompany.
        /// </summary>
        [Test]
        public void TestAddValidPublishingCompany()
        {
            var publication = new Publication
            {
                PublishingCompany = new PublishingCompany() { Name = "RAO" }
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationPublishingCompanyNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullBookWithdrawals.
        /// </summary>
        [Test]
        public void TestAddNullBookWithdrawals()
        {
            var publication = new Publication
            {
                BookWithdrawals = null
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationBookWithdrawalsNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddEmptyBookWithdrawals.
        /// </summary>
        [Test]
        public void TestAddEmptyBookWithdrawals()
        {
            var publication = new Publication
            {
                BookWithdrawals = new List<Withdrawal>()
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationBookWithdrawalsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidBookWithdrawals.
        /// </summary>
        [Test]
        public void TestAddValidBookWithdrawals()
        {
            var publication = new Publication
            {
                BookWithdrawals = new List<Withdrawal> { new Withdrawal() }
            };

            var results = this.PublicationService.Create(publication);
            var tag = results.FirstOrDefault(res => res.Tag == "PublicationBookWithdrawalsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestPublicationGetterSetterId.
        /// </summary>
        [Test]
        public void TestPublicationGetterSetterId()
        {
            int id = 5;
            var publication = new Publication
            {
                CoverType = Cover.HardCover,
                NumberOfPages = 459,
                PublicationDate = DateTime.Today.AddDays(-1),
                Book = new Book(),
                Stock = new Stock(),
                PublishingCompany = new PublishingCompany(),
                BookWithdrawals = new List<Withdrawal>()
            };

            typeof(Publication).GetProperty(nameof(Publication.Id)).SetValue(publication, id);

            Assert.IsTrue(id == publication.Id);
        }

        /// <summary>
        /// Defines the test method TestAddPublication.
        /// </summary>
        [Test]
        public void TestAddPublication()
        {
            var publication = new Publication
            {
                CoverType = Cover.HardCover,
                NumberOfPages = 459,
                PublicationDate = DateTime.Today.AddDays(-1),
                Book = new Book(),
                Stock = new Stock(),
                PublishingCompany = new PublishingCompany(),
                BookWithdrawals = new List<Withdrawal>()
            };

            var results = this.PublicationService.Create(publication);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
