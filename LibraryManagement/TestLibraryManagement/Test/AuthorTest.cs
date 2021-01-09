// <copyright file="AuthorTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Author Test class. </summary>
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
    /// Defines test class AuthorTest.
    /// </summary>
    [TestFixture]
    public class AuthorTest
    {
        /// <summary>
        /// Gets or sets the author service.
        /// </summary>
        /// <value>The author service.</value>
        private AuthorService AuthorService { get; set; }

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
            var mockSet = new Mock<DbSet<Author>>();
            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Author>()).Returns(mockSet.Object);
            this.AuthorService = new AuthorService(new AuthorRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddNullAuthor.
        /// </summary>
        [Test]
        public void TestAddNullAuthor()
        {
            Author nullAuthor = null;

            Assert.Throws<ArgumentNullException>(() => this.AuthorService.Create(nullAuthor));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullFirstName.
        /// </summary>
        [Test]
        public void TestAddNullFirstName()
        {
            var author = new Author
            {
                FirstName = null
            };

            var results = this.AuthorService.Create(author);
            var tag = results.FirstOrDefault(res => res.Tag == "AuthorFirstNameNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthFirstNameLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthFirstNameLowerBoundary()
        {
            var author = new Author
            {
                FirstName = string.Empty
            };

            var results = this.AuthorService.Create(author);
            var tag = results.FirstOrDefault(res => res.Tag == "AuthorFirstNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthFirstNameUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthFirstNameUpperBoundary()
        {
            var author = new Author
            {
                FirstName = new string('a', 210)
            };

            var results = this.AuthorService.Create(author);
            var tag = results.FirstOrDefault(res => res.Tag == "AuthorFirstNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidLengthFirstName.
        /// </summary>
        [Test]
        public void TestAddValidLengthFirstName()
        {
            var author = new Author
            {
                FirstName = "Lucian"
            };

            var results = this.AuthorService.Create(author);
            var tag = results.FirstOrDefault(res => res.Tag == "AuthorFirstNameLength");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullLastName.
        /// </summary>
        [Test]
        public void TestAddNullLastName()
        {
            var author = new Author
            {
                LastName = null
            };

            var results = this.AuthorService.Create(author);
            var tag = results.FirstOrDefault(res => res.Tag == "AuthorLastNameNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthLastNameLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthLastNameLowerBoundary()
        {
            var author = new Author
            {
                LastName = string.Empty
            };

            var results = this.AuthorService.Create(author);
            var tag = results.FirstOrDefault(res => res.Tag == "AuthorLastNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthLastNameUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthLastNameUpperBoundary()
        {
            var author = new Author
            {
                LastName = new string('a', 210)
            };

            var results = this.AuthorService.Create(author);
            var tag = results.FirstOrDefault(res => res.Tag == "AuthorLastNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidLengthLastName.
        /// </summary>
        [Test]
        public void TestAddValidLengthLastName()
        {
            var author = new Author
            {
                LastName = "Radu"
            };

            var results = this.AuthorService.Create(author);
            var tag = results.FirstOrDefault(res => res.Tag == "AuthorLastNameLength");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullBooks.
        /// </summary>
        [Test]
        public void TestAddNullBooks()
        {
            var author = new Author
            {
                Books = null
            };

            var results = this.AuthorService.Create(author);
            var tag = results.FirstOrDefault(res => res.Tag == "AuthorBooksNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddEmptyBooks.
        /// </summary>
        [Test]
        public void TestAddEmptyBooks()
        {
            var author = new Author
            {
                Books = new List<Book>()
            };

            var results = this.AuthorService.Create(author);
            var tag = results.FirstOrDefault(res => res.Tag == "AuthorBooksNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidBooks.
        /// </summary>
        [Test]
        public void TestAddValidBooks()
        {
            var author = new Author
            {
                Books = new List<Book> { new Book { Name = "Origin" } }
            };

            var results = this.AuthorService.Create(author);
            var tag = results.FirstOrDefault(res => res.Tag == "AuthorBooksNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAuthorGetterSetterId.
        /// </summary>
        [Test]
        public void TestAuthorGetterSetterId()
        {
            int id = 5;
            var author = new Author
            {
                FirstName = "Lucian",
                LastName = "Radu",
                Books = new List<Book> { new Book { Name = "Origin" } }
            };
            typeof(Author).GetProperty(nameof(Author.Id)).SetValue(author, id);

            Assert.IsTrue(id == author.Id);
        }

        /// <summary>
        /// Defines the test method TestAddValidAuthor.
        /// </summary>
        [Test]
        public void TestAddValidAuthor()
        {
            var author = new Author
            {
                FirstName = "Lucian",
                LastName = "Radu",
                Books = new List<Book> { new Book { Name = "Origin" } }
            };

            var results = this.AuthorService.Create(author);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
