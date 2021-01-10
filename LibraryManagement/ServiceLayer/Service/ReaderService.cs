// <copyright file="ReaderService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the ReaderService service. </summary>
namespace ServiceLayer.Service
{
    using DataMapper.Repository;
    using DomainModel.Model;
    using ServiceLayer.Service.Base;

    /// <summary>
    /// Class ReaderService.
    /// Implements the <see cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Reader}" />
    /// </summary>
    /// <seealso cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Reader}" />
    public class ReaderService : BaseService<Reader>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderService"/> class.
        /// </summary>
        /// <param name="readerRepository">The reader repository.</param>
        public ReaderService(ReaderRepository readerRepository) :
            base(readerRepository)
        {
        }
    }
}
