// <copyright file="StockServiceTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Stock Service Test class. </summary>
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
    /// Defines test class StockServiceTest.
    /// </summary>
    [TestFixture]
    public class StockServiceTest
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
        /// Gets or sets the stock list.
        /// </summary>
        /// <value>The stock list.</value>
        private List<Stock> StockList { get; set; }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.StockList =
                new List<Stock>
           {
                    new Stock
                    {
                        InitialStock = 90,
                        NumberOfBooksForLecture = 50,
                        Publication = new Publication()
                    },
                    new Stock
                    {
                        InitialStock = 50,
                        NumberOfBooksForLecture = 10,
                        Publication = new Publication()
                    },
            };

            typeof(Stock).GetProperty(nameof(Stock.Id)).SetValue(this.StockList[0], 0);
            typeof(Stock).GetProperty(nameof(Stock.Id)).SetValue(this.StockList[1], 1);

            var queryable = this.StockList.AsQueryable();

            var mockSet = new Mock<DbSet<Stock>>();
            mockSet.As<IQueryable<Stock>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Stock>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Stock>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Stock>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<Stock>())).Callback<Stock>((entity) => this.StockList.Add(entity));
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => this.StockList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Stock>())).Callback<Stock>((entity) => this.StockList.Remove(entity));
            mockSet.Setup(m => m.AsNoTracking()).Returns(mockSet.Object);

            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Stock>()).Returns(mockSet.Object);
            this.StockService = new StockService(new StockRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddStock.
        /// </summary>
        [Test]
        public void TestAddStockWithValidData()
        {
            var stock = new Stock
            {
                InitialStock = 250,
                NumberOfBooksForLecture = 50,
                Publication = new Publication()
            };

            var results = this.StockService.Create(stock);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddStockWithInvalidData.
        /// </summary>
        [Test]
        public void TestAddStockWithInvalidData()
        {
            var stock = new Stock();

            var results = this.StockService.Create(stock);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteStockWithValidData.
        /// </summary>
        [Test]
        public void TestDeleteStockWithValidData()
        {
            var stock = this.StockList.ElementAt(0);

            var results = this.StockService.Delete(stock);

            Assert.IsEmpty(results);
            Assert.IsTrue(this.StockList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteStockWithInvalidData.
        /// </summary>
        [Test]
        public void TestDeleteStockWithInvalidData()
        {
            var stock = new Stock();

            var results = this.StockService.Delete(stock);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteStockWithNullData.
        /// </summary>
        [Test]
        public void TestDeleteStockWithNullData()
        {
            Stock nullStock = null;

            Assert.Throws<ArgumentNullException>(() => this.StockService.Delete(nullStock));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteExistingStockWithId.
        /// </summary>
        [Test]
        public void TestDeleteExistingStockWithId()
        {
            this.StockService.Delete(0);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
            Assert.IsTrue(this.StockList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteInvalidStockWithId.
        /// </summary>
        [Test]
        public void TestDeleteInvalidStockWithId()
        {
            this.StockService.Delete(55);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateStockWithValidData.
        /// </summary>
        [Test]
        public void TestUpdateStockWithValidData()
        {
            var stock = this.StockList.ElementAt(0);

            stock.InitialStock = 100;

            var results = this.StockService.Update(stock);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestUpdateStockWithInvalidData.
        /// </summary>
        [Test]
        public void TestUpdateStockWithInvalidData()
        {
            var stock = this.StockList.ElementAt(0);

            stock.InitialStock = -1;

            var results = this.StockService.Update(stock);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateStockWithNullData.
        /// </summary>
        [Test]
        public void TestUpdateStockWithNullData()
        {
            Stock nullStock = null;

            Assert.Throws<ArgumentNullException>(() => this.StockService.Update(nullStock));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestFindValidStockWithId.
        /// </summary>
        [Test]
        public void TestFindValidStockWithId()
        {
            var stock = this.StockService.Find(0);

            Assert.IsNotNull(stock);
        }

        /// <summary>
        /// Defines the test method TestFindInvalidStockWithId.
        /// </summary>
        [Test]
        public void TestFindInvalidStockWithId()
        {
            var stock = this.StockService.Find(55);

            Assert.IsNull(stock);
        }

        /// <summary>
        /// Defines the test method TestFindAllStocks.
        /// </summary>
        [Test]
        public void TestFindAllStocks()
        {
            var stocks = this.StockService.FindAll();

            Assert.IsTrue(stocks.Count() == this.StockList.Count);
        }
    }
}
