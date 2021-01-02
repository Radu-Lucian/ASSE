// <copyright file="Book.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
namespace DomainModel.Model
{
    using System.Collections.Generic;
    using System.Configuration;
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
        [NotNullValidator(MessageTemplate = "Book name cannot be null")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the domains.
        /// </summary>
        /// <value>
        /// The domains.
        /// </value>
        [NotNullValidator(MessageTemplate = "Book domains cannot be null")]
        public virtual ICollection<Domain> Domains { get; set; }

        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        /// <value>
        /// The authors.
        /// </value>
        [NotNullValidator(MessageTemplate = "Book authors cannot be null")]
        public virtual ICollection<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the publications.
        /// </summary>
        /// <value>
        /// The publications.
        /// </value>
        [NotNullValidator(MessageTemplate = "Book publications cannot be null")]
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
                validationResults.AddResult(new ValidationResult("Domains is empty", this, "ValidateDomains", "error", null));
            }

            if (this.Domains.Count > int.Parse(ConfigurationManager.AppSettings["DOM"]))
            {
                validationResults.AddResult(new ValidationResult("Number of domains is higher than DOM", this, "ValidateDomainsDOM", "error", null));
            }

            {
                Dictionary<string, uint> domains = new Dictionary<string, uint>();
                foreach (var domain in this.Domains)
                {
                    Domain curent = domain;
                    while (curent != null)
                    {
                        if (!domains.TryGetValue(curent.Name, out uint count))
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
                        validationResults.AddResult(new ValidationResult("Domains ", this, "ValidateDomainsInharitance", "error", null));
                    }
                }
            }

            if (this.Authors.Count == 0)
            {
                validationResults.AddResult(new ValidationResult("Authors is empty", this, "ValidateAuthors", "error", null));
            }

            if (this.Publications.Count == 0)
            {
                validationResults.AddResult(new ValidationResult("Publications is empty", this, "ValidatePublications", "error", null));
            }
        }
    }
}
