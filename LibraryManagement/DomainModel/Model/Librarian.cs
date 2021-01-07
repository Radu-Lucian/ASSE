// <copyright file="Librarian.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the DB Librarian entity. </summary>
namespace DomainModel.Model
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// Librarian class.
    /// </summary>
    /// <seealso cref="DomainModel.Model.Reader" />
    [HasSelfValidation]
    public class Librarian : Reader
    {
    }
}
