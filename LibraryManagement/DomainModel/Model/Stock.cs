// <copyright file="Stock.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the DB Stock entity. </summary>
namespace DomainModel.Model
{
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// Stock class.
    /// </summary>
    [HasSelfValidation]
    public class Stock
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the initial stock.
        /// </summary>
        /// <value>
        /// The initial stock.
        /// </value>
        public uint InitialStock { get; set; }

        /// <summary>
        /// Gets or sets the number of books for lecture.
        /// </summary>
        /// <value>
        /// The number of books for lecture.
        /// </value>
        public uint NumberOfBooksForLecture { get; set; }

        /// <summary>
        /// Gets or sets the publication.
        /// </summary>
        /// <value>
        /// The publication.
        /// </value>
        [NotNullValidator(MessageTemplate = "Stock publication cannot be null")]
        public virtual Publication Publication { get; set; }

        /// <summary>
        /// Validates the specified validation results.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            if (this.NumberOfBooksForLecture > this.InitialStock)
            {
                validationResults.AddResult(new ValidationResult("Number of books for lecture must be less than the initial stock", this, "ValidateBookStock", "error", null));
            }
        }
    }
}
