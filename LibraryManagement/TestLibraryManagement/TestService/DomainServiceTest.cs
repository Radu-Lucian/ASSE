// <copyright file="DomainServiceTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Domain Service Test class. </summary>
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
    /// Defines test class DomainServiceTest.
    /// </summary>
    [TestFixture]
    public class DomainServiceTest
    {
        /// <summary>
        /// Gets or sets the domain service.
        /// </summary>
        /// <value>The domain service.</value>
        private DomainService DomainService { get; set; }

        /// <summary>
        /// Gets or sets the library context mock.
        /// </summary>
        /// <value>The library context mock.</value>
        private Mock<LibraryManagementContext> LibraryContextMock { get; set; }

        /// <summary>
        /// Gets or sets the domain list.
        /// </summary>
        /// <value>The domain list.</value>
        private List<Domain> DomainList { get; set; }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.DomainList =
                new List<Domain>
           {
                    new Domain
                    {
                        Name = "Science",
                        Books = new List<Book>()
                    },
                    new Domain
                    {
                        Name = "Fiction",
                        Books = new List<Book>()
                    },
            };
            var queryable = this.DomainList.AsQueryable();

            var mockSet = new Mock<DbSet<Domain>>();
            mockSet.As<IQueryable<Domain>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Domain>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<Domain>())).Callback<Domain>((entity) => this.DomainList.Add(entity));
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => this.DomainList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Domain>())).Callback<Domain>((entity) => this.DomainList.Remove(entity));
            mockSet.Setup(m => m.AsNoTracking()).Returns(mockSet.Object);

            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Domain>()).Returns(mockSet.Object);
            this.DomainService = new DomainService(new DomainRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddDomain.
        /// </summary>
        [Test]
        public void TestAddDomainWithValidData()
        {
            var domain = new Domain
            {
                Name = "Informatics",
                Books = new List<Book>()
            };

            var results = this.DomainService.Create(domain);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddDomainWithInvalidData.
        /// </summary>
        [Test]
        public void TestAddDomainWithInvalidData()
        {
            var domain = new Domain();

            var results = this.DomainService.Create(domain);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteDomainWithValidData.
        /// </summary>
        [Test]
        public void TestDeleteDomainWithValidData()
        {
            var domain = this.DomainList.ElementAt(0);

            var results = this.DomainService.Delete(domain);

            Assert.IsEmpty(results);
            Assert.IsTrue(this.DomainList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteDomainWithInvalidData.
        /// </summary>
        [Test]
        public void TestDeleteDomainWithInvalidData()
        {
            var domain = new Domain();

            var results = this.DomainService.Delete(domain);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteDomainWithNullData.
        /// </summary>
        [Test]
        public void TestDeleteDomainWithNullData()
        {
            Domain nullDomain = null;

            Assert.Throws<ArgumentNullException>(() => this.DomainService.Delete(nullDomain));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteExistingDomainWithId.
        /// </summary>
        [Test]
        public void TestDeleteExistingDomainWithId()
        {
            this.DomainService.Delete(0);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
            Assert.IsTrue(this.DomainList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteInvalidDomainWithId.
        /// </summary>
        [Test]
        public void TestDeleteInvalidDomainWithId()
        {
            this.DomainService.Delete(55);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateDomainWithValidData.
        /// </summary>
        [Test]
        public void TestUpdateDomainWithValidData()
        {
            var domain = this.DomainList.ElementAt(0);

            domain.Name = "Art";

            var results = this.DomainService.Update(domain);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestUpdateDomainWithInvalidData.
        /// </summary>
        [Test]
        public void TestUpdateDomainWithInvalidData()
        {
            var domain = this.DomainList.ElementAt(0);

            domain.Name = string.Empty;

            var results = this.DomainService.Update(domain);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateDomainWithNullData.
        /// </summary>
        [Test]
        public void TestUpdateDomainWithNullData()
        {
            Domain nullDomain = null;

            Assert.Throws<ArgumentNullException>(() => this.DomainService.Update(nullDomain));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestFindValidDomainWithId.
        /// </summary>
        [Test]
        public void TestFindValidDomainWithId()
        {
            var domain = this.DomainService.Find(0);

            Assert.IsNotNull(domain);
        }

        /// <summary>
        /// Defines the test method TestFindInvalidDomainWithId.
        /// </summary>
        [Test]
        public void TestFindInvalidDomainWithId()
        {
            var domain = this.DomainService.Find(55);

            Assert.IsNull(domain);
        }

        /// <summary>
        /// Defines the test method TestFindAllDomains.
        /// </summary>
        [Test]
        public void TestFindAllDomains()
        {
            var domains = this.DomainService.FindAll();

            Assert.IsTrue(domains.Count() == this.DomainList.Count);
        }
    }
}
