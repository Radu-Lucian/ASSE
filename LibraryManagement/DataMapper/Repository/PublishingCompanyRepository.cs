// <copyright file="PublishingCompanyRepository.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> PublishingCompanyRepository class. </summary>
namespace DataMapper.Repository
{
    using DataMapper.Repository.DataBaseContext;
    using DataMapper.Repository.RepositoryBase;
    using DomainModel.Model;

    /// <summary>
    /// PublishingCompanyRepository class.
    /// </summary>
    /// <seealso cref="DataMapper.Repository.RepositoryBase.RepositoryBase{DomainModel.Model.PublishingCompany}" />
    public class PublishingCompanyRepository : RepositoryBase<PublishingCompany>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublishingCompanyRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public PublishingCompanyRepository(LibraryManagementContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
