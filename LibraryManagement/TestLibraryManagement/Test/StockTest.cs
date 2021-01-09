// <copyright file="StockTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Stock Test class. </summary>
namespace TestLibraryManagement.Test
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using DataMapper.Repository;
    using DataMapper.Repository.DataBaseContext;
    using DomainModel.Model;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Service;

    /// <summary>
    /// Defines test class StockTest.
    /// </summary>
    [TestFixture]
    public class StockTest
    {
        /// <summary>
        /// Gets or sets the stock service.
        /// </summary>
        /// <value>The stock service.</value>
        private StockService StockService { get; set; }

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
            var mockSet = new Mock<DbSet<Stock>>();
            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Stock>()).Returns(mockSet.Object);
            this.StockService = new StockService(new StockRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddNullStock.
        /// </summary>
        [Test]
        public void TestAddNullStock()
        {
            Stock nullStock = null;

            Assert.Throws<ArgumentNullException>(() => this.StockService.Create(nullStock));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidInitialStockLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidInitialStockLowerBoundary()
        {
            var stock = new Stock
            {
                InitialStock = -1
            };

            var results = this.StockService.Create(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "StockInitialStockInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidInitialStockUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidInitialStockUpperBoundary()
        {
            var stock = new Stock
            {
                InitialStock = int.MaxValue
            };

            var results = this.StockService.Create(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "StockInitialStockInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidInitialStock.
        /// </summary>
        [Test]
        public void TestAddValidInitialStock()
        {
            var stock = new Stock
            {
                InitialStock = 500
            };

            var results = this.StockService.Create(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "StockInitialStockInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidRentedStockLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidRentedStockLowerBoundary()
        {
            var stock = new Stock
            {
                RentedStock = -1
            };

            var results = this.StockService.Create(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "StockRentedStockInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidRentedStockUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidRentedStockUpperBoundary()
        {
            var stock = new Stock
            {
                RentedStock = int.MaxValue
            };

            var results = this.StockService.Create(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "StockRentedStockInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidRentedStock.
        /// </summary>
        [Test]
        public void TestAddValidRentedStock()
        {
            var stock = new Stock
            {
                RentedStock = 0
            };

            var results = this.StockService.Create(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "StockRentedStockInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidNumberOfBooksForLectureLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidNumberOfBooksForLectureLowerBoundary()
        {
            var stock = new Stock
            {
                NumberOfBooksForLecture = -1
            };

            var results = this.StockService.Create(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "StockNumberOfBooksForLectureInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidNumberOfBooksForLectureUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidNumberOfBooksForLectureUpperBoundary()
        {
            var stock = new Stock
            {
                NumberOfBooksForLecture = int.MaxValue
            };

            var results = this.StockService.Create(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "StockNumberOfBooksForLectureInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidNumberOfBooksForLecture.
        /// </summary>
        [Test]
        public void TestAddValidNumberOfBooksForLecture()
        {
            var stock = new Stock
            {
                NumberOfBooksForLecture = 1000
            };

            var results = this.StockService.Create(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "StockNumberOfBooksForLectureInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidPublication.
        /// </summary>
        [Test]
        public void TestAddInvalidPublication()
        {
            var stock = new Stock
            {
                Publication = null
            };

            var results = this.StockService.Create(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "StockPublicationNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidPublication.
        /// </summary>
        [Test]
        public void TestAddValidPublication()
        {
            var stock = new Stock
            {
                Publication = new Publication()
            };

            var results = this.StockService.Create(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "StockPublicationNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidNumberOfBooksForLecture.
        /// </summary>
        [Test]
        public void TestAddInvalidNumberOfBooksForLecture()
        {
            var stock = new Stock
            {
                InitialStock = 100,
                NumberOfBooksForLecture = 150
            };

            var results = this.StockService.Create(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateBookStock");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestStockGetterSetterId.
        /// </summary>
        [Test]
        public void TestStockGetterSetterId()
        {
            int id = 5;
            var stock = new Stock
            {
                InitialStock = 100,
                NumberOfBooksForLecture = 50,
                Publication = new Publication()
            };

            typeof(Stock).GetProperty(nameof(Stock.Id)).SetValue(stock, id);

            Assert.IsTrue(id == stock.Id);
        }

        /// <summary>
        /// Defines the test method TestAddValidStock.
        /// </summary>
        [Test]
        public void TestAddValidStock()
        {
            var stock = new Stock
            {
                InitialStock = 100,
                NumberOfBooksForLecture = 50,
                Publication = new Publication()
            };

            var results = this.StockService.Create(stock);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
