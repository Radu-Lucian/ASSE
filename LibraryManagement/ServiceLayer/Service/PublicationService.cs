// <copyright file="PublicationService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Publication class. </summary>
namespace ServiceLayer.Service
{
    using DataMapper.Repository;
    using DomainModel.Model;
    using ServiceLayer.Service.Base;

    /// <summary>
    /// Class PublicationService.
    /// Implements the <see cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Publication}" />
    /// </summary>
    /// <seealso cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Publication}" />
    public class PublicationService : BaseService<Publication>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationService"/> class.
        /// </summary>
        /// <param name="publicationRepository">The publication repository.</param>
        public PublicationService(PublicationRepository publicationRepository) :
            base(publicationRepository)
        {
        }
    }
}
