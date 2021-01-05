// <copyright file="PublishingCompany.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the PublishingCompany Test class. </summary>
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

    public class PublishingCompanyTest
    {
        private PublishingCompanyService PublishingCompanyService { get; set; }

        private Mock<LibraryManagementContext> LibraryContextMock { get; set; }

        [SetUp]
        public void Setup()
        {
            var mockSet = new Mock<DbSet<PublishingCompany>>();
            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<PublishingCompany>()).Returns(mockSet.Object);
            this.PublishingCompanyService = new PublishingCompanyService(new PublishingCompanyRepository(this.LibraryContextMock.Object));
        }

        [Test]
        public void TestAddNullPublishingCompany()
        {
            PublishingCompany nullPublishingCompany = null;

            Assert.Throws<ArgumentNullException>(() => this.PublishingCompanyService.CreatePublishingCompany(nullPublishingCompany));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullName()
        {
            var publishingCompany = new PublishingCompany
            {
                Name = null
            };

            var results = this.PublishingCompanyService.CreatePublishingCompany(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyNameNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidNameLowerBoundary()
        {
            var publishingCompany = new PublishingCompany
            {
                Name = string.Empty
            };

            var results = this.PublishingCompanyService.CreatePublishingCompany(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyNameLenght");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidNameUpperBoundary()
        {
            var publishingCompany = new PublishingCompany
            {
                Name = new string('a', 210)
            };

            var results = this.PublishingCompanyService.CreatePublishingCompany(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyNameLenght");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddValidLenghtName()
        {
            var publishingCompany = new PublishingCompany
            {
                Name = "RAO"
            };

            var results = this.PublishingCompanyService.CreatePublishingCompany(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyNameLenght");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullPublications()
        {
            var publishingCompany = new PublishingCompany
            {
                Publications = null
            };

            var results = this.PublishingCompanyService.CreatePublishingCompany(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyPublicationsNull");

            Assert.IsNotNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddEmptyPublications()
        {
            var publishingCompany = new PublishingCompany
            {
                Publications = new List<Publication>()
            };

            var results = this.PublishingCompanyService.CreatePublishingCompany(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyPublicationsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddValidPublications()
        {
            var publishingCompany = new PublishingCompany
            {
                Publications = new List<Publication> { new Publication { CoverType = Cover.HardCover } }
            };

            var results = this.PublishingCompanyService.CreatePublishingCompany(publishingCompany);
            var tag = results.FirstOrDefault(res => res.Tag == "PublishingCompanyPublicationsNull");

            Assert.IsNull(tag);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddValidPublishingCompany()
        {
            var publishingCompany = new PublishingCompany
            {
                Name = "RAO",
                Publications = new List<Publication> { new Publication { CoverType = Cover.HardCover } }
            };

            var results = this.PublishingCompanyService.CreatePublishingCompany(publishingCompany);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
