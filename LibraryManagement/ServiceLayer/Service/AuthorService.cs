// <copyright file="AuthorService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Author service. </summary>
namespace ServiceLayer.Service
{
    using System.Reflection;
    using DataMapper.Logger;
    using DataMapper.Repository;
    using DomainModel.Model;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// Class AuthorService.
    /// </summary>
    public class AuthorService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorService"/> class.
        /// </summary>
        /// <param name="authorRepository">The author repository.</param>
        public AuthorService(AuthorRepository authorRepository)
        {
            this.AuthorRepository = authorRepository;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the author repository.
        /// </summary>
        /// <value>The author repository.</value>
        private AuthorRepository AuthorRepository { get; set; }

        /// <summary>
        /// Creates the author.
        /// </summary>
        /// <param name="author">The author.</param>
        /// <returns>ValidationResults based on author entity.</returns>
        /// <exception cref="System.ArgumentNullException">Null author</exception>
        public ValidationResults CreateAuthor(Author author)
        {
            if (author is null)
            {
                Logger.LogError("Author is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(author));
            }

            var authorValidator = ValidationFactory.CreateValidator<Author>();
            ValidationResults valResults = authorValidator.Validate(author);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.AuthorRepository.Create(author);
            return valResults;
        }
    }
}
