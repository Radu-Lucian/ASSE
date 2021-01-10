// <copyright file="AuthorServiceTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Author Service Test class. </summary>
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
    using ServiceLayer.Service.Base;

    /// <summary>
    /// Defines test class AuthorTest.
    /// </summary>
    [TestFixture]
    public class AuthorServiceTest
    {
        /// <summary>
        /// Gets or sets the author service.
        /// </summary>
        /// <value>The author service.</value>
        private IBaseService<Author> AuthorService { get; set; }

        /// <summary>
        /// Gets or sets the library context mock.
        /// </summary>
        /// <value>The library context mock.</value>
        private Mock<LibraryManagementContext> LibraryContextMock { get; set; }

        /// <summary>
        /// Gets or sets the author list.
        /// </summary>
        /// <value>The author list.</value>
        private List<Author> AuthorList { get; set; }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.AuthorList =
                new List<Author>
           {
                    new Author
                    {
                        FirstName = "Lucian",
                        LastName = "Radu",
                        Books = new List<Book> { new Book { Name = "Origin" } }
                    },
                    new Author
                    {
                        FirstName = "Andrei",
                        LastName = "Radu",
                        Books = new List<Book> { new Book { Name = "Stars" } }
                    },
            };
            var queryable = this.AuthorList.AsQueryable();

            var mockSet = new Mock<DbSet<Author>>();
            mockSet.As<IQueryable<Author>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Author>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Author>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Author>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(d => d.Add(It.IsAny<Author>())).Callback<Author>((s) => this.AuthorList.Add(s));
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => this.AuthorList.FirstOrDefault(d => d.Id == (int)ids[0]));
            // mockSet.Setup(m => m.AsNoTracking()).Returns(mockSet.Object);

            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Author>()).Returns(mockSet.Object);
            this.AuthorService = new AuthorService(new AuthorRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddAuthor.
        /// </summary>
        [Test]
        public void TestAddAuthorWithValidData()
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

        /// <summary>
        /// Defines the test method TestAddAuthorWithInvalidData.
        /// </summary>
        [Test]
        public void TestAddAuthorWithInvalidData()
        {
            var author = new Author();

            var results = this.AuthorService.Create(author);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteAuthorWithValidData.
        /// </summary>
        [Test]
        public void TestDeleteAuthorWithValidData()
        {
            var author = this.AuthorList.ElementAt(0);

            var results = this.AuthorService.Delete(author);

            Assert.IsEmpty(results);
        }

        /// <summary>
        /// Defines the test method TestDeleteAuthorWithInvalidData.
        /// </summary>
        [Test]
        public void TestDeleteAuthorWithInvalidData()
        {
            var author = new Author();

            var results = this.AuthorService.Delete(author);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteAuthorWithNullData.
        /// </summary>
        [Test]
        public void TestDeleteAuthorWithNullData()
        {
            Author nullAuthor = null;

            Assert.Throws<ArgumentNullException>(() => this.AuthorService.Delete(nullAuthor));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateAuthorWithValidData.
        /// </summary>
        [Test]
        public void TestUpdateAuthorWithValidData()
        {
            var author = this.AuthorList.ElementAt(0);

            author.FirstName = "Andrei";

            var results = this.AuthorService.Update(author);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestUpdateAuthorWithInvalidData.
        /// </summary>
        [Test]
        public void TestUpdateAuthorWithInvalidData()
        {
            var author = this.AuthorList.ElementAt(0);

            author.FirstName = string.Empty;

            var results = this.AuthorService.Update(author);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateAuthorWithNullData.
        /// </summary>
        [Test]
        public void TestUpdateAuthorWithNullData()
        {
            Author nullAuthor = null;

            Assert.Throws<ArgumentNullException>(() => this.AuthorService.Update(nullAuthor));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestFindAllAuthors.
        /// </summary>
        [Test]
        public void TestFindAllAuthors()
        {
            var authors = this.AuthorService.Find(0);

            Assert.IsNotNull(authors);
        }
    }
}
