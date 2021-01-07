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

            Assert.Throws<ArgumentNullException>(() => this.WithdrawalService.CreateWithdrawal(nullWithdrawal));
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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
            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>()
                {
                    new Publication { NumberOfPages = 5 },
                    new Publication { NumberOfPages = 7 },
                    new Publication { NumberOfPages = 7 },
                    new Publication { NumberOfPages = 9 },
                    new Publication { NumberOfPages = 87 },
                    new Publication { NumberOfPages = 688 }
                }
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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
            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>()
                {
                    new Publication { NumberOfPages = 5 },
                    new Publication { NumberOfPages = 7 }
                }
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidatePublicationsDomain");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidExtensionsCount.
        /// </summary>
        [Test]
        public void TestAddInvalidExtensionsCount()
        {
            var extension1 = new Extension { CreationDate = DateTime.Today.AddDays(-6) };
            var extension2 = new Extension { CreationDate = DateTime.Today.AddDays(-5) };
            var extension3 = new Extension { CreationDate = DateTime.Today.AddDays(-4) };
            var extension4 = new Extension { CreationDate = DateTime.Today.AddDays(-3) };
            var extension5 = new Extension { CreationDate = DateTime.Today.AddDays(-2) };
            var extension6 = new Extension { CreationDate = DateTime.Today.AddDays(-1) };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>(),
                Extensions = new List<Extension>
                {
                    extension1,
                    extension2,
                    extension3,
                    extension4,
                    extension5,
                    extension6
                }
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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
            var extension1 = new Extension { CreationDate = DateTime.Today.AddDays(-6) };
            var extension2 = new Extension { CreationDate = DateTime.Today.AddDays(-5) };
            var extension3 = new Extension { CreationDate = DateTime.Today.AddDays(-4) };
            var extension4 = new Extension { CreationDate = DateTime.Today.AddDays(-3) };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>(),
                Extensions = new List<Extension>
                {
                    extension1,
                    extension2,
                    extension3,
                    extension4
                }
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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
            var readerPublication1 = new Publication { NumberOfPages = 5 };
            var readerPublication2 = new Publication { NumberOfPages = 5 };
            var readerPublication3 = new Publication { NumberOfPages = 5 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-30),
                DueDate = DateTime.Today.AddDays(-10),
                Publications = new List<Publication>
                {
                    readerPublication1,
                    readerPublication2,
                    readerPublication3
                }
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateExtentions");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidNumberOfRentedBooksWithinPeriodOneWithdrawal.
        /// </summary>
        [Test]
        public void TestAddValidNumberOfRentedBooksWithinPeriodOneWithdrawal()
        {
            var readerPublication1 = new Publication { NumberOfPages = 5 };
            var readerPublication2 = new Publication { NumberOfPages = 5 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-30),
                DueDate = DateTime.Today.AddDays(-10),
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
                    readerWithdrawal1
                }
            };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { new Publication { NumberOfPages = 10 } },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateExtentions");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidNumberOfRentedBooksWithinPeriodMultipleWithdrawals.
        /// </summary>
        [Test]
        public void TestAddInvalidNumberOfRentedBooksWithinPeriodMultipleWithdrawals()
        {
            var readerPublication1 = new Publication { NumberOfPages = 5 };
            var readerPublication2 = new Publication { NumberOfPages = 5 };
            var readerPublication3 = new Publication { NumberOfPages = 5 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-70),
                DueDate = DateTime.Today.AddDays(-40),
                Publications = new List<Publication>
                {
                    readerPublication1,
                }
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-30),
                DueDate = DateTime.Today.AddDays(-10),
                Publications = new List<Publication>
                {
                    readerPublication2,
                    readerPublication3
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

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { new Publication { NumberOfPages = 10 } },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateExtentions");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidNumberOfRentedBooksWithinPeriodMultipleWithdrawals.
        /// </summary>
        [Test]
        public void TestAddValidNumberOfRentedBooksWithinPeriodMultipleWithdrawals()
        {
            var readerPublication1 = new Publication { NumberOfPages = 5 };
            var readerPublication2 = new Publication { NumberOfPages = 5 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-70),
                DueDate = DateTime.Today.AddDays(-40),
                Publications = new List<Publication>
                {
                    readerPublication1
                }
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-30),
                DueDate = DateTime.Today.AddDays(-10),
                Publications = new List<Publication>
                {
                    readerPublication2
                }
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateExtentions");

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

            var book1 = new Book { Domains = new List<Domain> { domain1 } };
            var book2 = new Book { Domains = new List<Domain> { domain1 } };

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-70),
                DueDate = DateTime.Today.AddDays(-40),
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
                    readerWithdrawal1
                }
            };

            var withdrawalBook1 = new Book { Domains = new List<Domain> { domain1 } };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { domain1 } };
            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { withdrawalPublication1, withdrawalPublication2 },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-70),
                DueDate = DateTime.Today.AddDays(-40),
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-70),
                DueDate = DateTime.Today.AddDays(-40),
                Publications = new List<Publication>
                {
                    readerPublication1
                }
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-70),
                DueDate = DateTime.Today.AddDays(-40),
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

            var withdrawalBook1 = new Book { Domains = new List<Domain> { domain1 } };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { domain1 } };
            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { withdrawalPublication1, withdrawalPublication2 },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-70),
                DueDate = DateTime.Today.AddDays(-40),
                Publications = new List<Publication>
                {
                    readerPublication1
                }
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddDays(-70),
                DueDate = DateTime.Today.AddDays(-40),
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

            var withdrawalBook = new Book { Domains = new List<Domain> { domain1 } };
            var withdrawalPublication = new Publication { NumberOfPages = 10, Book = withdrawalBook };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication> { withdrawalPublication },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var book1 = new Book { Domains = new List<Domain> { domain1 } };
            var book2 = new Book { Domains = new List<Domain> { domain1 } };

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2 };

            var readerWithdrawal = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
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
                    readerWithdrawal,
                }
            };

            var withdrawalBook1 = new Book { Domains = new List<Domain> { new Domain { Name = "Art" } } };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { new Domain { Name = "Fiction" } } };
            var withdrawalBook3 = new Book { Domains = new List<Domain> { new Domain { Name = "Science" } } };
            var withdrawalBook4 = new Book { Domains = new List<Domain> { new Domain { Name = "Philosophy" } } };

            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2 };
            var withdrawalPublication3 = new Publication { NumberOfPages = 10, Book = withdrawalBook3 };
            var withdrawalPublication4 = new Publication { NumberOfPages = 10, Book = withdrawalBook4 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>
                {
                    withdrawalPublication1,
                    withdrawalPublication2,
                    withdrawalPublication3,
                    withdrawalPublication4
                },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var book1 = new Book { Domains = new List<Domain> { domain1 } };
            var book2 = new Book { Domains = new List<Domain> { domain1 } };

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2 };

            var readerWithdrawal = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
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
                    readerWithdrawal,
                }
            };

            var withdrawalBook1 = new Book { Domains = new List<Domain> { new Domain { Name = "Art" } } };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { new Domain { Name = "Fiction" } } };
            var withdrawalBook3 = new Book { Domains = new List<Domain> { new Domain { Name = "Science" } } };

            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2 };
            var withdrawalPublication3 = new Publication { NumberOfPages = 10, Book = withdrawalBook3 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>
                {
                    withdrawalPublication1,
                    withdrawalPublication2,
                    withdrawalPublication3
                },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var book1 = new Book { Domains = new List<Domain> { domain1 } };
            var book2 = new Book { Domains = new List<Domain> { domain1 } };

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Publications = new List<Publication>
                {
                    readerPublication1
                }
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
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

            var withdrawalBook1 = new Book { Domains = new List<Domain> { new Domain { Name = "Art" } } };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { new Domain { Name = "Fiction" } } };
            var withdrawalBook3 = new Book { Domains = new List<Domain> { new Domain { Name = "Science" } } };
            var withdrawalBook4 = new Book { Domains = new List<Domain> { new Domain { Name = "Philosophy" } } };

            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2 };
            var withdrawalPublication3 = new Publication { NumberOfPages = 10, Book = withdrawalBook3 };
            var withdrawalPublication4 = new Publication { NumberOfPages = 10, Book = withdrawalBook4 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>
                {
                    withdrawalPublication1,
                    withdrawalPublication2,
                    withdrawalPublication3,
                    withdrawalPublication4
                },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var book1 = new Book { Domains = new List<Domain> { domain1 } };
            var book2 = new Book { Domains = new List<Domain> { domain1 } };

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Publications = new List<Publication>
                {
                    readerPublication1
                }
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
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

            var withdrawalBook1 = new Book { Domains = new List<Domain> { new Domain { Name = "Art" } } };
            var withdrawalBook2 = new Book { Domains = new List<Domain> { new Domain { Name = "Fiction" } } };
            var withdrawalBook3 = new Book { Domains = new List<Domain> { new Domain { Name = "Science" } } };

            var withdrawalPublication1 = new Publication { NumberOfPages = 10, Book = withdrawalBook1 };
            var withdrawalPublication2 = new Publication { NumberOfPages = 10, Book = withdrawalBook2 };
            var withdrawalPublication3 = new Publication { NumberOfPages = 10, Book = withdrawalBook3 };

            var withdrawal = new Withdrawal
            {
                Publications = new List<Publication>
                {
                    withdrawalPublication1,
                    withdrawalPublication2,
                    withdrawalPublication3
                },
                Extensions = new List<Extension>(),
                Reader = reader
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2 };

            var readerWithdrawal = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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
                RentedDate = DateTime.Today.AddMonths(-2),
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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

            var readerPublication1 = new Publication { NumberOfPages = 5, Book = book1 };
            var readerPublication2 = new Publication { NumberOfPages = 5, Book = book2 };

            var readerWithdrawal1 = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Publications = new List<Publication>
                {
                    readerPublication1
                }
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
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
                RentedDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Publications = new List<Publication>
                {
                    readerPublication1
                }
            };

            var readerWithdrawal2 = new Withdrawal
            {
                RentedDate = DateTime.Today.AddMonths(-2),
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

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateWithdrawalSameBookGraceDate");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidWithdrawal.
        /// </summary>
        [Test]
        public void TestAddValidWithdrawal()
        {
            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
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
                },
                Extensions = new List<Extension>(),
                Reader = reader,
                RentedDate = DateTime.Today.AddDays(-5),
                DueDate = DateTime.Today.AddDays(5)
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
