﻿// <copyright file="ReaderTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Reader Test class. </summary>
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
    /// Defines test class ReaderTest.
    /// </summary>
    [TestFixture]
    public class ReaderTest
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
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var mockSet = new Mock<DbSet<Reader>>();
            var mockSetLib = new Mock<DbSet<Librarian>>();
            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Reader>()).Returns(() => mockSet.Object);
            this.LibraryContextMock.Setup(m => m.Set<Librarian>()).Returns(() => mockSetLib.Object);
            this.ReaderService = new ReaderService(new ReaderRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddNullReader.
        /// </summary>
        [Test]
        public void TestAddNullReader()
        {
            Reader nullReader = null;

            Assert.Throws<ArgumentNullException>(() => this.ReaderService.Create(nullReader));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullFirstName.
        /// </summary>
        [Test]
        public void TestAddNullFirstName()
        {
            var reader = new Reader
            {
                FirstName = null
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                FirstName = string.Empty
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                FirstName = new string('a', 210)
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                FirstName = "Luci"
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                LastName = null
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                LastName = string.Empty
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                LastName = new string('a', 210)
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                LastName = "Radu"
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                Address = null
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                Address = string.Empty
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                Address = new string('a', 210)
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319"
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                PhoneNumber = null
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                PhoneNumber = string.Empty
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                PhoneNumber = invalidPhoneNumber
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                PhoneNumber = validPhoneNumber
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                EmailAddress = null
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                EmailAddress = string.Empty
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                EmailAddress = "not an email address"
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                EmailAddress = "radulucian.andrei@gmail.com"
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                FirstName = "Lucian",
                LastName = "Radu",
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319",
                Gender = "M",
                Withdrawals = new List<Withdrawal> { new Withdrawal() }
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                FirstName = "Lucian",
                LastName = "Radu",
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319",
                Gender = "M",
                EmailAddress = "radulucian.andrei@gmail.com",
                Withdrawals = new List<Withdrawal> { new Withdrawal() }
            };

            var results = this.ReaderService.Create(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateReaderPhoneEmail");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddValidContactInformationWithPhoneNumber.
        /// </summary>
        [Test]
        public void TestAddValidContactInformationWithPhoneNumber()
        {
            var reader = new Reader
            {
                FirstName = "Lucian",
                LastName = "Radu",
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319",
                Gender = "M",
                PhoneNumber = "0784000292",
                Withdrawals = new List<Withdrawal> { new Withdrawal() }
            };

            var results = this.ReaderService.Create(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateReaderPhoneEmail");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddNullGender.
        /// </summary>
        [Test]
        public void TestAddNullGender()
        {
            var reader = new Reader
            {
                Gender = null
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                Gender = "apache helicopter"
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                Gender = validGender
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                Withdrawals = null
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
            };

            var results = this.ReaderService.Create(reader);
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
            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>() { new Withdrawal() }
            };

            var results = this.ReaderService.Create(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderWithdrawalsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestReaderGetterSetterId.
        /// </summary>
        [Test]
        public void TestReaderGetterSetterId()
        {
            int id = 5;
            var reader = new Reader
            {
                FirstName = "Lucian",
                LastName = "Radu",
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319",
                Gender = "M",
                EmailAddress = "radulucian.andrei@gmail.com",
                PhoneNumber = "0784000292",
                Withdrawals = new List<Withdrawal> { new Withdrawal() }
            };

            typeof(Reader).GetProperty(nameof(Reader.Id)).SetValue(reader, id);

            Assert.IsTrue(id == reader.Id);
        }

        /// <summary>
        /// Defines the test method TestAddValidReader.
        /// </summary>
        [Test]
        public void TestAddValidReader()
        {
            var reader = new Reader
            {
                FirstName = "Lucian",
                LastName = "Radu",
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319",
                Gender = "M",
                EmailAddress = "radulucian.andrei@gmail.com",
                PhoneNumber = "0784000292",
                Withdrawals = new List<Withdrawal> { new Withdrawal() }
            };

            var results = this.ReaderService.Create(reader);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}