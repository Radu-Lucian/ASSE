// <copyright file="LibrarianTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Librarian Test class. </summary>
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
    /// Class LibrarianTest.
    /// </summary>
    public class LibrarianTest
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
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var mockSet = new Mock<DbSet<Librarian>>();
            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Librarian>()).Returns(mockSet.Object);
            this.LibrarianService = new LibrarianService(new LibrarianRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddNullLibrarian.
        /// </summary>
        [Test]
        public void TestAddNullLibrarian()
        {
            Librarian nullLibrarian = null;

            Assert.Throws<ArgumentNullException>(() => this.LibrarianService.Create(nullLibrarian));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullFirstName.
        /// </summary>
        [Test]
        public void TestAddNullFirstName()
        {
            var librarian = new Librarian
            {
                FirstName = null
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderFirstNameNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthFirstNameLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthFirstNameLowerBoundary()
        {
            var librarian = new Librarian
            {
                FirstName = string.Empty
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderFirstNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthFirstNameUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthFirstNameUpperBoundary()
        {
            var librarian = new Librarian
            {
                FirstName = new string('a', 210)
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderFirstNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidLengthFirstName.
        /// </summary>
        [Test]
        public void TestAddValidLengthFirstName()
        {
            var librarian = new Librarian
            {
                FirstName = "Luci"
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderFirstNameLength");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullLastName.
        /// </summary>
        [Test]
        public void TestAddNullLastName()
        {
            var librarian = new Librarian
            {
                LastName = null
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderLastNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthLastNameLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthLastNameLowerBoundary()
        {
            var librarian = new Librarian
            {
                LastName = string.Empty
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderLastNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthLastNameUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthLastNameUpperBoundary()
        {
            var librarian = new Librarian
            {
                LastName = new string('a', 210)
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderLastNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidLengthLastName.
        /// </summary>
        [Test]
        public void TestAddValidLengthLastName()
        {
            var librarian = new Librarian
            {
                LastName = "Radu"
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderLastNameLength");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullAddress.
        /// </summary>
        [Test]
        public void TestAddNullAddress()
        {
            var librarian = new Librarian
            {
                Address = null
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderAddressNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthAddressLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthAddressLowerBoundary()
        {
            var librarian = new Librarian
            {
                Address = string.Empty
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderAddressLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidLengthAddressUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthAddressUpperBoundary()
        {
            var librarian = new Librarian
            {
                Address = new string('a', 210)
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderAddressLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidLengthAddress.
        /// </summary>
        [Test]
        public void TestAddValidLengthAddress()
        {
            var librarian = new Librarian
            {
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319"
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderAddressLength");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullPhoneNumber.
        /// </summary>
        [Test]
        public void TestAddNullPhoneNumber()
        {
            var librarian = new Librarian
            {
                PhoneNumber = null
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderPhoneNumberInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddEmptyPhoneNumber.
        /// </summary>
        [Test]
        public void TestAddEmptyPhoneNumber()
        {
            var librarian = new Librarian
            {
                PhoneNumber = string.Empty
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderPhoneNumberInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidPhoneNumber.
        /// </summary>
        /// <param name="invalidPhoneNumber">The invalid phone number.</param>
        [TestCase("0413564864")]
        [TestCase("0790512346")]
        [TestCase("867-5309")]
        public void TestAddInvalidPhoneNumber(string invalidPhoneNumber)
        {
            var librarian = new Librarian
            {
                PhoneNumber = invalidPhoneNumber
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderPhoneNumberInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidPhoneNumber.
        /// </summary>
        /// <param name="validPhoneNumber">The valid phone number.</param>
        [TestCase("+40213.564.864")]
        [TestCase("+40213 564 864")]
        [TestCase("0213-564-864")]
        [TestCase("0213564864")]
        [TestCase("0763564864")]
        public void TestAddValidPhoneNumber(string validPhoneNumber)
        {
            var librarian = new Librarian
            {
                PhoneNumber = validPhoneNumber
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderPhoneNumberInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullEmailAddress.
        /// </summary>
        [Test]
        public void TestAddNullEmailAddress()
        {
            var librarian = new Librarian
            {
                EmailAddress = null
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderEmailAddressNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddEmptyEmailAddress.
        /// </summary>
        [Test]
        public void TestAddEmptyEmailAddress()
        {
            var librarian = new Librarian
            {
                EmailAddress = string.Empty
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderEmailAddressInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidEmailAddress.
        /// </summary>
        [Test]
        public void TestAddInvalidEmailAddress()
        {
            var librarian = new Librarian
            {
                EmailAddress = "not an email address"
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderEmailAddressInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidEmailAddress.
        /// </summary>
        [Test]
        public void TestAddValidEmailAddress()
        {
            var librarian = new Librarian
            {
                EmailAddress = "radulucian.andrei@gmail.com"
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderEmailAddressInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidContactInformation.
        /// </summary>
        [Test]
        public void TestAddInvalidContactInformation()
        {
            var librarian = new Librarian
            {
                FirstName = "Lucian",
                LastName = "Radu",
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319",
                Gender = "M",
                Withdrawals = new List<Withdrawal> { new Withdrawal() }
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateReaderPhoneEmail");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidContactInformationWithEmail.
        /// </summary>
        [Test]
        public void TestAddValidContactInformationWithEmail()
        {
            var librarian = new Librarian
            {
                FirstName = "Lucian",
                LastName = "Radu",
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319",
                Gender = "M",
                EmailAddress = "radulucian.andrei@gmail.com",
                Withdrawals = new List<Withdrawal> { new Withdrawal() }
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateLibrarianPhoneEmail");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddValidContactInformationWithPhoneNumber.
        /// </summary>
        [Test]
        public void TestAddValidContactInformationWithPhoneNumber()
        {
            var librarian = new Librarian
            {
                FirstName = "Lucian",
                LastName = "Radu",
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319",
                Gender = "M",
                PhoneNumber = "0784000292",
                Withdrawals = new List<Withdrawal> { new Withdrawal() }
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateLibrarianPhoneEmail");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddNullGender.
        /// </summary>
        [Test]
        public void TestAddNullGender()
        {
            var librarian = new Librarian
            {
                Gender = null
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderGenderNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidGender.
        /// </summary>
        [Test]
        public void TestAddInvalidGender()
        {
            var librarian = new Librarian
            {
                Gender = "apache helicopter"
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderGenderInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidGender.
        /// </summary>
        /// <param name="validGender">The valid gender.</param>
        [TestCase("M")]
        [TestCase("m")]
        [TestCase("F")]
        [TestCase("f")]
        public void TestAddValidGender(string validGender)
        {
            var librarian = new Librarian
            {
                Gender = validGender
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderGenderInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullWithdrawals.
        /// </summary>
        [Test]
        public void TestAddNullWithdrawals()
        {
            var librarian = new Librarian
            {
                Withdrawals = null
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderWithdrawalsNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddEmptyWithdrawals.
        /// </summary>
        [Test]
        public void TestAddEmptyWithdrawals()
        {
            var librarian = new Librarian
            {
                Withdrawals = new List<Withdrawal>()
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderWithdrawalsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidWithdrawals.
        /// </summary>
        [Test]
        public void TestAddValidWithdrawals()
        {
            var librarian = new Librarian
            {
                Withdrawals = new List<Withdrawal>() { new Withdrawal() }
            };

            var results = this.LibrarianService.Create(librarian);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderWithdrawalsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidLibrarian.
        /// </summary>
        [Test]
        public void TestAddValidLibrarian()
        {
            var librarian = new Librarian
            {
                FirstName = "Lucian",
                LastName = "Radu",
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319",
                Gender = "M",
                EmailAddress = "radulucian.andrei@gmail.com",
                PhoneNumber = "0784000292",
                Withdrawals = new List<Withdrawal> { new Withdrawal() }
            };

            var results = this.LibrarianService.Create(librarian);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
