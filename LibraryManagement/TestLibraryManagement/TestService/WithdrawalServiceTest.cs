// <copyright file="WithdrawalServiceTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Withdrawal Service Test class. </summary>
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
    /// Defines test class WithdrawalServiceTest.
    /// </summary>
    [TestFixture]
    public class WithdrawalServiceTest
    {
        /// <summary>
        /// Gets or sets the withdrawal service.
        /// </summary>
        /// <value>The withdrawal service.</value>
        private WithdrawalService WithdrawalService { get; set; }

        /// <summary>
        /// Gets or sets the library context mock.
        /// </summary>
        /// <value>The library context mock.</value>
        private Mock<LibraryManagementContext> LibraryContextMock { get; set; }

        /// <summary>
        /// Gets or sets the withdrawal list.
        /// </summary>
        /// <value>The withdrawal list.</value>
        private List<Withdrawal> WithdrawalList { get; set; }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var domain1 = new Domain { Name = "Art" };
            var domain2 = new Domain { Name = "Fiction" };
            var domain3 = new Domain { Name = "Science" };

            typeof(Domain).GetProperty(nameof(Domain.Id)).SetValue(domain1, 0);
            typeof(Domain).GetProperty(nameof(Domain.Id)).SetValue(domain2, 1);
            typeof(Domain).GetProperty(nameof(Domain.Id)).SetValue(domain3, 2);

            var book1 = new Book { Domains = new List<Domain> { domain1 }, Name = "Prehistoric Art" };
            var book2 = new Book { Domains = new List<Domain> { domain2 }, Name = "Origin" };
            var book3 = new Book { Domains = new List<Domain> { domain3 }, Name = "Stars" };

            typeof(Book).GetProperty(nameof(Book.Id)).SetValue(book1, 0);
            typeof(Book).GetProperty(nameof(Book.Id)).SetValue(book2, 1);
            typeof(Book).GetProperty(nameof(Book.Id)).SetValue(book3, 2);

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };
            var stock2 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };
            var stock3 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            typeof(Stock).GetProperty(nameof(Stock.Id)).SetValue(stock1, 0);
            typeof(Stock).GetProperty(nameof(Stock.Id)).SetValue(stock2, 1);
            typeof(Stock).GetProperty(nameof(Stock.Id)).SetValue(stock3, 2);

            var publication1 = new Publication
            {
                NumberOfPages = 234,
                Book = book1,
                Stock = stock1
            };

            var publication2 = new Publication
            {
                NumberOfPages = 498,
                Book = book2,
                Stock = stock2
            };

            var publication3 = new Publication
            {
                NumberOfPages = 543,
                Book = book2,
                Stock = stock3
            };

            typeof(Publication).GetProperty(nameof(Publication.Id)).SetValue(publication1, 0);
            typeof(Publication).GetProperty(nameof(Publication.Id)).SetValue(publication2, 1);
            typeof(Publication).GetProperty(nameof(Publication.Id)).SetValue(publication3, 2);

            this.WithdrawalList =
                new List<Withdrawal>
           {
                    new Withdrawal
                    {
                        Publications = new List<Publication>
                        {
                            publication1,
                            publication2
                        },
                        Extensions = new List<Extension>(),
                        Reader = new Reader { Withdrawals = new List<Withdrawal>() },
                        RentedDate = DateTime.Today.AddDays(-5),
                        DueDate = DateTime.Today.AddDays(5)
                    },
                    new Withdrawal
                    {
                        Publications = new List<Publication>
                        {
                            publication3
                        },
                        Extensions = new List<Extension>(),
                        Reader = new Reader { Withdrawals = new List<Withdrawal>() },
                        RentedDate = DateTime.Today.AddDays(-10),
                        DueDate = DateTime.Today.AddDays(10)
                    }
            };

            typeof(Withdrawal).GetProperty(nameof(Withdrawal.Id)).SetValue(this.WithdrawalList[0], 0);
            typeof(Withdrawal).GetProperty(nameof(Withdrawal.Id)).SetValue(this.WithdrawalList[1], 1);

            var queryable = this.WithdrawalList.AsQueryable();

            var mockSet = new Mock<DbSet<Withdrawal>>();
            mockSet.As<IQueryable<Withdrawal>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Withdrawal>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Withdrawal>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Withdrawal>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<Withdrawal>())).Callback<Withdrawal>((entity) => this.WithdrawalList.Add(entity));
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => this.WithdrawalList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Withdrawal>())).Callback<Withdrawal>((entity) => this.WithdrawalList.Remove(entity));
            mockSet.Setup(m => m.AsNoTracking()).Returns(mockSet.Object);

            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Withdrawal>()).Returns(mockSet.Object);
            this.WithdrawalService = new WithdrawalService(new WithdrawalRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddWithdrawal.
        /// </summary>
        [Test]
        public void TestAddWithdrawalWithValidData()
        {
            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>
                {
                    new Publication
                    {
                        NumberOfPages = 425,
                        Book = new Book { Domains = new List<Domain> { new Domain { Name = "Science" } }, Name = "Stars and galaxies" },
                        Stock = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 }
                    }
                },
                Extensions = new List<Extension>(),
                Reader = new Reader { Withdrawals = new List<Withdrawal>() },
                RentedDate = DateTime.Today.AddDays(-5),
                DueDate = DateTime.Today.AddDays(10)
            };

            var results = this.WithdrawalService.Create(withdrawal);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddWithdrawalWithInvalidData.
        /// </summary>
        [Test]
        public void TestAddWithdrawalWithInvalidData()
        {
            var withdrawal = new Withdrawal();

            var results = this.WithdrawalService.Create(withdrawal);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteWithdrawalWithValidData.
        /// </summary>
        [Test]
        public void TestDeleteWithdrawalWithValidData()
        {
            var withdrawal = this.WithdrawalList.ElementAt(0);

            var results = this.WithdrawalService.Delete(withdrawal);

            Assert.IsEmpty(results);
            Assert.IsTrue(this.WithdrawalList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteWithdrawalWithInvalidData.
        /// </summary>
        [Test]
        public void TestDeleteWithdrawalWithInvalidData()
        {
            var withdrawal = new Withdrawal();

            var results = this.WithdrawalService.Delete(withdrawal);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteWithdrawalWithNullData.
        /// </summary>
        [Test]
        public void TestDeleteWithdrawalWithNullData()
        {
            Withdrawal nullWithdrawal = null;

            Assert.Throws<ArgumentNullException>(() => this.WithdrawalService.Delete(nullWithdrawal));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteExistingWithdrawalWithId.
        /// </summary>
        [Test]
        public void TestDeleteExistingWithdrawalWithId()
        {
            this.WithdrawalService.Delete(0);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
            Assert.IsTrue(this.WithdrawalList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteInvalidWithdrawalWithId.
        /// </summary>
        [Test]
        public void TestDeleteInvalidWithdrawalWithId()
        {
            this.WithdrawalService.Delete(55);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateWithdrawalWithValidData.
        /// </summary>
        [Test]
        public void TestUpdateWithdrawalWithValidData()
        {
            var withdrawal = this.WithdrawalList.ElementAt(0);

            withdrawal.DueDate = DateTime.Today.AddDays(5);

            var results = this.WithdrawalService.Update(withdrawal);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestUpdateWithdrawalWithInvalidData.
        /// </summary>
        [Test]
        public void TestUpdateWithdrawalWithInvalidData()
        {
            var withdrawal = this.WithdrawalList.ElementAt(0);

            withdrawal.DueDate = withdrawal.RentedDate.AddDays(-1);

            var results = this.WithdrawalService.Update(withdrawal);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateWithdrawalWithNullData.
        /// </summary>
        [Test]
        public void TestUpdateWithdrawalWithNullData()
        {
            Withdrawal nullWithdrawal = null;

            Assert.Throws<ArgumentNullException>(() => this.WithdrawalService.Update(nullWithdrawal));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestFindValidWithdrawalWithId.
        /// </summary>
        [Test]
        public void TestFindValidWithdrawalWithId()
        {
            var withdrawal = this.WithdrawalService.Find(0);

            Assert.IsNotNull(withdrawal);
        }

        /// <summary>
        /// Defines the test method TestFindInvalidWithdrawalWithId.
        /// </summary>
        [Test]
        public void TestFindInvalidWithdrawalWithId()
        {
            var withdrawal = this.WithdrawalService.Find(55);

            Assert.IsNull(withdrawal);
        }

        /// <summary>
        /// Defines the test method TestFindAllWithdrawals.
        /// </summary>
        [Test]
        public void TestFindAllWithdrawals()
        {
            var withdrawals = this.WithdrawalService.FindAll();

            Assert.IsTrue(withdrawals.Count() == this.WithdrawalList.Count);
        }
    }
}
