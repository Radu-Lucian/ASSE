// <copyright file="PublishingCompanyService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the PublishingCompanyService class. </summary>
namespace ServiceLayer.Service
{
    using System.Reflection;
    using DataMapper.Logger;
    using DataMapper.Repository;
    using DomainModel.Model;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// Class PublishingCompanyService.
    /// </summary>
    public class PublishingCompanyService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublishingCompanyService"/> class.
        /// </summary>
        /// <param name="publishingCompanyRepository">The publishing company repository.</param>
        public PublishingCompanyService(PublishingCompanyRepository publishingCompanyRepository)
        {
            this.PublishingCompanyRepository = publishingCompanyRepository;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the publishing company repository.
        /// </summary>
        /// <value>The publishing company repository.</value>
        private PublishingCompanyRepository PublishingCompanyRepository { get; set; }

        /// <summary>
        /// Creates the publishing company.
        /// </summary>
        /// <param name="publishingCompany">The publishing company.</param>
        /// <returns>ValidationResults based on publication entity.</returns>
        /// <exception cref="System.ArgumentNullException">Null publishingCompany</exception>
        public ValidationResults CreatePublishingCompany(PublishingCompany publishingCompany)
        {
            if (publishingCompany is null)
            {
                Logger.LogError("Publication is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(publishingCompany));
            }

            var publishingCompanyValidator = ValidationFactory.CreateValidator<PublishingCompany>();
            ValidationResults valResults = publishingCompanyValidator.Validate(publishingCompany);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.PublishingCompanyRepository.Create(publishingCompany);
            return valResults;
        }
    }
}
