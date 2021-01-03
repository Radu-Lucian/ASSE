// <copyright file="ExtensionRepository.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> ExtensionRepository class. </summary>
namespace DataMapper.Repository
{
    using DataMapper.Repository.DataBaseContext;
    using DataMapper.Repository.RepositoryBase;
    using DomainModel.Model;

    /// <summary>
    /// ExtensionRepository class.
    /// </summary>
    /// <seealso cref="DataMapper.Repository.RepositoryBase.RepositoryBase{DomainModel.Model.Extension}" />
    public class ExtensionRepository : RepositoryBase<Extension>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public ExtensionRepository(LibraryManagementContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
