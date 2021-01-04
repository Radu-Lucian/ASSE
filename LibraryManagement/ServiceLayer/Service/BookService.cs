// <copyright file="BookService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Book service. </summary>
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
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        public BookService(BookRepository bookRepository)
        {
            this.BookRepository = bookRepository;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the book repository.
        /// </summary>
        /// <value>
        /// The book repository.
        /// </value>
        private BookRepository BookRepository { get; set; }

        /// <summary>
        /// Creates the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>ValidationResults based on book entity.</returns>
        /// <exception cref="System.ArgumentNullException">Null book.</exception>
        public ValidationResults CreateBook(Book book)
        {
            if (book is null)
            {
                Logger.LogError("Book is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(book));
            }

            var bookValidator = ValidationFactory.CreateValidator<Book>();
            ValidationResults valResults = bookValidator.Validate(book);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.BookRepository.Create(book);
            return valResults;
        }
    }
}
