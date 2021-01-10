// <copyright file="ExtensionService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Extension class. </summary>
namespace ServiceLayer.Service
{
    using DataMapper.Repository;
    using DomainModel.Model;
    using ServiceLayer.Service.Base;

    /// <summary>
    /// Class ExtensionService.
    /// Implements the <see cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Extension}" />
    /// </summary>
    /// <seealso cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Extension}" />
    public class ExtensionService : BaseService<Extension>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionService"/> class.
        /// </summary>
        /// <param name="extensionRepository">The extension repository.</param>
        public ExtensionService(ExtensionRepository extensionRepository) :
            base(extensionRepository)
        {
        }
    }
}
