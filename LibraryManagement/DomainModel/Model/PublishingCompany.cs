// <copyright file="PublishingCompany.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the DB Publishing Company entity. </summary>
namespace DomainModel.Model
{
    using System.Collections.Generic;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// Publishing Company class.
    /// </summary>
    public class PublishingCompany
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
        /// The publishing company name.
        /// </value>
        [NotNullValidator(MessageTemplate = "PublishingCompany name cannot be null")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the publication.
        /// </summary>
        /// <value>
        /// The publication.
        /// </value>
        [NotNullValidator(MessageTemplate = "PublishingCompany name cannot be null")]
        public virtual ICollection<Publication> Publications { get; set; }
    }
}
