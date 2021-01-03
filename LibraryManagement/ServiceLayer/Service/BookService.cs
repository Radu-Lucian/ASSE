// <copyright file="Author.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the DB Author entity. </summary>
namespace ServiceLayer.Service
{
    using System.Reflection;
    using DataMapper.Logger;
    using DataMapper.Repository;
    using DomainModel.Model;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// BookService class.
    /// </summary>
    public class BookService
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public Logger Logger { get; private set; }

        /// <summary>
        /// Gets or sets the book repository.
        /// </summary>
        /// <value>
        /// The book repository.
        /// </value>
        private BookRepository BookRepository { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        public BookService(BookRepository bookRepository)
        {
            this.BookRepository = bookRepository;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Creates the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns></returns>
        public bool CreateBook(Book book)
        {
            if (book == null)
            {
                Logger.LogError("Book is null", MethodBase.GetCurrentMethod());
                return false;
            }

            var pValidator = ValidationFactory.CreateValidator<Book>();
            ValidationResults valResults = pValidator.Validate(book);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return false;
            }

            this.BookRepository.Create(book);
            return true;
        }
    }
}
