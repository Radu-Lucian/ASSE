// <copyright file="ExtensionTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Extension Test class. </summary>
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
    /// Defines test class ExtensionTest.
    /// </summary>
    [TestFixture]
    public class ExtensionTest
    {
        /// <summary>
        /// Gets or sets the extension service.
        /// </summary>
        /// <value>The extension service.</value>
        private ExtensionService ExtensionService { get; set; }

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
            var mockSet = new Mock<DbSet<Extension>>();
            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Extension>()).Returns(mockSet.Object);
            this.ExtensionService = new ExtensionService(new ExtensionRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddNullExtension.
        /// </summary>
        [Test]
        public void TestAddNullExtension()
        {
            Extension nullExtention = null;

            Assert.Throws<ArgumentNullException>(() => this.ExtensionService.Create(nullExtention));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidExtraDaysLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidExtraDaysLowerBoundary()
        {
            var extention = new Extension
            {
                ExtraDays = 0
            };

            var results = this.ExtensionService.Create(extention);
            var tag = results.FirstOrDefault(res => res.Tag == "ExtentionsExtraDays");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidExtraDaysUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidExtraDaysUpperBoundary()
        {
            var extention = new Extension
            {
                ExtraDays = 370
            };

            var results = this.ExtensionService.Create(extention);
            var tag = results.FirstOrDefault(res => res.Tag == "ExtentionsExtraDays");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidExtraDays.
        /// </summary>
        [Test]
        public void TestAddValidExtraDays()
        {
            var extention = new Extension
            {
                ExtraDays = 5
            };

            var results = this.ExtensionService.Create(extention);
            var tag = results.FirstOrDefault(res => res.Tag == "ExtentionsExtraDays");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestInvalidCreationDateLowerBoundary.
        /// </summary>
        [Test]
        public void TestInvalidCreationDateLowerBoundary()
        {
            var extention = new Extension
            {
                CreationDate = new DateTime(2009, 12, 31)
            };

            var results = this.ExtensionService.Create(extention);
            var tag = results.FirstOrDefault(res => res.Tag == "ExtentionsCreationDate");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestInvalidCreationDateUpperBoundary.
        /// </summary>
        [Test]
        public void TestInvalidCreationDateUpperBoundary()
        {
            var extention = new Extension
            {
                CreationDate = new DateTime(2100, 1, 2)
            };

            var results = this.ExtensionService.Create(extention);
            var tag = results.FirstOrDefault(res => res.Tag == "ExtentionsCreationDate");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestInvalidCreationDatePastToday.
        /// </summary>
        [Test]
        public void TestInvalidCreationDatePastToday()
        {
            var extention = new Extension
            {
                CreationDate = DateTime.Today.AddDays(1)
            };

            var results = this.ExtensionService.Create(extention);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateExtentionCreationDate");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestValidCreationDate.
        /// </summary>
        [Test]
        public void TestValidCreationDate()
        {
            var extention = new Extension
            {
                CreationDate = DateTime.Today.AddDays(-1)
            };

            var results = this.ExtensionService.Create(extention);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateExtentionCreationDate");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestNullWithdrawal.
        /// </summary>
        [Test]
        public void TestNullWithdrawal()
        {
            var extention = new Extension
            {
                Withdrawal = null
            };

            var results = this.ExtensionService.Create(extention);
            var tag = results.FirstOrDefault(res => res.Tag == "ExtentionWithdrawalNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestValidWithdrawal.
        /// </summary>
        [Test]
        public void TestValidWithdrawal()
        {
            var extention = new Extension
            {
                Withdrawal = new Withdrawal { }
            };

            var results = this.ExtensionService.Create(extention);
            var tag = results.FirstOrDefault(res => res.Tag == "ExtentionWithdrawalNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestValidExtension.
        /// </summary>
        [Test]
        public void TestValidExtension()
        {
            var extention = new Extension
            {
                ExtraDays = 5,
                CreationDate = DateTime.Today.AddDays(-5),
                Withdrawal = new Withdrawal { }
            };

            var results = this.ExtensionService.Create(extention);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
