// <copyright file="LibrarianService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Librarian class. </summary>
namespace ServiceLayer.Service
{
    using System.Reflection;
    using DataMapper.Logger;
    using DataMapper.Repository;
    using DomainModel.Model;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// Class LibrarianService.
    /// </summary>
    public class LibrarianService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibrarianService"/> class.
        /// </summary>
        /// <param name="extensionRepository">The extension repository.</param>
        public LibrarianService(LibrarianRepository extensionRepository)
        {
            this.LibrarianRepository = extensionRepository;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the librarian repository.
        /// </summary>
        /// <value>The librarian repository.</value>
        private LibrarianRepository LibrarianRepository { get; set; }

        /// <summary>
        /// Creates the librarian.
        /// </summary>
        /// <param name="librarian">The librarian.</param>
        /// <returns>ValidationResults based on librarian entity.</returns>
        /// <exception cref="System.ArgumentNullException">Null librarian.</exception>
        public ValidationResults CreateLibrarian(Librarian librarian)
        {
            if (librarian is null)
            {
                Logger.LogError("Librarian is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(librarian));
            }

            var extensionValidator = ValidationFactory.CreateValidator<Librarian>();
            ValidationResults valResults = extensionValidator.Validate(librarian);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.LibrarianRepository.Create(librarian);
            return valResults;
        }
    }
}
