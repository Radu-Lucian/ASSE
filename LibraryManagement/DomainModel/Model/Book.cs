// <copyright file="Book.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the DB Book entity. </summary>
namespace DomainModel.Model
{
    using System.Collections.Generic;
    using DomainModel.Options;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// Book class.
    /// </summary>
    [HasSelfValidation]
    public class Book
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
        /// The book name.
        /// </value>
        [NotNullValidator(MessageTemplate = "Book name cannot be null", Tag = "BookNameNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 200, RangeBoundaryType.Inclusive, ErrorMessage = "Book name should be between {3} and {5} characters", Tag = "BookNameLenght")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the domains.
        /// </summary>
        /// <value>
        /// The domains.
        /// </value>
        [NotNullValidator(MessageTemplate = "Book domains cannot be null", Tag = "BookDomainsNull")]
        public virtual ICollection<Domain> Domains { get; set; }

        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        /// <value>
        /// The authors.
        /// </value>
        [NotNullValidator(MessageTemplate = "Book authors cannot be null", Tag = "BookAuthorsNull")]
        public virtual ICollection<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the publications.
        /// </summary>
        /// <value>
        /// The publications.
        /// </value>
        [NotNullValidator(MessageTemplate = "Book publications cannot be null", Tag = "BookPublicationsNull")]
        public virtual ICollection<Publication> Publications { get; set; }

        /// <summary>
        /// Validates the specified validation results.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            if (this.Domains.Count == 0)
            {
                validationResults.AddResult(new ValidationResult("Domains is empty", this, "Domains", "ValidateDomains", null));
            }

            if (this.Domains.Count > ApplicationOptions.Options.DOM)
            {
                validationResults.AddResult(new ValidationResult("Number of domains is higher than DOM", this, "Domains", "ValidateDomainsDOM", null));
            }

            {
                Dictionary<string, uint> domains = new Dictionary<string, uint>();
                foreach (var domain in this.Domains)
                {
                    Domain curent = domain;
                    while (curent != null)
                    {
                        if (domains.TryGetValue(curent.Name, out uint count))
                        {
                            domains[curent.Name] = count + 1;
                        }
                        else
                        {
                            domains[curent.Name] = 1;
                        }

                        curent = curent.Parent;
                    }
                }

                foreach (var domain in domains)
                {
                    if (domain.Value > 1)
                    {
                        validationResults.AddResult(new ValidationResult("Domains cannot be in relations ancestor-descendant", this, "Domains", "ValidateDomainsInharitance", null));
                    }
                }
            }

            if (this.Authors.Count == 0)
            {
                validationResults.AddResult(new ValidationResult("Authors is empty", this, "Authors", "ValidateAuthors", null));
            }

            if (this.Publications.Count == 0)
            {
                validationResults.AddResult(new ValidationResult("Publications is empty", this, "Publications", "ValidatePublications", null));
            }
        }
    }
}
