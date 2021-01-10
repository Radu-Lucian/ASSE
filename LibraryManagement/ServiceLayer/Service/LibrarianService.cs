// <copyright file="LibrarianService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Librarian class. </summary>
namespace ServiceLayer.Service
{
    using DataMapper.Repository;
    using DomainModel.Model;
    using ServiceLayer.Service.Base;

    /// <summary>
    /// Class LibrarianService.
    /// Implements the <see cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Librarian}" />
    /// </summary>
    /// <seealso cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Librarian}" />
    public class LibrarianService : BaseService<Librarian>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibrarianService"/> class.
        /// </summary>
        /// <param name="extensionRepository">The extension repository.</param>
        public LibrarianService(LibrarianRepository extensionRepository) :
            base(extensionRepository)
        {
        }
    }
}
