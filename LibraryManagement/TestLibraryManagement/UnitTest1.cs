using DataMapper.Repository;
using DomainModel.Model;
using NUnit.Framework;
using ServiceLayer.Service;
using System.Collections.Generic;

namespace TestLibraryManagement
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            BookService bookService = new BookService(new BookRepository(new DataMapper.Repository.DataBaseContext.LibraryManagementContext()));

            Author autor = new Author { FirstName = "Yes", LastName = "UI" };
            Domain field1 = new Domain { Name = "Art" };
            Domain field2 = new Domain { Name = "Science" };
            Domain field3 = new Domain { Name = "Philosophy" };

            Book book = new Book
            {
                Domains = new List<Domain>
                {
                    field1, field2, field3
                },
                Authors = new List<Author> { autor },
                Publications = new List<Publication> { }
            };

            bookService.CreateBook(book);
        }
    }
}