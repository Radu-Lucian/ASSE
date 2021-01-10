// <copyright file="IBaseService.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the IBase service. </summary>
namespace ServiceLayer.Service.Base
{
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// Interface IBaseService
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    public interface IBaseService<T>
    {
        /// <summary>
        /// Creates the specified to create.
        /// </summary>
        /// <param name="toCreate">To create.</param>
        /// <returns>ValidationResults based on entity validation.</returns>
        ValidationResults Create(T toCreate);

        /// <summary>
        /// Updates the specified to update.
        /// </summary>
        /// <param name="toUpdate">To update.</param>
        /// <returns>ValidationResults based on entity validation.</returns>
        ValidationResults Update(T toUpdate);

        /// <summary>
        /// Deletes the specified to delete.
        /// </summary>
        /// <param name="toDelete">To delete.</param>
        /// <returns>ValidationResults based on entity validation.</returns>
        ValidationResults Delete(T toDelete);

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity T.</returns>
        T Find(int id);
    }
}
