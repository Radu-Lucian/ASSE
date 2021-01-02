// <copyright file="Domain.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
namespace DomainModel.Model
{
    using System.Collections.Generic;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// Domain class.
    /// </summary>
    public class Domain
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The domain name.
        /// </value>
        [NotNullValidator(MessageTemplate = "Domain name cannot be null")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public Domain Parent { get; set; }

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        [NotNullValidator(MessageTemplate = "Domain books cannot be null")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
