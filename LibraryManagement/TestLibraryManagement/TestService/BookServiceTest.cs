// <copyright file="BookServiceTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Book Service Test class. </summary>
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
    /// Defines test class BookServiceTest.
    /// </summary>
    [TestFixture]
    public class BookServiceTest
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
        /// Gets or sets the book list.
        /// </summary>
        /// <value>The book list.</value>
        private List<Book> BookList { get; set; }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.BookList =
                new List<Book>
           {
                    new Book
                    {
                        Name = "Origin",
                        Authors = new List<Author> { new Author { FirstName = "Dan", LastName = "Brown" } },
                        Publications = new List<Publication> { new Publication() },
                        Domains = new List<Domain> { new Domain { Name = "Fiction" } }
                    },
                    new Book
                    {
                        Name = "Universe in a nutshell",
                        Authors = new List<Author> { new Author { FirstName = "Stephen", LastName = "Hawking" } },
                        Publications = new List<Publication> { new Publication() },
                        Domains = new List<Domain> { new Domain { Name = "Science" } }
                    },
            };
            var queryable = this.BookList.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<Book>())).Callback<Book>((entity) => this.BookList.Add(entity));
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => this.BookList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Book>())).Callback<Book>((entity) => this.BookList.Remove(entity));
            mockSet.Setup(m => m.AsNoTracking()).Returns(mockSet.Object);

            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Book>()).Returns(mockSet.Object);
            this.BookService = new BookService(new BookRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddBook.
        /// </summary>
        [Test]
        public void TestAddBookWithValidData()
        {
            var book = new Book
            {
                Name = "The pale blue dot",
                Authors = new List<Author> { new Author { FirstName = "Carl", LastName = "Sagan" } },
                Publications = new List<Publication> { new Publication() },
                Domains = new List<Domain> { new Domain { Name = "Science" } }
            };

            var results = this.BookService.Create(book);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddBookWithInvalidData.
        /// </summary>
        [Test]
        public void TestAddBookWithInvalidData()
        {
            var book = new Book();

            var results = this.BookService.Create(book);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteBookWithValidData.
        /// </summary>
        [Test]
        public void TestDeleteBookWithValidData()
        {
            var book = this.BookList.ElementAt(0);

            var results = this.BookService.Delete(book);

            Assert.IsEmpty(results);
            Assert.IsTrue(this.BookList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteBookWithInvalidData.
        /// </summary>
        [Test]
        public void TestDeleteBookWithInvalidData()
        {
            var book = new Book();

            var results = this.BookService.Delete(book);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteBookWithNullData.
        /// </summary>
        [Test]
        public void TestDeleteBookWithNullData()
        {
            Book nullBook = null;

            Assert.Throws<ArgumentNullException>(() => this.BookService.Delete(nullBook));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteExistingBookWithId.
        /// </summary>
        [Test]
        public void TestDeleteExistingBookWithId()
        {
            this.BookService.Delete(0);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
            Assert.IsTrue(this.BookList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteInvalidBookWithId.
        /// </summary>
        [Test]
        public void TestDeleteInvalidBookWithId()
        {
            this.BookService.Delete(55);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateBookWithValidData.
        /// </summary>
        [Test]
        public void TestUpdateBookWithValidData()
        {
            var book = this.BookList.ElementAt(0);

            book.Name = "Stars and galaxies";

            var results = this.BookService.Update(book);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestUpdateBookWithInvalidData.
        /// </summary>
        [Test]
        public void TestUpdateBookWithInvalidData()
        {
            var book = this.BookList.ElementAt(0);

            book.Name = string.Empty;

            var results = this.BookService.Update(book);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateBookWithNullData.
        /// </summary>
        [Test]
        public void TestUpdateBookWithNullData()
        {
            Book nullBook = null;

            Assert.Throws<ArgumentNullException>(() => this.BookService.Update(nullBook));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestFindValidBookWithId.
        /// </summary>
        [Test]
        public void TestFindValidBookWithId()
        {
            var book = this.BookService.Find(0);

            Assert.IsNotNull(book);
        }

        /// <summary>
        /// Defines the test method TestFindInvalidBookWithId.
        /// </summary>
        [Test]
        public void TestFindInvalidBookWithId()
        {
            var book = this.BookService.Find(55);

            Assert.IsNull(book);
        }

        /// <summary>
        /// Defines the test method TestFindAllBooks.
        /// </summary>
        [Test]
        public void TestFindAllBooks()
        {
            var books = this.BookService.FindAll();

            Assert.IsTrue(books.Count() == this.BookList.Count);
        }
    }
}
