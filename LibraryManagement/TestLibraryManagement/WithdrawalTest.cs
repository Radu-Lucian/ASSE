// <copyright file="WithdrawalTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Withdrawal Test class. </summary>
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
        /// Defines the test method TestAddNullFirstName.
        /// </summary>
        [Test]
        public void TestAdd()
        {
            var withdrawal = new Withdrawal
            {
            };

            var results = this.WithdrawalService.CreateWithdrawal(withdrawal);
            var tag = results.FirstOrDefault(res => res.Tag == "");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }
    }
}
