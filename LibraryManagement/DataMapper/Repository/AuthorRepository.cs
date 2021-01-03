// <copyright file="AuthorRepository.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> AuthorRepository class. </summary>
namespace DataMapper.Repository
{
    using DataMapper.Repository.DataBaseContext;
    using DataMapper.Repository.RepositoryBase;
    using DomainModel.Model;

    /// <summary>
    /// AuthorRepository class.
    /// </summary>
    /// <seealso cref="DataMapper.Repository.RepositoryBase.RepositoryBase{DomainModel.Model.Author}" />
    public class AuthorRepository : RepositoryBase<Author>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public AuthorRepository(LibraryManagementContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
