// <copyright file="BookTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Book Test class. </summary>
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
    /// Defines test class BookTest.
    /// </summary>
    [TestFixture]
    public class BookTest
    {
        /// <summary>
        /// Gets or sets the book service.
        /// </summary>
        /// <value>The book service.</value>
        private BookService BookService { get; set; }

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
            var mockSet = new Mock<DbSet<Book>>();
            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Book>()).Returns(mockSet.Object);
            this.BookService = new BookService(new BookRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddNullBook.
        /// </summary>
        [Test]
        public void TestAddNullBook()
        {
            Book nullBook = null;

            Assert.Throws<ArgumentNullException>(() => this.BookService.Create(nullBook));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullName.
        /// </summary>
        [Test]
        public void TestAddNullName()
        {
            var book = new Book
            {
                Name = null
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "BookNameNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthNameLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthNameLowerBoundary()
        {
            var book = new Book
            {
                Name = string.Empty
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "BookNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthNameUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthNameUpperBoundary()
        {
            var book = new Book
            {
                Name = new string('a', 210)
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "BookNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidLengthNameInBoundary.
        /// </summary>
        [Test]
        public void TestAddValidLengthNameInBoundary()
        {
            var book = new Book
            {
                Name = "Origin"
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "BookNameLength");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullDomains.
        /// </summary>
        [Test]
        public void TestAddNullDomains()
        {
            var book = new Book
            {
                Name = "Origin",
                Domains = null
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "BookDomainsNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddEmptyDomains.
        /// </summary>
        [Test]
        public void TestAddEmptyDomains()
        {
            var book = new Book
            {
                Name = "Origin",
                Domains = new List<Domain>()
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateDomains");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddMoreDomainsDomainsThanDOM.
        /// </summary>
        [Test]
        public void TestAddMoreDomainsDomainsThanDOM()
        {
            var book = new Book
            {
                Name = "Origin",
                Domains = new List<Domain> { new Domain { Name = "Informatics" }, new Domain { Name = "Mathematics" }, new Domain { Name = "Psychology" } }
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateDomainsDOM");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddLessDomainsDomainsThanDOM.
        /// </summary>
        [Test]
        public void TestAddLessDomainsDomainsThanDOM()
        {
            var book = new Book
            {
                Name = "Origin",
                Domains = new List<Domain> { new Domain { Name = "Informatics" }, new Domain { Name = "Mathematics" } }
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateDomainsDOM");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddDomainsWithInheritance.
        /// </summary>
        [Test]
        public void TestAddDomainsWithInheritance()
        {
            var domain1 = new Domain { Name = "Science" };
            var domain2 = new Domain { Name = "Mathematics", Parent = domain1 };
            var book = new Book
            {
                Name = "Origin",
                Domains = new List<Domain> { domain1, domain2 }
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateDomainsInharitance");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddDomainsWithInheritanceTwoLevel.
        /// </summary>
        [Test]
        public void TestAddDomainsWithInheritanceTwoLevel()
        {
            var domain1 = new Domain { Name = "Science" };
            var domain2 = new Domain { Name = "Mathematics", Parent = domain1 };
            var domain3 = new Domain { Name = "Algebra", Parent = domain2 };
            var book = new Book
            {
                Name = "Origin",
                Domains = new List<Domain> { domain1, domain2 }
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateDomainsInharitance");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddDomainsWithoutInheritance.
        /// </summary>
        [Test]
        public void TestAddDomainsWithoutInheritance()
        {
            var domain1 = new Domain { Name = "Informatics" };
            var domain2 = new Domain { Name = "Mathematics" };
            var domain3 = new Domain { Name = "Algorithms", Parent = domain1 };
            var domain4 = new Domain { Name = "Algebra", Parent = domain2 };
            var book = new Book
            {
                Name = "Origin",
                Domains = new List<Domain> { domain1, domain2 }
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateDomainsInharitance");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullAuthors.
        /// </summary>
        [Test]
        public void TestAddNullAuthors()
        {
            var book = new Book
            {
                Name = "Origin",
                Authors = null
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "BookAuthorsNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddEmptyAuthors.
        /// </summary>
        [Test]
        public void TestAddEmptyAuthors()
        {
            var book = new Book
            {
                Name = "Origin",
                Authors = new List<Author>(),
                Domains = new List<Domain>()
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateAuthors");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidAuthors.
        /// </summary>
        [Test]
        public void TestAddValidAuthors()
        {
            var book = new Book
            {
                Name = "Origin",
                Authors = new List<Author> { new Author { FirstName = "Lucian", LastName = "Radu" } },
                Domains = new List<Domain>()
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateAuthors");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullPublications.
        /// </summary>
        [Test]
        public void TestAddNullPublications()
        {
            var book = new Book
            {
                Name = "Origin",
                Publications = null
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "BookPublicationsNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddEmptyPublications.
        /// </summary>
        [Test]
        public void TestAddEmptyPublications()
        {
            var book = new Book
            {
                Name = "Origin",
                Authors = new List<Author>(),
                Publications = new List<Publication>(),
                Domains = new List<Domain>()
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublications");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidPublications.
        /// </summary>
        [Test]
        public void TestAddValidPublications()
        {
            var book = new Book
            {
                Name = "Origin",
                Authors = new List<Author>(),
                Publications = new List<Publication> { new Publication() },
                Domains = new List<Domain>()
            };

            var results = this.BookService.Create(book);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublications");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidBook.
        /// </summary>
        [Test]
        public void TestAddValidBook()
        {
            var book = new Book
            {
                Name = "Origin",
                Authors = new List<Author> { new Author { FirstName = "Luci", LastName = "Radu" } },
                Publications = new List<Publication> { new Publication() },
                Domains = new List<Domain> { new Domain { Name = "Informatics" } }
            };

            var results = this.BookService.Create(book);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}