// <copyright file="Author.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the DB Author entity. </summary>
namespace DomainModel.Model
{
    using System.Collections.Generic;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// Author class.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [NotNullValidator(MessageTemplate = "Author first name cannot be null", Tag = "AuthorFirstNameNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 200, RangeBoundaryType.Inclusive, MessageTemplate = "Author first name should be between 2 and 200 characters", Tag = "AuthorFirstNameLength")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [NotNullValidator(MessageTemplate = "Author last name cannot be null", Tag = "AuthorLastNameNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 200, RangeBoundaryType.Inclusive, MessageTemplate = "Author last name should be between 2 and 200 characters", Tag = "AuthorLastNameLength")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        [NotNullValidator(MessageTemplate = "Author books cannot be null", Tag = "AuthorBooksNull")]
        public ICollection<Book> Books { get; set; }
    }
}
