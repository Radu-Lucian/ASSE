// <copyright file="AuthorService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Author service. </summary>
namespace ServiceLayer.Service
{
    using DataMapper.Repository;
    using DomainModel.Model;
    using ServiceLayer.Service.Base;

    /// <summary>
    /// Class AuthorService.
    /// Implements the <see cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Author}" />
    /// </summary>
    /// <seealso cref="ServiceLayer.Service.Base.BaseService{DomainModel.Model.Author}" />
    public class AuthorService : BaseService<Author>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorService"/> class.
        /// </summary>
        /// <param name="authorRepository">The author repository.</param>
        public AuthorService(AuthorRepository authorRepository) :
            base(authorRepository)
        {
        }
    }
}
