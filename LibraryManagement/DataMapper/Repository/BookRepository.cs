// <copyright file="BookRepository.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> BookRepository class. </summary>
namespace DataMapper.Repository
{
    using DataMapper.Repository.DataBaseContext;
    using DataMapper.Repository.RepositoryBase;
    using DomainModel.Model;

    /// <summary>
    /// BookRepository class.
    /// </summary>
    /// <seealso cref="DataMapper.Repository.RepositoryBase.RepositoryBase{DomainModel.Model.Book}" />
    public class BookRepository : RepositoryBase<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public BookRepository(LibraryManagementContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
