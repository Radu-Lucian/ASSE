// <copyright file="PublicationService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Publication class. </summary>
namespace ServiceLayer.Service
{
    using System.Reflection;
    using DataMapper.Logger;
    using DataMapper.Repository;
    using DomainModel.Model;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// Class PublicationService.
    /// </summary>
    public class PublicationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationService"/> class.
        /// </summary>
        /// <param name="publicationRepository">The publication repository.</param>
        public PublicationService(PublicationRepository publicationRepository)
        {
            this.PublicationRepository = publicationRepository;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the publication repository.
        /// </summary>
        /// <value>The publication repository.</value>
        private PublicationRepository PublicationRepository { get; set; }

        /// <summary>
        /// Creates the publication.
        /// </summary>
        /// <param name="publication">The publication.</param>
        /// <returns>ValidationResults based on publication entity.</returns>
        /// <exception cref="System.ArgumentNullException">Null publication.</exception>
        public ValidationResults CreatePublication(Publication publication)
        {
            if (publication is null)
            {
                Logger.LogError("Publication is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(publication));
            }

            var extensionValidator = ValidationFactory.CreateValidator<Publication>();
            ValidationResults valResults = extensionValidator.Validate(publication);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.PublicationRepository.Create(publication);
            return valResults;
        }
    }
}
