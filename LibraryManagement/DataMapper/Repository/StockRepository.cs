// <copyright file="StockRepository.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> StockRepository class. </summary>
namespace DataMapper.Repository
{
    using DataMapper.Repository.DataBaseContext;
    using DataMapper.Repository.RepositoryBase;
    using DomainModel.Model;

    /// <summary>
    /// StockRepository class.
    /// </summary>
    /// <seealso cref="DataMapper.Repository.RepositoryBase.RepositoryBase{DomainModel.Model.Stock}" />
    public class StockRepository : RepositoryBase<Stock>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public StockRepository(LibraryManagementContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
