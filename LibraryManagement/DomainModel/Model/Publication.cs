// <copyright file="Publication.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the DB Publication entity. </summary>
namespace DomainModel.Model
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// Cover enumeration.
    /// </summary>
    public enum Cover
    {
        /// <summary>
        /// The hard cover.
        /// </summary>
        HardCover,

        /// <summary>
        /// The paper back.
        /// </summary>
        PaperBack,

        /// <summary>
        /// Invalid type.
        /// </summary>
        Invalid
    }

    /// <summary>
    /// Publication class.
    /// </summary>
    [HasSelfValidation]
    public class Publication
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the type of the cover.
        /// </summary>
        /// <value>
        /// The type of the cover.
        /// </value>
        [DomainValidator(Cover.HardCover, Cover.PaperBack, MessageTemplate = "Unknown cover type", Tag = "PublicationCoverTypeInvalid")]
        public Cover CoverType { get; set; }

        /// <summary>
        /// Gets or sets the number of pages.
        /// </summary>
        /// <value>
        /// The number of pages.
        /// </value>
        [RangeValidator(1, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore, MessageTemplate = "Publication page number must be grater than 0", Tag = "PublicationNumberOfPagesInvalid")]
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Gets or sets the publication date.
        /// </summary>
        /// <value>
        /// The publication date.
        /// </value>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the book.
        /// </summary>
        /// <value>
        /// The Publication book.
        /// </value>
        [NotNullValidator(MessageTemplate = "Publication book cannot be null", Tag = "PublicationBookNull")]
        public virtual Book Book { get; set; }

        /// <summary>
        /// Gets or sets the stock.
        /// </summary>
        /// <value>
        /// The stock.
        /// </value>
        [System.ComponentModel.DataAnnotations.Required]
        [NotNullValidator(MessageTemplate = "Publication stock cannot be null", Tag = "PublicationStockNull")]
        public virtual Stock Stock { get; set; }

        /// <summary>
        /// Gets or sets the publishing company.
        /// </summary>
        /// <value>
        /// The publishing company.
        /// </value>
        [NotNullValidator(MessageTemplate = "Publication publishing company cannot be null", Tag = "PublicationPublishingCompanyNull")]
        public virtual PublishingCompany PublishingCompany { get; set; }

        /// <summary>
        /// Gets or sets the book withdrawals.
        /// </summary>
        /// <value>
        /// The book withdrawals.
        /// </value>
        [NotNullValidator(MessageTemplate = "Publication withdrawals cannot be null", Tag = "PublicationBookWithdrawalsNull")]
        public virtual ICollection<Withdrawal> BookWithdrawals { get; set; }

        /// <summary>
        /// Validates the specified validation results.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            if (this.PublicationDate > DateTime.Now)
            {
                validationResults.AddResult(new ValidationResult("Publication date cannot be higher than the current date", this, "Publication", "ValidateDateTime", null));
            }
        }
    }
}
