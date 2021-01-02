// <copyright file="Extension.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
namespace DomainModel.Model
{
    using System;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// Extension class.
    /// </summary>
    [HasSelfValidation]
    public class Extension
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the extra days.
        /// </summary>
        /// <value>
        /// The extra days.
        /// </value>
        [RangeValidator(0, RangeBoundaryType.Exclusive, uint.MaxValue, RangeBoundaryType.Ignore, MessageTemplate = "Extension days must be grater than 0")]
        public uint ExtraDays { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        [NotNullValidator(MessageTemplate = "Extension creation date cannot be null")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the withdrawal.
        /// </summary>
        /// <value>
        /// The withdrawal.
        /// </value>
        [NotNullValidator(MessageTemplate = "Extension withdrawal cannot be null")]
        public virtual Withdrawal Withdrawal { get; set; }

        /// <summary>
        /// Validates the specified validation results.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            if (this.CreationDate > DateTime.Today)
            {
                validationResults.AddResult(new ValidationResult("Creation date cannot be in the future", this, "ValidateExtentionCreationDate", "error", null));
            }
        }
    }
}
