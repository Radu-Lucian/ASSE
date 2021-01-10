// <copyright file="BookService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Book service. </summary>
namespace ServiceLayer.Service
{
    using DataMapper.Repository;
    using DomainModel.Model;
    using ServiceLayer.Service.Base;

    /// <summary>
    /// Class BookService.
    /// Implements the <see cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Book}" />
    /// </summary>
    /// <seealso cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Book}" />
    public class BookService : BaseService<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        public BookService(BookRepository bookRepository) :
            base(bookRepository)
        {
        }
    }
}
