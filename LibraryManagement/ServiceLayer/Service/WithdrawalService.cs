// <copyright file="WithdrawalService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Withdrawal service. </summary>
namespace ServiceLayer.Service
{
    using System.Reflection;
    using DataMapper.Logger;
    using DataMapper.Repository;
    using DomainModel.Model;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// Class WithdrawalService.
    /// </summary>
    public class WithdrawalService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithdrawalService"/> class.
        /// </summary>
        /// <param name="withdrawalRepository">The withdrawal repository.</param>
        public WithdrawalService(WithdrawalRepository withdrawalRepository)
        {
            this.WithdrawalRepository = withdrawalRepository;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the withdrawal repository.
        /// </summary>
        /// <value>The withdrawal repository.</value>
        private WithdrawalRepository WithdrawalRepository { get; set; }

        /// <summary>
        /// Creates the withdrawal.
        /// </summary>
        /// <param name="withdrawal">The withdrawal.</param>
        /// <returns>ValidationResults based on withdrawal entity.</returns>
        /// <exception cref="System.ArgumentNullException">Null withdrawal</exception>
        public ValidationResults CreateWithdrawal(Withdrawal withdrawal)
        {
            if (withdrawal is null)
            {
                Logger.LogError("Withdrawal is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(withdrawal));
            }

            var withdrawalValidator = ValidationFactory.CreateValidator<Withdrawal>();
            ValidationResults valResults = withdrawalValidator.Validate(withdrawal);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.WithdrawalRepository.Create(withdrawal);
            return valResults;
        }
    }
}
