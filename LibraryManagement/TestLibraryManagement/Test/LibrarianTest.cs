// <copyright file="LibrarianTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Librarian Test class. </summary>
namespace TestLibraryManagement.Test
{
    using System;
    using System.Data.Entity;
    using DataMapper.Repository;
    using DataMapper.Repository.DataBaseContext;
    using DomainModel.Model;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Service;

    /// <summary>
    /// Class LibrarianTest.
    /// </summary>
    public class LibrarianTest
    {
        /// <summary>
        /// Gets or sets the librarian service.
        /// </summary>
        /// <value>The librarian service.</value>
        private LibrarianService LibrarianService { get; set; }

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
            var mockSet = new Mock<DbSet<Librarian>>();
            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Librarian>()).Returns(mockSet.Object);
            this.LibrarianService = new LibrarianService(new LibrarianRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddNullLibrarian.
        /// </summary>
        [Test]
        public void TestAddNullLibrarian()
        {
            Librarian nullLibrarian = null;

            Assert.Throws<ArgumentNullException>(() => this.LibrarianService.CreateLibrarian(nullLibrarian));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }
    }
}
