// <copyright file="ReaderTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Reader Test class. </summary>
namespace TestLibraryManagement
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

    [TestFixture]
    public class ReaderTest
    {
        private ReaderService ReaderService { get; set; }

        private Mock<LibraryManagementContext> LibraryContextMock { get; set; }

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

        [Test]
        public void TestAddNullReader()
        {
            Reader nullReader = null;

            Assert.Throws<ArgumentNullException>(() => this.ReaderService.CreateReader(nullReader));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullFirstName()
        {
            var reader = new Reader
            {
                FirstName = null
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderFirstNameNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidLenghtFirstNameLowerBoundary()
        {
            var reader = new Reader
            {
                FirstName = string.Empty
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderFirstNameLenght");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidLenghtFirstNameUpperBoundary()
        {
            var reader = new Reader
            {
                FirstName = new string('a', 210)
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderFirstNameLenght");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddValidLenghtFirstName()
        {
            var reader = new Reader
            {
                FirstName = "Luci"
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderFirstNameLenght");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullLastName()
        {
            var reader = new Reader
            {
                LastName = null
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderLastNameLenght");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidLenghtLastNameLowerBoundary()
        {
            var reader = new Reader
            {
                LastName = string.Empty
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderLastNameLenght");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidLenghtLastNameUpperBoundary()
        {
            var reader = new Reader
            {
                LastName = new string('a', 210)
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderLastNameLenght");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddValidLenghtLastName()
        {
            var reader = new Reader
            {
                LastName = "Radu"
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderLastNameLenght");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullAddress()
        {
            var reader = new Reader
            {
                Address = null
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderAddressNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidLenghtAddressLowerBoundary()
        {
            var reader = new Reader
            {
                Address = string.Empty
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderAddressLenght");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidLenghtAddressUpperBoundary()
        {
            var reader = new Reader
            {
                Address = new string('a', 210)
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderAddressLenght");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddValidLenghtAddress()
        {
            var reader = new Reader
            {
                Address = "Brasov, Com.Sanpetru, Str.Meschendorfer, 319"
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderAddressLenght");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullPhoneNumber()
        {
            var reader = new Reader
            {
                PhoneNumber = null
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderPhoneNumberInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddEmptyPhoneNumber()
        {
            var reader = new Reader
            {
                PhoneNumber = ""
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderPhoneNumberInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [TestCase("0413564864")]
        [TestCase("0790512346")]
        [TestCase("867-5309")]
        public void TestAddInvalidPhoneNumber(string invalidPhoneNumber)
        {
            var reader = new Reader
            {
                PhoneNumber = invalidPhoneNumber
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderPhoneNumberInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

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

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderPhoneNumberInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullEmailAddress()
        {
            var reader = new Reader
            {
                EmailAddress = null
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderEmailAddressNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddEmptyEmailAddress()
        {
            var reader = new Reader
            {
                EmailAddress = ""
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderEmailAddressInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidEmailAddress()
        {
            var reader = new Reader
            {
                EmailAddress = "not a email address"
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderEmailAddressInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddValidEmailAddress()
        {
            var reader = new Reader
            {
                EmailAddress = "radulucian.andrei@gmail.com"
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderEmailAddressInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

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

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateReaderPhoneEmail");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

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

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateReaderPhoneEmail");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

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

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ValidateReaderPhoneEmail");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        [Test]
        public void TestAddNullGender()
        {
            var reader = new Reader
            {
                Gender = null
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderGenderNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidGender()
        {
            var reader = new Reader
            {
                Gender = "apache helicopter"
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderGenderInvalid");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

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

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderGenderInvalid");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullWithdrawals()
        {
            var reader = new Reader
            {
                Withdrawals = null
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderWithdrawalsNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddEmptyWithdrawals()
        {
            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>()
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderWithdrawalsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddValidWithdrawals()
        {
            var reader = new Reader
            {
                Withdrawals = new List<Withdrawal>() { new Withdrawal() }
            };

            var results = this.ReaderService.CreateReader(reader);
            var tag = results.FirstOrDefault(res => res.Tag == "ReaderWithdrawalsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

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

            var results = this.ReaderService.CreateReader(reader);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}