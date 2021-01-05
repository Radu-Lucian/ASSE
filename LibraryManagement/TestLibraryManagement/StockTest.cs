// <copyright file="StockTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Stock Test class. </summary>
namespace TestLibraryManagement
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

            Assert.Throws<ArgumentNullException>(() => this.StockService.CreateStock(nullStock));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullFirstName.
        /// </summary>
        [Test]
        public void TestAdd()
        {
            var stock = new Stock
            {
            };

            var results = this.StockService.CreateStock(stock);
            var tag = results.FirstOrDefault(res => res.Tag == "");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }
    }
}
