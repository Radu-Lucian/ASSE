// <copyright file="DomainService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Domain Service class. </summary>
namespace ServiceLayer.Service
{
    using System.Reflection;
    using DataMapper.Logger;
    using DataMapper.Repository;
    using DomainModel.Model;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// Class DomainService.
    /// </summary>
    public class DomainService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainService" /> class.
        /// </summary>
        /// <param name="domainRepository">The domain repository.</param>
        public DomainService(DomainRepository domainRepository)
        {
            this.DomainRepository = domainRepository;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the domain repository.
        /// </summary>
        /// <value>The domain repository.</value>
        private DomainRepository DomainRepository { get; set; }

        /// <summary>
        /// Creates the domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>ValidationResults based on domain entity.</returns>
        /// <exception cref="System.ArgumentNullException">Null domain.</exception>
        public ValidationResults CreateDomain(Domain domain)
        {
            if (domain is null)
            {
                Logger.LogError("Domain is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(domain));
            }

            var domainValidator = ValidationFactory.CreateValidator<Domain>();
            ValidationResults valResults = domainValidator.Validate(domain);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.DomainRepository.Create(domain);
            return valResults;
        }
    }
}
