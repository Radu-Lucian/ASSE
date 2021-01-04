// <copyright file="Withdrawal.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Defines the DB Withdrawal entity. </summary>
namespace DomainModel.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel.Options;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// Withdrawal class.
    /// </summary>
    [HasSelfValidation]
    public class Withdrawal
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the rented date.
        /// </summary>
        /// <value>
        /// The rented date.
        /// </value>
        [NotNullValidator(MessageTemplate = "Withdrawal rented date cannot be null")]
        public DateTime RentedDate { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [NotNullValidator(MessageTemplate = "Withdrawal due date cannot be null")]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the extensions.
        /// </summary>
        /// <value>
        /// The extensions.
        /// </value>
        [NotNullValidator(MessageTemplate = "Withdrawal extensions cannot be null")]
        public virtual ICollection<Extension> Extensions { get; set; }

        /// <summary>
        /// Gets or sets the publications.
        /// </summary>
        /// <value>
        /// The publications.
        /// </value>
        [NotNullValidator(MessageTemplate = "Withdrawal publications cannot be null")]
        public virtual ICollection<Publication> Publications { get; set; }

        /// <summary>
        /// Gets or sets the reader.
        /// </summary>
        /// <value>
        /// The reader.
        /// </value>
        [NotNullValidator(MessageTemplate = "Withdrawal reader cannot be null")]
        public virtual Reader Reader { get; set; }

        /// <summary>
        /// Validates the specified validation results.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            if (this.Publications.Count == 0)
            {
                validationResults.AddResult(new ValidationResult("Publications is empty", this, "ValidatePublications", "error", null));
            }

            if (this.Publications.Count > ApplicationOptions.Options.C)
            {
                validationResults.AddResult(new ValidationResult("Cannot withdraw more books than \"C\"", this, "ValidatePublicationsC", "error", null));
            }

            {
                if (this.Publications.Count >= 3)
                {
                    HashSet<string> domains = new HashSet<string>();

                    foreach (var publication in this.Publications)
                    {
                        foreach (var domain in publication.Book.Domains)
                        {
                            domains.Add(domain.Name);
                        }
                    }

                    if (domains.Count < 2)
                    {
                        validationResults.AddResult(new ValidationResult("Cannot withdraw more than 3 books if the books are not from 2 distinct categories", this, "ValidatePublicationsDomain", "error", null));
                    }
                }
            }

            if (this.RentedDate >= this.DueDate)
            {
                validationResults.AddResult(new ValidationResult("Invalid rented and due dates", this, "ValidatePublicationsDate", "error", null));
            }

            {
                var orderedExtentions = this.Extensions.Where(element => element.CreationDate >= DateTime.Today.AddMonths(-3)).Count();
                if (orderedExtentions > ApplicationOptions.Options.LIM)
                {
                    validationResults.AddResult(new ValidationResult("Number of extensions in last 3 months cannot be grater than \"LIM\"", this, "ValidateExtentions", "error", null));
                }
            }

            {
                var numberOfBooksRentedInPeriod = 0;
                foreach (var withdrawal in this.Reader.Withdrawals)
                {
                    if ((withdrawal.DueDate - withdrawal.RentedDate).TotalDays > ApplicationOptions.Options.PER)
                    {
                        numberOfBooksRentedInPeriod += withdrawal.Publications.Count;
                    }
                }

                if (numberOfBooksRentedInPeriod + this.Publications.Count > ApplicationOptions.Options.NMC)
                {
                    validationResults.AddResult(new ValidationResult("Number of rented books within the period \"PER\" cannot be grater than \"NMC\"", this, "ValidateExtentions", "error", null));
                }
            }

            {
                Dictionary<string, uint> domains = new Dictionary<string, uint>();
                foreach (var withdrawal in this.Reader.Withdrawals)
                {
                    if (withdrawal.RentedDate >= DateTime.Today.AddMonths(-ApplicationOptions.Options.L))
                    {
                        foreach (var publication in withdrawal.Publications)
                        {
                            foreach (var domain in publication.Book.Domains)
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
                        }
                    }
                }

                foreach (var publication in this.Publications)
                {
                    foreach (var domain in publication.Book.Domains)
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
                }

                foreach (var domain in domains)
                {
                    if (domain.Value > ApplicationOptions.Options.D)
                    {
                        validationResults.AddResult(new ValidationResult("Cannot borrow more than \"D\" books that are from the same domain within a period \"L\"  ", this, "ValidateWithdrawalBookDomain", "error", null));
                    }
                }
            }

            {
                var numberOfBooksRentedInPeriod = 0;
                foreach (var withdrawal in this.Reader.Withdrawals)
                {
                    if (withdrawal.RentedDate.Date == DateTime.Today)
                    {
                        numberOfBooksRentedInPeriod += withdrawal.Publications.Count;
                    }
                }

                if (numberOfBooksRentedInPeriod + this.Publications.Count > ApplicationOptions.Options.NCZ)
                {
                    validationResults.AddResult(new ValidationResult("Number of rented books within one day cannot be grater than \"NCZ\"", this, "ValidateWithdrawalBookDay", "error", null));
                }
            }

            {
                HashSet<string> booksThatHaveNotPassedTheGracePeriod = new HashSet<string>();
                foreach (var withdrawal in this.Reader.Withdrawals)
                {
                    foreach (var publication in withdrawal.Publications)
                    {
                        if ((withdrawal.DueDate - withdrawal.RentedDate).TotalDays > ApplicationOptions.Options.DELTA)
                        {
                            booksThatHaveNotPassedTheGracePeriod.Add(publication.Book.Name);
                        }
                    }
                }

                foreach (var publication in this.Publications)
                {
                    if (booksThatHaveNotPassedTheGracePeriod.Contains(publication.Book.Name))
                    {
                        validationResults.AddResult(new ValidationResult("A book cannot be borrowed another time within a grace period \"DELTA\"", this, "ValidateWithdrawalSameBookGraceDate", "error", null));
                    }
                }
            }
        }
    }
}
