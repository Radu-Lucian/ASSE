// <copyright file="StockService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Stock service. </summary>
namespace ServiceLayer.Service
{
    using DataMapper.Repository;
    using DomainModel.Model;
    using ServiceLayer.Service.Base;

    /// <summary>
    /// Class StockService.
    /// Implements the <see cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Stock}" />
    /// </summary>
    /// <seealso cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Stock}" />
    public class StockService : BaseService<Stock>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockService"/> class.
        /// </summary>
        /// <param name="stockRepository">The stock repository.</param>
        public StockService(StockRepository stockRepository) :
            base(stockRepository)
        {
        }
    }
}
