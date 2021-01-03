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
        /// Finds all.
        /// </summary>
        /// <returns>Returns all entities</returns>
        IQueryable<T> FindAll();

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
    }
}
