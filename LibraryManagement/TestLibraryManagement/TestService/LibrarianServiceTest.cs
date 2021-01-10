// <copyright file="LibrarianServiceTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Librarian Service Test class. </summary>
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
    /// Defines test class LibrarianServiceTest.
    /// </summary>
    [TestFixture]
    public class LibrarianServiceTest
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
        /// Gets or sets the librarian list.
        /// </summary>
        /// <value>The librarian list.</value>
        private List<Librarian> LibrarianList { get; set; }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.LibrarianList =
                new List<Librarian>
           {
                    new Librarian
                    {
                        FirstName = "Lucian",
                        LastName = "Radu",
                        Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319",
                        Gender = "M",
                        EmailAddress = "radulucian.andrei@gmail.com",
                        PhoneNumber = "0784000292",
                        Withdrawals = new List<Withdrawal> { new Withdrawal() }
                    },
                    new Librarian
                    {
                        FirstName = "Andrei",
                        LastName = "Radu",
                        Address = "Bucuresti, Sector 1, Str.Unirii, 15",
                        Gender = "M",
                        EmailAddress = "lucian.radu@student.unitbv.ro",
                        PhoneNumber = "0784000292",
                        Withdrawals = new List<Withdrawal> { new Withdrawal() }
                    },
            };

            typeof(Reader).GetProperty(nameof(Reader.Id)).SetValue(this.LibrarianList[0], 0);
            typeof(Reader).GetProperty(nameof(Reader.Id)).SetValue(this.LibrarianList[1], 1);

            var queryable = this.LibrarianList.AsQueryable();

            var mockSet = new Mock<DbSet<Librarian>>();
            mockSet.As<IQueryable<Librarian>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Librarian>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Librarian>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Librarian>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<Librarian>())).Callback<Librarian>((entity) => this.LibrarianList.Add(entity));
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => this.LibrarianList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Librarian>())).Callback<Librarian>((entity) => this.LibrarianList.Remove(entity));
            mockSet.Setup(m => m.AsNoTracking()).Returns(mockSet.Object);

            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Librarian>()).Returns(mockSet.Object);
            this.LibrarianService = new LibrarianService(new LibrarianRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddLibrarian.
        /// </summary>
        [Test]
        public void TestAddLibrarianWithValidData()
        {
            var librarian = new Librarian
            {
                FirstName = "Tom",
                LastName = "Hanks",
                Address = "Brasov, Brasov, Str.Meschendorfer, 319",
                Gender = "M",
                EmailAddress = "tom.hanks@gmail.com",
                PhoneNumber = "0784000292",
                Withdrawals = new List<Withdrawal> { new Withdrawal() }
            };

            var results = this.LibrarianService.Create(librarian);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddLibrarianWithInvalidData.
        /// </summary>
        [Test]
        public void TestAddLibrarianWithInvalidData()
        {
            var librarian = new Librarian();

            var results = this.LibrarianService.Create(librarian);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteLibrarianWithValidData.
        /// </summary>
        [Test]
        public void TestDeleteLibrarianWithValidData()
        {
            var librarian = this.LibrarianList.ElementAt(0);

            var results = this.LibrarianService.Delete(librarian);

            Assert.IsEmpty(results);
            Assert.IsTrue(this.LibrarianList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteLibrarianWithInvalidData.
        /// </summary>
        [Test]
        public void TestDeleteLibrarianWithInvalidData()
        {
            var librarian = new Librarian();

            var results = this.LibrarianService.Delete(librarian);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteLibrarianWithNullData.
        /// </summary>
        [Test]
        public void TestDeleteLibrarianWithNullData()
        {
            Librarian nullLibrarian = null;

            Assert.Throws<ArgumentNullException>(() => this.LibrarianService.Delete(nullLibrarian));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteExistingLibrarianWithId.
        /// </summary>
        [Test]
        public void TestDeleteExistingLibrarianWithId()
        {
            this.LibrarianService.Delete(0);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
            Assert.IsTrue(this.LibrarianList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteInvalidLibrarianWithId.
        /// </summary>
        [Test]
        public void TestDeleteInvalidLibrarianWithId()
        {
            this.LibrarianService.Delete(55);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateLibrarianWithValidData.
        /// </summary>
        [Test]
        public void TestUpdateLibrarianWithValidData()
        {
            var librarian = this.LibrarianList.ElementAt(0);

            librarian.FirstName = "Tom";

            var results = this.LibrarianService.Update(librarian);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestUpdateLibrarianWithInvalidData.
        /// </summary>
        [Test]
        public void TestUpdateLibrarianWithInvalidData()
        {
            var librarian = this.LibrarianList.ElementAt(0);

            librarian.FirstName = string.Empty;

            var results = this.LibrarianService.Update(librarian);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateLibrarianWithNullData.
        /// </summary>
        [Test]
        public void TestUpdateLibrarianWithNullData()
        {
            Librarian nullLibrarian = null;

            Assert.Throws<ArgumentNullException>(() => this.LibrarianService.Update(nullLibrarian));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestFindValidLibrarianWithId.
        /// </summary>
        [Test]
        public void TestFindValidLibrarianWithId()
        {
            var librarian = this.LibrarianService.Find(0);

            Assert.IsNotNull(librarian);
        }

        /// <summary>
        /// Defines the test method TestFindInvalidLibrarianWithId.
        /// </summary>
        [Test]
        public void TestFindInvalidLibrarianWithId()
        {
            var librarian = this.LibrarianService.Find(55);

            Assert.IsNull(librarian);
        }

        /// <summary>
        /// Defines the test method TestFindAllLibrarians.
        /// </summary>
        [Test]
        public void TestFindAllLibrarians()
        {
            var librarians = this.LibrarianService.FindAll();

            Assert.IsTrue(librarians.Count() == this.LibrarianList.Count);
        }
    }
}
