// <copyright file="StockService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Stock service. </summary>
namespace ServiceLayer.Service
{
    using System.Reflection;
    using DataMapper.Logger;
    using DataMapper.Repository;
    using DomainModel.Model;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// Class StockService.
    /// </summary>
    public class StockService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockService"/> class.
        /// </summary>
        /// <param name="stockRepository">The stock repository.</param>
        public StockService(StockRepository stockRepository)
        {
            this.StockRepository = stockRepository;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the stock repository.
        /// </summary>
        /// <value>The stock repository.</value>
        private StockRepository StockRepository { get; set; }

        /// <summary>
        /// Creates the stock.
        /// </summary>
        /// <param name="stock">The stock.</param>
        /// <returns>ValidationResults based on author entity.</returns>
        /// <exception cref="System.ArgumentNullException">Null stock.</exception>
        public ValidationResults CreateStock(Stock stock)
        {
            if (stock is null)
            {
                Logger.LogError("Stock is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(stock));
            }

            var stockValidator = ValidationFactory.CreateValidator<Stock>();
            ValidationResults valResults = stockValidator.Validate(stock);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.StockRepository.Create(stock);
            return valResults;
        }
    }
}
