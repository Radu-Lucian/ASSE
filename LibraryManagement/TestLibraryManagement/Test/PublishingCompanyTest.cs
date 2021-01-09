// <copyright file="PublishingCompanyTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the PublishingCompany Test class. </summary>
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
    /// Class PublishingCompanyTest.
    /// </summary>
    public class PublishingCompanyTest
    {
        /// <summary>
        /// Gets or sets the publishing company service.
        /// </summary>
        /// <value>The publishing company service.</value>
        private PublishingCompanyService PublishingCompanyService { get; set; }

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
            var mockSet = new Mock<DbSet<PublishingCompany>>();
            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<PublishingCompany>()).Returns(mockSet.Object);
            this.PublishingCompanyService = new PublishingCompanyService(new PublishingCompanyRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddNullPublishingCompany.
        /// </summary>
        [Test]
        public void TestAddNullPublishingCompany()
        {
            PublishingCompany nullPublishingCompany = null;

            Assert.Throws<ArgumentNullException>(() => this.PublishingCompanyService.Create(nullPublishingCompany));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullName.
        /// </summary>
        [Test]
        public void TestAddNullName()
        {
            var publishingCompany = new PublishingCompany
            {
                Name = null
            };

            var results = this.PublishingCompanyService.Create(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyNameNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidNameLowerBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidNameLowerBoundary()
        {
            var publishingCompany = new PublishingCompany
            {
                Name = string.Empty
            };

            var results = this.PublishingCompanyService.Create(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddInvalidNameUpperBoundary.
        /// </summary>
        [Test]
        public void TestAddInvalidNameUpperBoundary()
        {
            var publishingCompany = new PublishingCompany
            {
                Name = new string('a', 210)
            };

            var results = this.PublishingCompanyService.Create(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyNameLength");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidLengthName.
        /// </summary>
        [Test]
        public void TestAddValidLengthName()
        {
            var publishingCompany = new PublishingCompany
            {
                Name = "RAO"
            };

            var results = this.PublishingCompanyService.Create(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyNameLength");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddNullPublications.
        /// </summary>
        [Test]
        public void TestAddNullPublications()
        {
            var publishingCompany = new PublishingCompany
            {
                Publications = null
            };

            var results = this.PublishingCompanyService.Create(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyPublicationsNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddEmptyPublications.
        /// </summary>
        [Test]
        public void TestAddEmptyPublications()
        {
            var publishingCompany = new PublishingCompany
            {
                Publications = new List<Publication>()
            };

            var results = this.PublishingCompanyService.Create(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyPublicationsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestAddValidPublications.
        /// </summary>
        [Test]
        public void TestAddValidPublications()
        {
            var publishingCompany = new PublishingCompany
            {
                Publications = new List<Publication> { new Publication { CoverType = Cover.HardCover } }
            };

            var results = this.PublishingCompanyService.Create(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyPublicationsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestPublishingCompanyGetterSetterId.
        /// </summary>
        [Test]
        public void TestPublishingCompanyGetterSetterId()
        {
            int id = 5;
            var publishingCompany = new PublishingCompany
            {
                Name = "RAO",
                Publications = new List<Publication> { new Publication { CoverType = Cover.HardCover } }
            };

            typeof(PublishingCompany).GetProperty(nameof(PublishingCompany.Id)).SetValue(publishingCompany, id);

            Assert.IsTrue(id == publishingCompany.Id);
        }

        /// <summary>
        /// Defines the test method TestAddValidPublishingCompany.
        /// </summary>
        [Test]
        public void TestAddValidPublishingCompany()
        {
            var publishingCompany = new PublishingCompany
            {
                Name = "RAO",
                Publications = new List<Publication> { new Publication { CoverType = Cover.HardCover } }
            };

            var results = this.PublishingCompanyService.Create(publishingCompany);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
