// <copyright file="ExtensionService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Extension class. </summary>
namespace ServiceLayer.Service
{
    using System.Reflection;
    using DataMapper.Logger;
    using DataMapper.Repository;
    using DomainModel.Model;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// Class ExtensionService.
    /// </summary>
    public class ExtensionService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionService"/> class.
        /// </summary>
        /// <param name="extensionRepository">The extension repository.</param>
        public ExtensionService(ExtensionRepository extensionRepository)
        {
            this.ExtensionRepository = extensionRepository;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the extension repository.
        /// </summary>
        /// <value>The extension repository.</value>
        private ExtensionRepository ExtensionRepository { get; set; }

        /// <summary>
        /// Creates the extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns>ValidationResults based on domain entity.</returns>
        /// <exception cref="System.ArgumentNullException">Null extension.</exception>
        public ValidationResults CreateExtension(Extension extension)
        {
            if (extension is null)
            {
                Logger.LogError("Extension is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(extension));
            }

            var extensionValidator = ValidationFactory.CreateValidator<Extension>();
            ValidationResults valResults = extensionValidator.Validate(extension);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.ExtensionRepository.Create(extension);
            return valResults;
        }
    }
}
