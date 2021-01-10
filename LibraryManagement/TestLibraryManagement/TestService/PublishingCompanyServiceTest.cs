// <copyright file="PublishingCompanyServiceTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the PublishingCompany Service Test class. </summary>
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
    /// Defines test class PublishingCompanyServiceTest.
    /// </summary>
    [TestFixture]
    public class PublishingCompanyServiceTest
    {
        /// <summary>
        /// Gets or sets the publishingCompany service.
        /// </summary>
        /// <value>The publishingCompany service.</value>
        private PublishingCompanyService PublishingCompanyService { get; set; }

        /// <summary>
        /// Gets or sets the library context mock.
        /// </summary>
        /// <value>The library context mock.</value>
        private Mock<LibraryManagementContext> LibraryContextMock { get; set; }

        /// <summary>
        /// Gets or sets the publishingCompany list.
        /// </summary>
        /// <value>The publishingCompany list.</value>
        private List<PublishingCompany> PublishingCompanyList { get; set; }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.PublishingCompanyList =
                new List<PublishingCompany>
           {
                    new PublishingCompany
                    {
                        Name = "RAO",
                        Publications = new List<Publication> { new Publication { CoverType = Cover.HardCover } }
                    },
                    new PublishingCompany
                    {
                        Name = "Litera",
                        Publications = new List<Publication> { new Publication { CoverType = Cover.PaperBack } }
                    },
            };

            typeof(PublishingCompany).GetProperty(nameof(PublishingCompany.Id)).SetValue(this.PublishingCompanyList[0], 0);
            typeof(PublishingCompany).GetProperty(nameof(PublishingCompany.Id)).SetValue(this.PublishingCompanyList[1], 1);

            var queryable = this.PublishingCompanyList.AsQueryable();

            var mockSet = new Mock<DbSet<PublishingCompany>>();
            mockSet.As<IQueryable<PublishingCompany>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<PublishingCompany>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<PublishingCompany>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<PublishingCompany>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<PublishingCompany>())).Callback<PublishingCompany>((entity) => this.PublishingCompanyList.Add(entity));
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => this.PublishingCompanyList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<PublishingCompany>())).Callback<PublishingCompany>((entity) => this.PublishingCompanyList.Remove(entity));
            mockSet.Setup(m => m.AsNoTracking()).Returns(mockSet.Object);

            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<PublishingCompany>()).Returns(mockSet.Object);
            this.PublishingCompanyService = new PublishingCompanyService(new PublishingCompanyRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddPublishingCompany.
        /// </summary>
        [Test]
        public void TestAddPublishingCompanyWithValidData()
        {
            var publishingCompany = new PublishingCompany
            {
                Name = "Litera",
                Publications = new List<Publication> { new Publication { CoverType = Cover.PaperBack } }
            };

            var results = this.PublishingCompanyService.Create(publishingCompany);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddPublishingCompanyWithInvalidData.
        /// </summary>
        [Test]
        public void TestAddPublishingCompanyWithInvalidData()
        {
            var publishingCompany = new PublishingCompany();

            var results = this.PublishingCompanyService.Create(publishingCompany);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeletePublishingCompanyWithValidData.
        /// </summary>
        [Test]
        public void TestDeletePublishingCompanyWithValidData()
        {
            var publishingCompany = this.PublishingCompanyList.ElementAt(0);

            var results = this.PublishingCompanyService.Delete(publishingCompany);

            Assert.IsEmpty(results);
            Assert.IsTrue(this.PublishingCompanyList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeletePublishingCompanyWithInvalidData.
        /// </summary>
        [Test]
        public void TestDeletePublishingCompanyWithInvalidData()
        {
            var publishingCompany = new PublishingCompany();

            var results = this.PublishingCompanyService.Delete(publishingCompany);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeletePublishingCompanyWithNullData.
        /// </summary>
        [Test]
        public void TestDeletePublishingCompanyWithNullData()
        {
            PublishingCompany nullPublishingCompany = null;

            Assert.Throws<ArgumentNullException>(() => this.PublishingCompanyService.Delete(nullPublishingCompany));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteExistingPublishingCompanyWithId.
        /// </summary>
        [Test]
        public void TestDeleteExistingPublishingCompanyWithId()
        {
            this.PublishingCompanyService.Delete(0);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
            Assert.IsTrue(this.PublishingCompanyList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteInvalidPublishingCompanyWithId.
        /// </summary>
        [Test]
        public void TestDeleteInvalidPublishingCompanyWithId()
        {
            this.PublishingCompanyService.Delete(55);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdatePublishingCompanyWithValidData.
        /// </summary>
        [Test]
        public void TestUpdatePublishingCompanyWithValidData()
        {
            var publishingCompany = this.PublishingCompanyList.ElementAt(0);

            publishingCompany.Name = "Polirom";

            var results = this.PublishingCompanyService.Update(publishingCompany);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestUpdatePublishingCompanyWithInvalidData.
        /// </summary>
        [Test]
        public void TestUpdatePublishingCompanyWithInvalidData()
        {
            var publishingCompany = this.PublishingCompanyList.ElementAt(0);

            publishingCompany.Name = string.Empty;

            var results = this.PublishingCompanyService.Update(publishingCompany);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdatePublishingCompanyWithNullData.
        /// </summary>
        [Test]
        public void TestUpdatePublishingCompanyWithNullData()
        {
            PublishingCompany nullPublishingCompany = null;

            Assert.Throws<ArgumentNullException>(() => this.PublishingCompanyService.Update(nullPublishingCompany));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestFindValidPublishingCompanyWithId.
        /// </summary>
        [Test]
        public void TestFindValidPublishingCompanyWithId()
        {
            var publishingCompany = this.PublishingCompanyService.Find(0);

            Assert.IsNotNull(publishingCompany);
        }

        /// <summary>
        /// Defines the test method TestFindInvalidPublishingCompanyWithId.
        /// </summary>
        [Test]
        public void TestFindInvalidPublishingCompanyWithId()
        {
            var publishingCompany = this.PublishingCompanyService.Find(55);

            Assert.IsNull(publishingCompany);
        }

        /// <summary>
        /// Defines the test method TestFindAllPublishingCompanies.
        /// </summary>
        [Test]
        public void TestFindAllPublishingCompanys()
        {
            var publishingCompanys = this.PublishingCompanyService.FindAll();

            Assert.IsTrue(publishingCompanys.Count() == this.PublishingCompanyList.Count);
        }
    }
}
