// <copyright file="DomainService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Domain Service class. </summary>
namespace ServiceLayer.Service
{
    using DataMapper.Repository;
    using DomainModel.Model;
    using ServiceLayer.Service.Base;

    /// <summary>
    /// Class DomainService.
    /// Implements the <see cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Domain}" />
    /// </summary>
    /// <seealso cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Domain}" />
    public class DomainService : BaseService<Domain>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainService" /> class.
        /// </summary>
        /// <param name="domainRepository">The domain repository.</param>
        public DomainService(DomainRepository domainRepository) :
            base(domainRepository)
        {
        }
    }
}
