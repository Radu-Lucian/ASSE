// <copyright file="ReaderService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the ReaderService service. </summary>
namespace ServiceLayer.Service
{
    using System.Reflection;
    using DataMapper.Logger;
    using DataMapper.Repository;
    using DomainModel.Model;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// Class ReaderService.
    /// </summary>
    public class ReaderService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderService"/> class.
        /// </summary>
        /// <param name="readerRepository">The reader repository.</param>
        public ReaderService(ReaderRepository readerRepository)
        {
            this.ReaderRepository = readerRepository;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the reader repository.
        /// </summary>
        /// <value>The reader repository.</value>
        private ReaderRepository ReaderRepository { get; set; }

        /// <summary>
        /// Creates the reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>ValidationResults based on reader entity.</returns>
        /// <exception cref="System.ArgumentNullException">Null reader</exception>
        public ValidationResults CreateReader(Reader reader)
        {
            if (reader is null)
            {
                Logger.LogError("Reader is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(reader));
            }

            var authorValidator = ValidationFactory.CreateValidator<Reader>();
            ValidationResults valResults = authorValidator.Validate(reader);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.ReaderRepository.Create(reader);
            return valResults;
        }
    }
}
