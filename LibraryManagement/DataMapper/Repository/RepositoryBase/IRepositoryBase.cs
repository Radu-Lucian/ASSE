// <copyright file="IRepositoryBase.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Base interface for repository. </summary>
namespace DataMapper.Repository.RepositoryBase
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The IRepositoryBase interface
    /// </summary>
    /// <typeparam name="T">Entity data model</typeparam>
    public interface IRepositoryBase<T>
    {
        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity T.</returns>
        T Find(int id);

        /// <summary>
        /// Finds the by condition.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Return entities that met the condition</returns>
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Create(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(int id);
    }
}
