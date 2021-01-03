// <copyright file="LibrarianRepository.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> LibrarianRepository class. </summary>
namespace DataMapper.Repository
{
    using DataMapper.Repository.DataBaseContext;
    using DataMapper.Repository.RepositoryBase;
    using DomainModel.Model;

    /// <summary>
    /// LibrarianRepository class.
    /// </summary>
    /// <seealso cref="DataMapper.Repository.RepositoryBase.RepositoryBase{DomainModel.Model.Librarian}" />
    public class LibrarianRepository : RepositoryBase<Librarian>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibrarianRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public LibrarianRepository(LibraryManagementContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
