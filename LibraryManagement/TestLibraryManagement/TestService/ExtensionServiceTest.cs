// <copyright file="ExtensionServiceTest.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Extension Service Test class. </summary>
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
    /// Defines test class ExtensionServiceTest.
    /// </summary>
    [TestFixture]
    public class ExtensionServiceTest
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
        /// Gets or sets the extension list.
        /// </summary>
        /// <value>The extension list.</value>
        private List<Extension> ExtensionList { get; set; }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.ExtensionList =
                new List<Extension>
           {
                    new Extension
                    {
                        ExtraDays = 5,
                        CreationDate = DateTime.Today.AddDays(-5),
                        Withdrawal = new Withdrawal { }
                    },
                    new Extension
                    {
                        ExtraDays = 10,
                        CreationDate = DateTime.Today.AddDays(-2),
                        Withdrawal = new Withdrawal { }
                    },
            };
            var queryable = this.ExtensionList.AsQueryable();

            var mockSet = new Mock<DbSet<Extension>>();
            mockSet.As<IQueryable<Extension>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<Extension>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<Extension>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<Extension>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<Extension>())).Callback<Extension>((entity) => this.ExtensionList.Add(entity));
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => this.ExtensionList.FirstOrDefault(d => d.Id == (int)ids[0]));
            mockSet.Setup(m => m.Remove(It.IsAny<Extension>())).Callback<Extension>((entity) => this.ExtensionList.Remove(entity));
            mockSet.Setup(m => m.AsNoTracking()).Returns(mockSet.Object);

            this.LibraryContextMock = new Mock<LibraryManagementContext>();
            this.LibraryContextMock.Setup(m => m.Set<Extension>()).Returns(mockSet.Object);
            this.ExtensionService = new ExtensionService(new ExtensionRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Defines the test method TestAddExtension.
        /// </summary>
        [Test]
        public void TestAddExtensionWithValidData()
        {
            var extension = new Extension
            {
                ExtraDays = 8,
                CreationDate = DateTime.Today.AddDays(-3),
                Withdrawal = new Withdrawal { }
            };

            var results = this.ExtensionService.Create(extension);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestAddExtensionWithInvalidData.
        /// </summary>
        [Test]
        public void TestAddExtensionWithInvalidData()
        {
            var extension = new Extension();

            var results = this.ExtensionService.Create(extension);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteExtensionWithValidData.
        /// </summary>
        [Test]
        public void TestDeleteExtensionWithValidData()
        {
            var extension = this.ExtensionList.ElementAt(0);

            var results = this.ExtensionService.Delete(extension);

            Assert.IsEmpty(results);
            Assert.IsTrue(this.ExtensionList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteExtensionWithInvalidData.
        /// </summary>
        [Test]
        public void TestDeleteExtensionWithInvalidData()
        {
            var extension = new Extension();

            var results = this.ExtensionService.Delete(extension);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteExtensionWithNullData.
        /// </summary>
        [Test]
        public void TestDeleteExtensionWithNullData()
        {
            Extension nullExtension = null;

            Assert.Throws<ArgumentNullException>(() => this.ExtensionService.Delete(nullExtension));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestDeleteExistingExtensionWithId.
        /// </summary>
        [Test]
        public void TestDeleteExistingExtensionWithId()
        {
            this.ExtensionService.Delete(0);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
            Assert.IsTrue(this.ExtensionList.Count == 1);
        }

        /// <summary>
        /// Defines the test method TestDeleteInvalidExtensionWithId.
        /// </summary>
        [Test]
        public void TestDeleteInvalidExtensionWithId()
        {
            this.ExtensionService.Delete(55);

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateExtensionWithValidData.
        /// </summary>
        [Test]
        public void TestUpdateExtensionWithValidData()
        {
            var extension = this.ExtensionList.ElementAt(0);

            extension.ExtraDays = 15;

            var results = this.ExtensionService.Update(extension);

            Assert.IsEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Defines the test method TestUpdateExtensionWithInvalidData.
        /// </summary>
        [Test]
        public void TestUpdateExtensionWithInvalidData()
        {
            var extension = this.ExtensionList.ElementAt(0);

            extension.ExtraDays = 0;

            var results = this.ExtensionService.Update(extension);

            Assert.IsNotEmpty(results);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestUpdateExtensionWithNullData.
        /// </summary>
        [Test]
        public void TestUpdateExtensionWithNullData()
        {
            Extension nullExtension = null;

            Assert.Throws<ArgumentNullException>(() => this.ExtensionService.Update(nullExtension));
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Defines the test method TestFindValidExtensionWithId.
        /// </summary>
        [Test]
        public void TestFindValidExtensionWithId()
        {
            var extension = this.ExtensionService.Find(0);

            Assert.IsNotNull(extension);
        }

        /// <summary>
        /// Defines the test method TestFindInvalidExtensionWithId.
        /// </summary>
        [Test]
        public void TestFindInvalidExtensionWithId()
        {
            var extension = this.ExtensionService.Find(55);

            Assert.IsNull(extension);
        }

        /// <summary>
        /// Defines the test method TestFindAllExtensions.
        /// </summary>
        [Test]
        public void TestFindAllExtensions()
        {
            var extensions = this.ExtensionService.FindAll();

            Assert.IsTrue(extensions.Count() == this.ExtensionList.Count);
        }
    }
}
