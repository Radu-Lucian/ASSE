// <copyright file="PublishingCompanyService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the PublishingCompanyService class. </summary>
namespace ServiceLayer.Service
{
    using DataMapper.Repository;
    using DomainModel.Model;
    using ServiceLayer.Service.Base;

    /// <summary>
    /// Class PublishingCompanyService.
    /// Implements the <see cref="ServiceLayer.Service.Base.BaseService{DataMapper.Repository.PublishingCompanyRepository, DomainModel.Model.PublishingCompany}" />
    /// </summary>
    /// <seealso cref="ServiceLayer.Service.Base.BaseService{DataMapper.Repository.PublishingCompanyRepository, DomainModel.Model.PublishingCompany}" />
    public class PublishingCompanyService : BaseService<PublishingCompanyRepository, PublishingCompany>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublishingCompanyService"/> class.
        /// </summary>
        /// <param name="publishingCompanyRepository">The publishing company repository.</param>
        public PublishingCompanyService(PublishingCompanyRepository publishingCompanyRepository) :
            base(publishingCompanyRepository)
        {
        }
    }
}
