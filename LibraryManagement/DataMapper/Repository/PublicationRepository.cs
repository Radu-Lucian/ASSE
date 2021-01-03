// <copyright file="PublicationRepository.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> PublicationRepository class. </summary>
namespace DataMapper.Repository
{
    using DataMapper.Repository.DataBaseContext;
    using DataMapper.Repository.RepositoryBase;
    using DomainModel.Model;

    /// <summary>
    /// PublicationRepository class.
    /// </summary>
    /// <seealso cref="DataMapper.Repository.RepositoryBase.RepositoryBase{DomainModel.Model.Publication}" />
    public class PublicationRepository : RepositoryBase<Publication>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public PublicationRepository(LibraryManagementContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
