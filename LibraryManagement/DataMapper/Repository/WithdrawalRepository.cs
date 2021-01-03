// <copyright file="WithdrawalRepository.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> WithdrawalRepository class. </summary>
namespace DataMapper.Repository
{
    using DataMapper.Repository.DataBaseContext;
    using DataMapper.Repository.RepositoryBase;
    using DomainModel.Model;

    /// <summary>
    /// WithdrawalRepository class.
    /// </summary>
    /// <seealso cref="DataMapper.Repository.RepositoryBase.RepositoryBase{DomainModel.Model.Withdrawal}" />
    public class WithdrawalRepository : RepositoryBase<Withdrawal>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithdrawalRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public WithdrawalRepository(LibraryManagementContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
