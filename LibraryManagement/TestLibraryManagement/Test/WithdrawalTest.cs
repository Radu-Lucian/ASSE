// <copyright file="WithdrawalTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Withdrawal Test class. </summary>
namespace TestLibraryManagement.Test
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using DataMapper.Repository;
    using DataMapper.Repository.DataBaseContext;
    using DomainModel.Model;
    using DomainModel.Options;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Service;

    /// <summary>
    /// Defines test class WithdrawalTest.
    /// </summary>
    [TestFixture]
    public class WithdrawalTest
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
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var mockSet = new Mock<DbSet<Withdrawal>>();
            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Withdrawal>()).Returns(mockSet.Object);
            this.WithdrawalService = new WithdrawalService(new WithdrawalRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddNullWithdrawal.
        /// </summary>
        [Test]
        public void TestAddNullWithdrawal()
        {
            Withdrawal nullWithdrawal = null;

            Assert.Throws<ArgumentNullException>(() => this.WithdrawalService.Create(nullWithdrawal));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidRentedDateLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidRentedDateLowerBoundary()
        {
            var withdrawal = new Withdrawal
            {
                RentedDate = new DateTime(2009, 12, 31)
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalRentedDate");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidRentedDateUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidRentedDateUpperBoundary()
        {
            var withdrawal = new Withdrawal
            {
                RentedDate = new DateTime(2100, 01, 02)
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalRentedDate");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidRentedDate.
        /// </summary>
        [Test]
        public void TestAddValidRentedDate()
        {
            var withdrawal = new Withdrawal
            {
                RentedDate = new DateTime(2020, 01, 06)
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalRentedDate");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidDueDateLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidDueDateLowerBoundary()
        {
            var withdrawal = new Withdrawal
            {
                DueDate = new DateTime(2009, 12, 31)
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalDueDate");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidDueDateUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidDueDateUpperBoundary()
        {
            var withdrawal = new Withdrawal
            {
                DueDate = new DateTime(2100, 01, 02)
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalDueDate");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidDueDate.
        /// </summary>
        [Test]
        public void TestAddValidDueDate()
        {
            var withdrawal = new Withdrawal
            {
                DueDate = new DateTime(2020, 01, 06)
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalDueDate");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidDueDateRentedDate.
        /// </summary>
        [Test]
        public void TestAddInvalidDueDateRentedDate()
        {
            var withdrawal = new Withdrawal
            {
                RentedDate = new DateTime(2020, 01, 06),
                DueDate = new DateTime(2020, 01, 02),
                Publications = new List<Publication>()
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsDate");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidDueDateRentedDate.
        /// </summary>
        [Test]
        public void TestAddValidDueDateRentedDate()
        {
            var withdrawal = new Withdrawal
            {
                RentedDate = new DateTime(2020, 01, 06),
                DueDate = new DateTime(2020, 01, 10)
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsDate");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullExtensions.
        /// </summary>
        [Test]
        public void TestAddNullExtensions()
        {
            var withdrawal = new Withdrawal
            {
                Extensions = null
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalExtensionsNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddEmptyExtensions.
        /// </summary>
        [Test]
        public void TestAddEmptyExtensions()
        {
            var withdrawal = new Withdrawal
            {
                Extensions = new List<Extension>()
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalExtensionsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidExtensions.
        /// </summary>
        [Test]
        public void TestAddValidExtensions()
        {
            var withdrawal = new Withdrawal
            {
                Extensions = new List<Extension>() { new Extension { ExtraDays = 5 } }
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalExtensionsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullPublications.
        /// </summary>
        [Test]
        public void TestAddNullPublications()
        {
            var withdrawal = new Withdrawal
            {
                Publications = null
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalPublicationsNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddEmptyPublications.
        /// </summary>
        [Test]
        public void TestAddEmptyPublications()
        {
            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>()
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublications");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidPublications.
        /// </summary>
        [Test]
        public void TestAddValidPublications()
        {
            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>() { new Publication { NumberOfPages = 5 } }
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalPublicationsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullReader.
        /// </summary>
        [Test]
        public void TestAddNullReader()
        {
            var withdrawal = new Withdrawal
            {
                Reader = null
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalReaderNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidReader.
        /// </summary>
        [Test]
        public void TestAddValidReader()
        {
            var withdrawal = new Withdrawal
            {
                Reader = new Reader { FirstName = "Luci" }
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "WithdrawalReaderNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidPublicationsC.
        /// </summary>
        [Test]
        public void TestAddInvalidPublicationsC()
        {
            var publications = Enumerable.Range(0, ApplicationOptions.Options.C + 1).Select(i => new Publication { NumberOfPages = i }).ToList();
            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>(publications)
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsC");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidPublicationsC.
        /// </summary>
        [Test]
        public void TestAddValidPublicationsC()
        {
            var publications = Enumerable.Range(0, ApplicationOptions.Options.C - 1).Select(i => new Publication { NumberOfPages = i }).ToList();
            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>(publications)
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsC");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidPublicationOneDomainFromThreeBooks.
        /// </summary>
        [Test]
        public void TestAddInvalidPublicationOneDomainFromThreeBooks()
        {
            var domain1 = new Domain { Name = "Science" };

            var book1 = new Book { Name = "Stars", Domains = new List<Domain> { domain1 } };
            var book2 = new Book { Name = "Stars and galaxies", Domains = new List<Domain> { domain1 } };
            var book3 = new Book { Name = "Stars and galaxies complete edition", Domains = new List<Domain> { domain1 } };

            var publication1 = new Publication { NumberOfPages = 487, Book = book1 };
            var publication2 = new Publication { NumberOfPages = 346, Book = book2 };
            var publication3 = new Publication { NumberOfPages = 890, Book = book3 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>()
                {
                    publication1,
                    publication2,
                    publication3
                }
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsDomain");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidPublicationTwoDomainsFromThreeBooks.
        /// </summary>
        [Test]
        public void TestAddValidPublicationTwoDomainsFromThreeBooks()
        {
            var domain1 = new Domain { Name = "Science" };
            var domain2 = new Domain { Name = "Fiction" };

            var book1 = new Book { Name = "Stars", Domains = new List<Domain> { domain1 } };
            var book2 = new Book { Name = "Stars and galaxies", Domains = new List<Domain> { domain1 } };
            var book3 = new Book { Name = "Origin", Domains = new List<Domain> { domain2 } };

            var publication1 = new Publication { NumberOfPages = 487, Book = book1 };
            var publication2 = new Publication { NumberOfPages = 346, Book = book2 };
            var publication3 = new Publication { NumberOfPages = 890, Book = book3 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>()
                {
                    publication1,
                    publication2,
                    publication3
                }
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsDomain");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidPublicationOneDomainsFromTwoBooks.
        /// </summary>
        [Test]
        public void TestAddValidPublicationOneDomainsFromTwoBooks()
        {
            var domain1 = new Domain { Name = "Science" };

            var book1 = new Book { Name = "Stars", Domains = new List<Domain> { domain1 } };
            var book2 = new Book { Name = "Stars and galaxies", Domains = new List<Domain> { domain1 } };

            var publication1 = new Publication { NumberOfPages = 487, Book = book1 };
            var publication2 = new Publication { NumberOfPages = 346, Book = book2 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>()
                {
                    publication1,
                    publication2
                }
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsDomain");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidPublicationStockAllBooksLecture.
        /// </summary>
        [Test]
        public void TestAddInvalidPublicationStockAllBooksLecture()
        {
            var book = new Book { Name = "Stars" };

            var stock = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 50 };

            var publication = new Publication { NumberOfPages = 487, Book = book, Stock = stock };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>()
                {
                    publication
                },
                Extensions = new List<Extension>()
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationStock");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidPublicationStockLess10Percent.
        /// </summary>
        [Test]
        public void TestAddInvalidPublicationStockLess10Percent()
        {
            var book = new Book { Name = "Stars" };

            var stock = new Stock { InitialStock = 50, RentedStock = 37, NumberOfBooksForLecture = 10 };

            var publication = new Publication { NumberOfPages = 487, Book = book, Stock = stock };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>()
                {
                    publication
                },
                Extensions = new List<Extension>()
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationStock");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidMultiplePublicationStockAllBooksLecture.
        /// </summary>
        [Test]
        public void TestAddInvalidMultiplePublicationStockAllBooksLecture()
        {
            var book1 = new Book { Name = "Stars" };
            var book2 = new Book { Name = "Galaxies" };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };
            var stock2 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 50 };

            var publication1 = new Publication { NumberOfPages = 487, Book = book1, Stock = stock1 };
            var publication2 = new Publication { NumberOfPages = 487, Book = book2, Stock = stock2 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>()
                {
                    publication1,
                    publication2
                },
                Extensions = new List<Extension>()
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationStock");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidMultiplePublicationStockLess10Percent.
        /// </summary>
        [Test]
        public void TestAddInvalidMultiplePublicationStockLess10Percent()
        {
            var book1 = new Book { Name = "Stars" };
            var book2 = new Book { Name = "Galaxies" };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };
            var stock2 = new Stock { InitialStock = 50, RentedStock = 37, NumberOfBooksForLecture = 50 };

            var publication1 = new Publication { NumberOfPages = 487, Book = book1, Stock = stock1 };
            var publication2 = new Publication { NumberOfPages = 487, Book = book2, Stock = stock2 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>()
                {
                    publication1,
                    publication2
                },
                Extensions = new List<Extension>()
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationStock");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidMultiplePublicationStock.
        /// </summary>
        [Test]
        public void TestAddValidMultiplePublicationStock()
        {
            var book1 = new Book { Name = "Stars" };
            var book2 = new Book { Name = "Galaxies" };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };
            var stock2 = new Stock { InitialStock = 50, RentedStock = 32, NumberOfBooksForLecture = 50 };

            var publication1 = new Publication { NumberOfPages = 487, Book = book1, Stock = stock1 };
            var publication2 = new Publication { NumberOfPages = 487, Book = book2, Stock = stock2 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>()
                {
                    publication1,
                    publication2
                },
                Extensions = new List<Extension>()
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationStock");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidPublicationStock.
        /// </summary>
        [Test]
        public void TestAddValidPublicationStock()
        {
            var book = new Book { Name = "Stars" };

            var stock = new Stock { InitialStock = 50, RentedStock = 32, NumberOfBooksForLecture = 10 };

            var publication = new Publication { NumberOfPages = 487, Book = book, Stock = stock };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>()
                {
                    publication
                },
                Extensions = new List<Extension>()
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationStock");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidExtensionsCount.
        /// </summary>
        [Test]
        public void TestAddInvalidExtensionsCount()
        {
            var extensions = Enumerable.Range(0, ApplicationOptions.Options.LIM + 1).Select(i => new Extension { CreationDate = DateTime.Today.AddDays(-i - 1) }).ToList();

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>(),
                Extensions = new List<Extension>(extensions)
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateExtentions");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidExtensionsCount.
        /// </summary>
        [Test]
        public void TestAddValidExtensionsCount()
        {
            var extensions = Enumerable.Range(0, ApplicationOptions.Options.LIM - 1).Select(i => new Extension { CreationDate = DateTime.Today.AddDays(-i - 1) }).ToList();

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>(),
                Extensions = new List<Extension>(extensions)
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateExtentions");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidNumberOfRentedBooksWithinPeriodOneWithdrawal.
        /// </summary>
        [Test]
        public void TestAddInvalidNumberOfRentedBooksWithinPeriodOneWithdrawal()
        {
            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            var readerPublications = Enumerable.Range(0, ApplicationOptions.Options.NMC).Select(i => new Publication { NumberOfPages = i, Stock = stock1 }).ToList();

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.PER + ApplicationOptions.Options.PER)),
                DueDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.PER - 1)),
                Publications = new List<Publication>(readerPublications)
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal1
                }
            };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { new Publication { NumberOfPages = 10, Stock = stock1 } },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsBook");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidNumberOfRentedBooksWithinPeriodOneWithdrawal.
        /// </summary>
        [Test]
        public void TestAddValidNumberOfRentedBooksWithinPeriodOneWithdrawal()
        {
            var readerPublications = Enumerable.Range(0, ApplicationOptions.Options.NMC - 1).Select(i => new Publication { NumberOfPages = i }).ToList();

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.PER + ApplicationOptions.Options.PER)),
                DueDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.PER - 1)),
                Publications = new List<Publication>(readerPublications)
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal1
                }
            };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { new Publication { NumberOfPages = 10 } },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsBook");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidNumberOfRentedBooksWithinPeriodMultipleWithdrawals.
        /// </summary>
        [Test]
        public void TestAddInvalidNumberOfRentedBooksWithinPeriodMultipleWithdrawals()
        {
            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            var readerPublications1 = Enumerable.Range(0, ApplicationOptions.Options.NMC / 2).Select(i => new Publication { NumberOfPages = i }).ToList();
            var readerPublications2 = Enumerable.Range(ApplicationOptions.Options.NMC / 2, ApplicationOptions.Options.NMC - 1).Select(i => new Publication { NumberOfPages = i }).ToList();

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.PER + ApplicationOptions.Options.PER)),
                DueDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.PER - 1)),
                Publications = new List<Publication>(readerPublications1)
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.PER + ApplicationOptions.Options.PER)),
                DueDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.PER - 1)),
                Publications = new List<Publication>(readerPublications2)
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal1,
                    readerWithdrawal2
                }
            };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { new Publication { NumberOfPages = 10, Stock = stock1 } },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsBook");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidNumberOfRentedBooksWithinPeriodMultipleWithdrawals.
        /// </summary>
        [Test]
        public void TestAddValidNumberOfRentedBooksWithinPeriodMultipleWithdrawals()
        {
            var readerPublications1 = Enumerable.Range(0, ApplicationOptions.Options.NMC / 2).Select(i => new Publication { NumberOfPages = i }).ToList();
            var readerPublications2 = Enumerable.Range(ApplicationOptions.Options.NMC / 2, ApplicationOptions.Options.NMC - 2).Select(i => new Publication { NumberOfPages = i }).ToList();

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.PER + ApplicationOptions.Options.PER)),
                DueDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.PER - 1)),
                Publications = new List<Publication>(readerPublications1)
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.PER + ApplicationOptions.Options.PER)),
                DueDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.PER - 1)),
                Publications = new List<Publication>(readerPublications2)
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal1
                }
            };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { new Publication { NumberOfPages = 10 } },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsBook");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidNumberOfRentedBooksWithinSameDomain.
        /// </summary>
        [Test]
        public void TestAddInvalidNumberOfRentedBooksWithinSameDomain()
        {
            var domain1 = new Domain { Name = "Science" };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            var publications = Enumerable.Range(0, ApplicationOptions.Options.D).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { domain1 } },
                Stock = stock1
            }).ToList();

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddMonths(-(ApplicationOptions.Options.L + 2)),
                DueDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.L + 1)),
                Publications = new List<Publication>(publications)
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal1
                }
            };

            var withdrawalBook1 = new Book { Domains = new List<Domain> { domain1 } };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { domain1 } };
            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1, Stock = stock1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2, Stock = stock1 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { withdrawalPublication1, withdrawalPublication2 },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalBookDomain");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidNumberOfRentedBooksWithinSameDomain.
        /// </summary>
        [Test]
        public void TestAddValidNumberOfRentedBooksWithinSameDomain()
        {
            var domain1 = new Domain { Name = "Science" };

            var book1 = new Book { Domains = new List<Domain> { domain1 } };
            var book2 = new Book { Domains = new List<Domain> { domain1 } };

            var publications = Enumerable.Range(0, ApplicationOptions.Options.D - 1).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { domain1 } },
            }).ToList();

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddMonths(-(ApplicationOptions.Options.L + 2)),
                DueDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.L + 1)),
                Publications = new List<Publication>(publications)
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal1
                }
            };

            var withdrawalBook = new Book { Domains = new List<Domain> { domain1 } };
            var withdrawalPublication = new Publication { NumberOfPages = 10, Book = withdrawalBook };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { withdrawalPublication },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalBookDomain");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidNumberOfRentedBooksWithinSameDomainMultipleWithdrawals.
        /// </summary>
        [Test]
        public void TestAddInvalidNumberOfRentedBooksWithinSameDomainMultipleWithdrawals()
        {
            var domain1 = new Domain { Name = "Science" };

            var book1 = new Book { Domains = new List<Domain> { domain1 } };
            var book2 = new Book { Domains = new List<Domain> { domain1 } };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            var publications1 = Enumerable.Range(0, ApplicationOptions.Options.D / 2).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { domain1 } },
            }).ToList();

            var publications2 = Enumerable.Range(ApplicationOptions.Options.D / 2, ApplicationOptions.Options.D).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { domain1 } },
            }).ToList();

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddMonths(-(ApplicationOptions.Options.L + 2)),
                DueDate = DateTime.Today.AddMonths(-(ApplicationOptions.Options.L + 1)),
                Publications = new List<Publication>(publications1)
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddMonths(-(ApplicationOptions.Options.L + 2)),
                DueDate = DateTime.Today.AddMonths(-(ApplicationOptions.Options.L + 1)),
                Publications = new List<Publication>(publications2)
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal1,
                    readerWithdrawal2
                }
            };

            var withdrawalBook1 = new Book { Domains = new List<Domain> { domain1 } };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { domain1 } };
            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1, Stock = stock1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2, Stock = stock1 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { withdrawalPublication1, withdrawalPublication2 },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalBookDomain");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidNumberOfRentedBooksWithinSameDomainMultipleWithdrawal.
        /// </summary>
        [Test]
        public void TestAddValidNumberOfRentedBooksWithinSameDomainMultipleWithdrawal()
        {
            var domain1 = new Domain { Name = "Science" };

            var book1 = new Book { Domains = new List<Domain> { domain1 } };
            var book2 = new Book { Domains = new List<Domain> { domain1 } };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            var publications1 = Enumerable.Range(0, ApplicationOptions.Options.D / 2).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { domain1 } },
                Stock = stock1
            }).ToList();

            var publications2 = Enumerable.Range(ApplicationOptions.Options.D / 2, ApplicationOptions.Options.D - 2).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { domain1 } },
                Stock = stock1
            }).ToList();

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddMonths(-(ApplicationOptions.Options.L + 2)),
                DueDate = DateTime.Today.AddMonths(-(ApplicationOptions.Options.L + 1)),
                Publications = new List<Publication>(publications1)
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddMonths(-(ApplicationOptions.Options.L + 2)),
                DueDate = DateTime.Today.AddMonths(-(ApplicationOptions.Options.L + 1)),
                Publications = new List<Publication>(publications2)
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal1,
                    readerWithdrawal2
                }
            };

            var withdrawalBook = new Book { Domains = new List<Domain> { domain1 } };
            var withdrawalPublication = new Publication { NumberOfPages = 10, Book = withdrawalBook, Stock = stock1 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { withdrawalPublication },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalBookDomain");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidNumberOfRentedBooksWithinSameDay.
        /// </summary>
        [Test]
        public void TestAddInvalidNumberOfRentedBooksWithinSameDay()
        {
            var domain1 = new Domain { Name = "Science" };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            var publications = Enumerable.Range(0, ApplicationOptions.Options.NCZ - 2).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { domain1 } },
                Stock = stock1
            }).ToList();

            var readerWithdrawal = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Publications = new List<Publication>(publications)
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal,
                }
            };

            var withdrawalPublications = Enumerable.Range(ApplicationOptions.Options.NCZ - 2, ApplicationOptions.Options.NCZ).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { new Domain { Name = "Domain" + i } } },
                Stock = stock1
            }).ToList();

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>(withdrawalPublications),
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalBookDay");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidNumberOfRentedBooksWithinSameDay.
        /// </summary>
        [Test]
        public void TestAddValidNumberOfRentedBooksWithinSameDay()
        {
            var domain1 = new Domain { Name = "Science" };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            var publications = Enumerable.Range(0, ApplicationOptions.Options.NCZ - 2).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { domain1 } },
                Stock = stock1
            }).ToList();

            var readerWithdrawal = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Publications = new List<Publication>(publications)
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal,
                }
            };

            var withdrawalBook = new Book { Domains = new List<Domain> { new Domain { Name = "Art" } } };

            var withdrawalPublication = new Publication { NumberOfPages = 10, Book = withdrawalBook };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>
                {
                    withdrawalPublication
                },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalBookDay");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidNumberOfRentedBooksWithinSameDayMultipleWithdrawal.
        /// </summary>
        [Test]
        public void TestAddInvalidNumberOfRentedBooksWithinSameDayMultipleWithdrawal()
        {
            var domain1 = new Domain { Name = "Science" };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            var publications1 = Enumerable.Range(0, ApplicationOptions.Options.NCZ / 2).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { domain1 } },
                Stock = stock1
            }).ToList();

            var publications2 = Enumerable.Range(ApplicationOptions.Options.NCZ / 2, ApplicationOptions.Options.NCZ - 2).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { domain1 } },
                Stock = stock1
            }).ToList();

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Publications = new List<Publication>(publications1)
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Publications = new List<Publication>(publications2)
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal1,
                    readerWithdrawal2
                }
            };

            var withdrawalPublications = Enumerable.Range(ApplicationOptions.Options.NCZ - 2, ApplicationOptions.Options.NCZ).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { new Domain { Name = "Domain" + i } } },
                Stock = stock1
            }).ToList();

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>(withdrawalPublications),
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalBookDay");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidNumberOfRentedBooksWithinSameDayMultipleWithdrawal.
        /// </summary>
        [Test]
        public void TestAddValidNumberOfRentedBooksWithinSameDayMultipleWithdrawal()
        {
            var domain1 = new Domain { Name = "Science" };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            var publications1 = Enumerable.Range(0, ApplicationOptions.Options.NCZ / 2).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { domain1 } },
                Stock = stock1
            }).ToList();

            var publications2 = Enumerable.Range(ApplicationOptions.Options.NCZ / 2, ApplicationOptions.Options.NCZ - 2).Select(i => new Publication
            {
                NumberOfPages = i,
                Book = new Book { Domains = new List<Domain> { domain1 } },
                Stock = stock1
            }).ToList();

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Publications = new List<Publication>(publications1)
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Publications = new List<Publication>(publications2)
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal1,
                    readerWithdrawal2
                }
            };

            var withdrawalBook = new Book { Domains = new List<Domain> { new Domain { Name = "Art" } } };

            var withdrawalPublication = new Publication { NumberOfPages = 10, Book = withdrawalBook };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>
                {
                    withdrawalPublication
                },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalBookDay");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidRentSameBookWithinGracePeriod.
        /// </summary>
        [Test]
        public void TestAddInvalidRentSameBookWithinGracePeriod()
        {
            var domain1 = new Domain { Name = "Science" };

            var book1 = new Book { Domains = new List<Domain> { domain1 }, Name = "book1" };
            var book2 = new Book { Domains = new List<Domain> { domain1 }, Name = "book2" };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1, Stock = stock1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2, Stock = stock1 };

            var readerWithdrawal = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-ApplicationOptions.Options.DELTA + 1),
                DueDate = DateTime.Today.AddDays(3),
                Publications = new List<Publication>
                {
                    readerPublication1,
                    readerPublication2
                }
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal
                }
            };

            var withdrawalBook1 = new Book { Domains = new List<Domain> { new Domain { Name = "Art" } }, Name = "withdrawalBook1" };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { new Domain { Name = "Fiction" } }, Name = "withdrawalBook2" };

            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1, Stock = stock1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2, Stock = stock1 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>
                {
                    withdrawalPublication1,
                    withdrawalPublication2,
                    readerPublication1
                },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalSameBookGraceDate");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidRentSameBookWithinGracePeriod.
        /// </summary>
        [Test]
        public void TestAddValidRentSameBookWithinGracePeriod()
        {
            var domain1 = new Domain { Name = "Science" };

            var book1 = new Book { Domains = new List<Domain> { domain1 }, Name = "book1" };
            var book2 = new Book { Domains = new List<Domain> { domain1 }, Name = "book2" };

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2 };

            var readerWithdrawal = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.DELTA * 3)),
                DueDate = DateTime.Today.AddDays(-15),
                Publications = new List<Publication>
                {
                    readerPublication1,
                    readerPublication2
                }
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal
                }
            };

            var withdrawalBook1 = new Book { Domains = new List<Domain> { new Domain { Name = "Art" } }, Name = "withdrawalBook1" };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { new Domain { Name = "Fiction" } }, Name = "withdrawalBook2" };

            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>
                {
                    withdrawalPublication1,
                    withdrawalPublication2,
                    readerPublication1
                },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalSameBookGraceDate");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidRentSameBookWithinGracePeriodMultipleWithdrawals.
        /// </summary>
        [Test]
        public void TestAddInvalidRentSameBookWithinGracePeriodMultipleWithdrawals()
        {
            var domain1 = new Domain { Name = "Science" };

            var book1 = new Book { Domains = new List<Domain> { domain1 }, Name = "book1" };
            var book2 = new Book { Domains = new List<Domain> { domain1 }, Name = "book2" };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1, Stock = stock1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2, Stock = stock1 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-ApplicationOptions.Options.DELTA + 1),
                DueDate = DateTime.Today.AddDays(3),
                Publications = new List<Publication>
                {
                    readerPublication1
                }
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-ApplicationOptions.Options.DELTA + 1),
                DueDate = DateTime.Today.AddDays(3),
                Publications = new List<Publication>
                {
                    readerPublication2
                }
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal1,
                    readerWithdrawal2
                }
            };

            var withdrawalBook1 = new Book { Domains = new List<Domain> { new Domain { Name = "Art" } }, Name = "withdrawalBook1" };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { new Domain { Name = "Fiction" } }, Name = "withdrawalBook2" };

            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1, Stock = stock1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2, Stock = stock1 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>
                {
                    withdrawalPublication1,
                    withdrawalPublication2,
                    readerPublication2
                },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalSameBookGraceDate");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidRentSameBookWithinGracePeriodMultipleWithdrawals.
        /// </summary>
        [Test]
        public void TestAddValidRentSameBookWithinGracePeriodMultipleWithdrawals()
        {
            var domain1 = new Domain { Name = "Science" };

            var book1 = new Book { Domains = new List<Domain> { domain1 }, Name = "book1" };
            var book2 = new Book { Domains = new List<Domain> { domain1 }, Name = "book2" };

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.DELTA * 3)),
                DueDate = DateTime.Today.AddDays(-15),
                Publications = new List<Publication>
                {
                    readerPublication1
                }
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-(ApplicationOptions.Options.DELTA * 2)),
                DueDate = DateTime.Today.AddDays(-15),
                Publications = new List<Publication>
                {
                    readerPublication2
                }
            };

            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
                {
                    readerWithdrawal1,
                    readerWithdrawal2
                }
            };

            var withdrawalBook1 = new Book { Domains = new List<Domain> { new Domain { Name = "Art" } }, Name = "withdrawalBook1" };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { new Domain { Name = "Fiction" } }, Name = "withdrawalBook2" };

            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>
                {
                    withdrawalPublication1,
                    withdrawalPublication2,
                    readerPublication2
                },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalSameBookGraceDate");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddLibrarianValidPublicationsC.
        /// </summary>
        [Test]
        public void TestAddLibrarianValidPublicationsC()
        {
            var publications = Enumerable.Range(0, ApplicationOptions.Options.C + 1).Select(i => new Publication { NumberOfPages = i }).ToList();
            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>(publications),
                Reader = new Librarian()
            };

            var results = this.WithdrawalService.Create(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsC");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestWithdrawalGetterSetterId.
        /// </summary>
        [Test]
        public void TestWithdrawalGetterSetterId()
        {
            int id = 5;

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>(),
                Extensions = new List<Extension>(),
                RentedDate = DateTime.Today.AddDays(-5),
                DueDate = DateTime.Today.AddDays(5)
            };

            typeof(Withdrawal).GetProperty(nameof(Withdrawal.Id)).SetValue(withdrawal, id);

            Assert.IsTrue(id == withdrawal.Id);
        }

        /// <summary>
        /// Defines the test method TestAddValidWithdrawal.
        /// </summary>
        [Test]
        public void TestAddValidWithdrawal()
        {
            var reader = new Reader()
            {
                Withdrawals = new List<Withdrawal>()
            };

            var withdrawalBook1 = new Book { Domains = new List<Domain> { new Domain { Name = "Art" } }, Name = "withdrawalBook1" };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { new Domain { Name = "Fiction" } }, Name = "withdrawalBook2" };

            var stock1 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };
            var stock2 = new Stock { InitialStock = 50, RentedStock = 0, NumberOfBooksForLecture = 10 };

            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1, Stock = stock1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2, Stock = stock2 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>
                {
                    withdrawalPublication1,
                    withdrawalPublication2,
                },
                Extensions = new List<Extension>(),
                Reader = reader,
                RentedDate = DateTime.Today.AddDays(-5),
                DueDate = DateTime.Today.AddDays(5)
            };

            var results = this.WithdrawalService.Create(withdrawal);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
