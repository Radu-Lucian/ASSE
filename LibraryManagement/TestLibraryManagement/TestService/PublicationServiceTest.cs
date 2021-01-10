// <copyright file="PublicationServiceTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Publication Service Test class. </summary>
namespace TestLibraryManagement.TestService
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
    /// Defines test class PublicationServiceTest.
    /// </summary>
    [TestFixture]
    public class PublicationServiceTest
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
        /// Gets or sets the publication list.
        /// </summary>
        /// <value>The publication list.</value>
        private List<Publication> PublicationList { get; set; }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.PublicationList =
                new List<Publication>
           {
                    new Publication
                    {
                        CoverType = Cover.PaperBack,
                        NumberOfPages = 459,
                        PublicationDate = DateTime.Today.AddDays(-60),
                        Book = new Book(),
                        Stock = new Stock(),
                        PublishingCompany = new PublishingCompany(),
                        BookWithdrawals = new List<Withdrawal>()
                    },
                    new Publication
                    {
                        CoverType = Cover.HardCover,
                        NumberOfPages = 359,
                        PublicationDate = DateTime.Today.AddDays(-100),
                        Book = new Book(),
                        Stock = new Stock(),
                        PublishingCompany = new PublishingCompany(),
                        BookWithdrawals = new List<Withdrawal>()
                    },
            };

            typeof(Publication).GetProperty(nameof(Publication.Id)).SetValue(this.PublicationList[0], 0);
            typeof(Publication).GetProperty(nameof(Publication.Id)).SetValue(this.PublicationList[1], 1);

            var queryable = this.PublicationList.AsQueryable();

            var mockSet = new Mock<DbSet<Publication>>();
            mockSet.As<IQueryable<Publication>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Publication>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Publication>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Publication>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<Publication>())).Callback<Publication>((entity) => this.PublicationList.Add(entity));
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => this.PublicationList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Publication>())).Callback<Publication>((entity) => this.PublicationList.Remove(entity));
            mockSet.Setup(m => m.AsNoTracking()).Returns(mockSet.Object);

            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Publication>()).Returns(mockSet.Object);
            this.PublicationService = new PublicationService(new PublicationRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddPublication.
        /// </summary>
        [Test]
        public void TestAddPublicationWithValidData()
        {
            var publication = new Publication
            {
                CoverType = Cover.HardCover,
                NumberOfPages = 768,
                PublicationDate = DateTime.Today.AddDays(-12),
                Book = new Book(),
                Stock = new Stock(),
                PublishingCompany = new PublishingCompany(),
                BookWithdrawals = new List<Withdrawal>()
            };

            var results = this.PublicationService.Create(publication);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddPublicationWithInvalidData.
        /// </summary>
        [Test]
        public void TestAddPublicationWithInvalidData()
        {
            var publication = new Publication();

            var results = this.PublicationService.Create(publication);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeletePublicationWithValidData.
        /// </summary>
        [Test]
        public void TestDeletePublicationWithValidData()
        {
            var publication = this.PublicationList.ElementAt(0);

            var results = this.PublicationService.Delete(publication);

            Assert.IsEmpty(results);
            Assert.IsTrue(this.PublicationList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeletePublicationWithInvalidData.
        /// </summary>
        [Test]
        public void TestDeletePublicationWithInvalidData()
        {
            var publication = new Publication();

            var results = this.PublicationService.Delete(publication);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeletePublicationWithNullData.
        /// </summary>
        [Test]
        public void TestDeletePublicationWithNullData()
        {
            Publication nullPublication = null;

            Assert.Throws<ArgumentNullException>(() => this.PublicationService.Delete(nullPublication));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteExistingPublicationWithId.
        /// </summary>
        [Test]
        public void TestDeleteExistingPublicationWithId()
        {
            this.PublicationService.Delete(0);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
            Assert.IsTrue(this.PublicationList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteInvalidPublicationWithId.
        /// </summary>
        [Test]
        public void TestDeleteInvalidPublicationWithId()
        {
            this.PublicationService.Delete(55);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdatePublicationWithValidData.
        /// </summary>
        [Test]
        public void TestUpdatePublicationWithValidData()
        {
            var publication = this.PublicationList.ElementAt(0);

            publication.CoverType = Cover.HardCover;

            var results = this.PublicationService.Update(publication);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestUpdatePublicationWithInvalidData.
        /// </summary>
        [Test]
        public void TestUpdatePublicationWithInvalidData()
        {
            var publication = this.PublicationList.ElementAt(0);

            publication.CoverType = Cover.Invalid;

            var results = this.PublicationService.Update(publication);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdatePublicationWithNullData.
        /// </summary>
        [Test]
        public void TestUpdatePublicationWithNullData()
        {
            Publication nullPublication = null;

            Assert.Throws<ArgumentNullException>(() => this.PublicationService.Update(nullPublication));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestFindValidPublicationWithId.
        /// </summary>
        [Test]
        public void TestFindValidPublicationWithId()
        {
            var publication = this.PublicationService.Find(0);

            Assert.IsNotNull(publication);
        }

        /// <summary>
        /// Defines the test method TestFindInvalidPublicationWithId.
        /// </summary>
        [Test]
        public void TestFindInvalidPublicationWithId()
        {
            var publication = this.PublicationService.Find(55);

            Assert.IsNull(publication);
        }

        /// <summary>
        /// Defines the test method TestFindAllPublications.
        /// </summary>
        [Test]
        public void TestFindAllPublications()
        {
            var publications = this.PublicationService.FindAll();

            Assert.IsTrue(publications.Count() == this.PublicationList.Count);
        }
    }
}
