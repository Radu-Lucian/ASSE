// <copyright file="Reader.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the DB Reader entity. </summary>
namespace DomainModel.Model
{
    using System.Collections.Generic;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// Reader class.
    /// </summary>
    [HasSelfValidation]
    public class Reader
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
        [NotNullValidator(MessageTemplate = "Reader first name cannot be null")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore, ErrorMessage = "Reader first name should have at least {3} letters")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [NotNullValidator(MessageTemplate = "Reader last name cannot be null")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore, ErrorMessage = "Reader last name should have at least {3} letters")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [NotNullValidator(MessageTemplate = "Reader address name cannot be null")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value> 
        [RegexValidator(@"^(\+4|)?(07[0-8]{1}[0-9]{1}|02[0-9]{2}|03[0-9]{2}){1}?(\s|\.|\-)?([0-9]{3}(\s|\.|\-|)){2}$", MessageTemplate = "Reader phone number is invalid")] // https://regexr.com/39fv1
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        [RegexValidator(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", MessageTemplate = "Reader email address is invalid")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [NotNullValidator(MessageTemplate = "Reader gender cannot be null")]
        [DomainValidator(new object[] { "m", "f", "M", "F" }, MessageTemplate = "Unknown gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the withdrawals.
        /// </summary>
        /// <value>
        /// The withdrawals.
        /// </value>
        [NotNullValidator(MessageTemplate = "Reader withdrawals cannot be null")]
        public virtual ICollection<Withdrawal> Withdrawals { get; set; }

        /// <summary>
        /// Validates the specified validation results.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            if (this.EmailAddress == null && this.PhoneNumber == null)
            {
                validationResults.AddResult(new ValidationResult("Reader has no valid contact information", this, "ValidateReaderPhoneEmail", "error", null));
            }
        }
    }
}
