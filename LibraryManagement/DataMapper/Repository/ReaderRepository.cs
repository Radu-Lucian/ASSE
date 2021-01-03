// <copyright file="ReaderRepository.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> ReaderRepository class. </summary>
namespace DataMapper.Repository
{
    using DataMapper.Repository.DataBaseContext;
    using DataMapper.Repository.RepositoryBase;
    using DomainModel.Model;

    /// <summary>
    /// ReaderRepository class.
    /// </summary>
    /// <seealso cref="DataMapper.Repository.RepositoryBase.RepositoryBase{DomainModel.Model.Reader}" />
    public class ReaderRepository : RepositoryBase<Reader>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public ReaderRepository(LibraryManagementContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
