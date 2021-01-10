// <copyright file="WithdrawalService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Withdrawal service. </summary>
namespace ServiceLayer.Service
{
    using DataMapper.Repository;
    using DomainModel.Model;
    using ServiceLayer.Service.Base;

    /// <summary>
    /// Class WithdrawalService.
    /// Implements the <see cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Withdrawal}" />
    /// </summary>
    /// <seealso cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Withdrawal}" />
    public class WithdrawalService : BaseService<Withdrawal>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithdrawalService"/> class.
        /// </summary>
        /// <param name="withdrawalRepository">The withdrawal repository.</param>
        public WithdrawalService(WithdrawalRepository withdrawalRepository) :
            base(withdrawalRepository)
        {
        }
    }
}
