// <copyright file="ReaderServiceTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Reader Service Test class. </summary>
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
    /// Defines test class ReaderServiceTest.
    /// </summary>
    [TestFixture]
    public class ReaderServiceTest
    {
        /// <summary>
        /// Gets or sets the reader service.
        /// </summary>
        /// <value>The reader service.</value>
        private ReaderService ReaderService { get; set; }

        /// <summary>
        /// Gets or sets the library context mock.
        /// </summary>
        /// <value>The library context mock.</value>
        private Mock<LibraryManagementContext> LibraryContextMock { get; set; }

        /// <summary>
        /// Gets or sets the reader list.
        /// </summary>
        /// <value>The reader list.</value>
        private List<Reader> ReaderList { get; set; }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.ReaderList =
                new List<Reader>
           {
                    new Reader
                    {
                        FirstName = "Lucian",
                        LastName = "Radu",
                        Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319",
                        Gender = "M",
                        EmailAddress = "radulucian.andrei@gmail.com",
                        PhoneNumber = "0784000292",
                        Withdrawals = new List<Withdrawal> { new Withdrawal() }
                    },
                    new Reader
                    {
                        FirstName = "Andrei",
                        LastName = "Radu",
                        Address = "Bucuresti, Sector 5, Str.Regelui, 16",
                        Gender = "M",
                        EmailAddress = "luci.andrei@unitbv.com",
                        PhoneNumber = "0784000292",
                        Withdrawals = new List<Withdrawal> { new Withdrawal() }
                    },
            };

            typeof(Reader).GetProperty(nameof(Reader.Id)).SetValue(this.ReaderList[0], 0);
            typeof(Reader).GetProperty(nameof(Reader.Id)).SetValue(this.ReaderList[1], 1);

            var queryable = this.ReaderList.AsQueryable();

            var mockSet = new Mock<DbSet<Reader>>();
            var mockSetLib = new Mock<DbSet<Librarian>>();
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Reader>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<Reader>())).Callback<Reader>((entity) => this.ReaderList.Add(entity));
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => this.ReaderList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Reader>())).Callback<Reader>((entity) => this.ReaderList.Remove(entity));
            mockSet.Setup(m => m.AsNoTracking()).Returns(mockSet.Object);

            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Reader>()).Returns(mockSet.Object);
            this.LibraryContextMock.Setup(m => m.Set<Librarian>()).Returns(mockSetLib.Object);
            this.ReaderService = new ReaderService(new ReaderRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddReader.
        /// </summary>
        [Test]
        public void TestAddReaderWithValidData()
        {
            var reader = new Reader
            {
                FirstName = "Tom",
                LastName = "Hank",
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 390",
                Gender = "M",
                EmailAddress = "tom.hanks@unitbv.ro",
                PhoneNumber = "0784000292",
                Withdrawals = new List<Withdrawal> { new Withdrawal() }
            };

            var results = this.ReaderService.Create(reader);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddReaderWithInvalidData.
        /// </summary>
        [Test]
        public void TestAddReaderWithInvalidData()
        {
            var reader = new Reader();

            var results = this.ReaderService.Create(reader);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteReaderWithValidData.
        /// </summary>
        [Test]
        public void TestDeleteReaderWithValidData()
        {
            var reader = this.ReaderList.ElementAt(0);

            var results = this.ReaderService.Delete(reader);

            Assert.IsEmpty(results);
            Assert.IsTrue(this.ReaderList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteReaderWithInvalidData.
        /// </summary>
        [Test]
        public void TestDeleteReaderWithInvalidData()
        {
            var reader = new Reader();

            var results = this.ReaderService.Delete(reader);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteReaderWithNullData.
        /// </summary>
        [Test]
        public void TestDeleteReaderWithNullData()
        {
            Reader nullReader = null;

            Assert.Throws<ArgumentNullException>(() => this.ReaderService.Delete(nullReader));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteExistingReaderWithId.
        /// </summary>
        [Test]
        public void TestDeleteExistingReaderWithId()
        {
            this.ReaderService.Delete(0);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
            Assert.IsTrue(this.ReaderList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteInvalidReaderWithId.
        /// </summary>
        [Test]
        public void TestDeleteInvalidReaderWithId()
        {
            this.ReaderService.Delete(55);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateReaderWithValidData.
        /// </summary>
        [Test]
        public void TestUpdateReaderWithValidData()
        {
            var reader = this.ReaderList.ElementAt(0);

            reader.FirstName = "Martin";

            var results = this.ReaderService.Update(reader);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestUpdateReaderWithInvalidData.
        /// </summary>
        [Test]
        public void TestUpdateReaderWithInvalidData()
        {
            var reader = this.ReaderList.ElementAt(0);

            reader.FirstName = string.Empty;

            var results = this.ReaderService.Update(reader);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateReaderWithNullData.
        /// </summary>
        [Test]
        public void TestUpdateReaderWithNullData()
        {
            Reader nullReader = null;

            Assert.Throws<ArgumentNullException>(() => this.ReaderService.Update(nullReader));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestFindValidReaderWithId.
        /// </summary>
        [Test]
        public void TestFindValidReaderWithId()
        {
            var reader = this.ReaderService.Find(0);

            Assert.IsNotNull(reader);
        }

        /// <summary>
        /// Defines the test method TestFindInvalidReaderWithId.
        /// </summary>
        [Test]
        public void TestFindInvalidReaderWithId()
        {
            var reader = this.ReaderService.Find(55);

            Assert.IsNull(reader);
        }

        /// <summary>
        /// Defines the test method TestFindAllReaders.
        /// </summary>
        [Test]
        public void TestFindAllReaders()
        {
            var readers = this.ReaderService.FindAll();

            Assert.IsTrue(readers.Count() == this.ReaderList.Count);
        }
    }
}
