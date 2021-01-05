// <copyright file="DomainTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Domain Test class. </summary>
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
    /// Defines test class DomainTest.
    /// </summary>
    [TestFixture]
    public class DomainTest
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
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var mockSet = new Mock<DbSet<Domain>>();
            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Domain>()).Returns(mockSet.Object);
            this.DomainService = new DomainService(new DomainRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddNullDomain.
        /// </summary>
        [Test]
        public void TestAddNullDomain()
        {
            Domain nulldomain = null;

            Assert.Throws<ArgumentNullException>(() => this.DomainService.CreateDomain(nulldomain));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullName.
        /// </summary>
        [Test]
        public void TestAddNullName()
        {
            var domain = new Domain
            {
                Name = null
            };

            var results = this.DomainService.CreateDomain(domain);
            var tag = results.FirstOrDefault(res => res.Tag == "DomainNameNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthNameLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthNameLowerBoundary()
        {
            var domain = new Domain
            {
                Name = string.Empty
            };

            var results = this.DomainService.CreateDomain(domain);
            var tag = results.FirstOrDefault(res => res.Tag == "DomainNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthNameUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthNameUpperBoundary()
        {
            var domain = new Domain
            {
                Name = new string('a', 210)
            };

            var results = this.DomainService.CreateDomain(domain);
            var tag = results.FirstOrDefault(res => res.Tag == "DomainNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidLengthNameInBoundary.
        /// </summary>
        [Test]
        public void TestAddValidLengthNameInBoundary()
        {
            var domain = new Domain
            {
                Name = "Informatics"
            };

            var results = this.DomainService.CreateDomain(domain);
            var tag = results.FirstOrDefault(res => res.Tag == "DomainNameLength");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullParent.
        /// </summary>
        [Test]
        public void TestAddNullParent()
        {
            var domain = new Domain
            {
                Parent = null
            };

            var results = this.DomainService.CreateDomain(domain);
            var tag = results.FirstOrDefault(res => res.Tag == "DomainParentNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidParent.
        /// </summary>
        [Test]
        public void TestAddValidParent()
        {
            var domain = new Domain
            {
                Parent = new Domain { Name = "Science" }
            };

            var results = this.DomainService.CreateDomain(domain);
            var tag = results.FirstOrDefault(res => res.Tag == "DomainParentNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullBooks.
        /// </summary>
        [Test]
        public void TestAddNullBooks()
        {
            var domain = new Domain
            {
                Books = null
            };

            var results = this.DomainService.CreateDomain(domain);
            var tag = results.FirstOrDefault(res => res.Tag == "DomainBooksNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddEmptyBooks.
        /// </summary>
        [Test]
        public void TestAddEmptyBooks()
        {
            var domain = new Domain
            {
                Books = new List<Book>()
            };

            var results = this.DomainService.CreateDomain(domain);
            var tag = results.FirstOrDefault(res => res.Tag == "DomainBooksNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNotEmptyBooks.
        /// </summary>
        [Test]
        public void TestAddNotEmptyBooks()
        {
            var domain = new Domain
            {
                Books = new List<Book> { new Book { Name = "Origin" } }
            };

            var results = this.DomainService.CreateDomain(domain);
            var tag = results.FirstOrDefault(res => res.Tag == "DomainBooksNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidDomain.
        /// </summary>
        [Test]
        public void TestAddValidDomain()
        {
            var domain = new Domain
            {
                Name = "Science",
                Books = new List<Book>()
            };

            var results = this.DomainService.CreateDomain(domain);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
