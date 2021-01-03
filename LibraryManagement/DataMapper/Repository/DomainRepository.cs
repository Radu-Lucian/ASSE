// <copyright file="DomainRepository.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> DomainRepository class. </summary>
namespace DataMapper.Repository
{
    using DataMapper.Repository.DataBaseContext;
    using DataMapper.Repository.RepositoryBase;
    using DomainModel.Model;

    /// <summary>
    /// DomainRepository class.
    /// </summary>
    /// <seealso cref="DataMapper.Repository.RepositoryBase.RepositoryBase{DomainModel.Model.Domain}" />
    public class DomainRepository : RepositoryBase<Domain>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public DomainRepository(LibraryManagementContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
