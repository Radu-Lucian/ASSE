// <copyright file="BaseService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the Base service. </summary>
namespace ServiceLayer.Service.Base
{
    using System.Reflection;
    using DataMapper.Logger;
    using DataMapper.Repository.RepositoryBase;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// Class BaseService.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    public class BaseService<T> : IBaseService<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{T}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BaseService(IRepositoryBase<T> repository)
        {
            this.Repository = repository;
            this.Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>The repository.</value>
        private IRepositoryBase<T> Repository { get; set; }

        /// <summary>
        /// Creates the specified to create.
        /// </summary>
        /// <param name="toCreate">To create.</param>
        /// <returns>ValidationResults based on entity validation.</returns>
        /// <exception cref="System.ArgumentNullException">Null toCreate.</exception>
        public ValidationResults Create(T toCreate)
        {
            if (toCreate is null)
            {
                Logger.LogError($"{typeof(T).Name} is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(toCreate));
            }

            var withdrawalValidator = ValidationFactory.CreateValidator<T>();
            ValidationResults valResults = withdrawalValidator.Validate(toCreate);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.Repository.Create(toCreate);
            return valResults;
        }

        /// <summary>
        /// Updates the specified to update.
        /// </summary>
        /// <param name="toUpdate">To update.</param>
        /// <returns>ValidationResults based on entity validation.</returns>
        /// <exception cref="System.ArgumentNullException">Null toUpdate.</exception>
        public ValidationResults Update(T toUpdate)
        {
            if (toUpdate is null)
            {
                Logger.LogError($"{typeof(T).Name} is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(toUpdate));
            }

            var withdrawalValidator = ValidationFactory.CreateValidator<T>();
            ValidationResults valResults = withdrawalValidator.Validate(toUpdate);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.Repository.Update(toUpdate);
            return valResults;
        }

        /// <summary>
        /// Deletes the specified to delete.
        /// </summary>
        /// <param name="toDelete">To delete.</param>
        /// <returns>ValidationResults based on entity validation.</returns>
        /// <exception cref="System.ArgumentNullException">Null toDelete.</exception>
        public ValidationResults Delete(T toDelete)
        {
            if (toDelete is null)
            {
                Logger.LogError($"{typeof(T).Name} is null", MethodBase.GetCurrentMethod());
                throw new System.ArgumentNullException(nameof(toDelete));
            }

            var withdrawalValidator = ValidationFactory.CreateValidator<T>();
            ValidationResults valResults = withdrawalValidator.Validate(toDelete);

            if (!valResults.IsValid)
            {
                foreach (var result in valResults)
                {
                    Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                }

                return valResults;
            }

            this.Repository.Delete(toDelete);
            return valResults;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            this.Repository.Delete(id);
        }

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity T.</returns>
        public T Find(int id)
        {
            return this.Repository.Find(id);
        }
    }
}
