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
        [DateTimeRangeValidator("2010-01-01T00:00:00", "2100-01-01T00:00:00", MessageTemplate = "Withdrawal rented date must be after 2010-01-01 but before 2100-01-01", Tag = "WithdrawalRentedDate")]
        public DateTime RentedDate { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [DateTimeRangeValidator("2010-01-01T00:00:00", "2100-01-01T00:00:00", MessageTemplate = "Withdrawal due date must be after 2010-01-01 but before 2100-01-01", Tag = "WithdrawalDueDate")]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the extensions.
        /// </summary>
        /// <value>
        /// The extensions.
        /// </value>
        [NotNullValidator(MessageTemplate = "Withdrawal extensions cannot be null", Tag = "WithdrawalExtensionsNull")]
        public virtual ICollection<Extension> Extensions { get; set; }

        /// <summary>
        /// Gets or sets the publications.
        /// </summary>
        /// <value>
        /// The publications.
        /// </value>
        [NotNullValidator(MessageTemplate = "Withdrawal publications cannot be null", Tag = "WithdrawalPublicationsNull")]
        public virtual ICollection<Publication> Publications { get; set; }

        /// <summary>
        /// Gets or sets the reader.
        /// </summary>
        /// <value>
        /// The reader.
        /// </value>
        [NotNullValidator(MessageTemplate = "Withdrawal reader cannot be null", Tag = "WithdrawalReaderNull")]
        public virtual Reader Reader { get; set; }

        /// <summary>
        /// Validates the specified validation results.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            int maxBooksPerInterval = ApplicationOptions.Options.NMC;
            int maxBooksPerWithdrawal = ApplicationOptions.Options.C;
            int maxBooksPerDomain = ApplicationOptions.Options.D;
            int maxExtensions = ApplicationOptions.Options.LIM;
            int rentedBookPeriod = ApplicationOptions.Options.PER;
            int bookGracePeriod = ApplicationOptions.Options.DELTA;
            int maxBooksPerDay = ApplicationOptions.Options.NCZ;

            if (this.Reader is Librarian)
            {
                maxBooksPerInterval *= 2;
                maxBooksPerWithdrawal *= 2;
                maxBooksPerDomain *= 2;
                maxExtensions *= 2;
                rentedBookPeriod /= 2;
                bookGracePeriod /= 2;
                maxBooksPerDay = int.MaxValue;
            }

            if (this.Publications.Count == 0)
            {
                validationResults.AddResult(new ValidationResult("Publications is empty", this, "Withdrawal", "ValidatePublications", null));
            }

            if (this.Publications.Count > maxBooksPerWithdrawal)
            {
                validationResults.AddResult(new ValidationResult($"Cannot withdraw more books than {maxBooksPerWithdrawal}", this, "Withdrawal", "ValidatePublicationsC", null));
            }

            {
                if (this.Publications.Count >= 3)
                {
                    HashSet<string> domains = new HashSet<string>();

                    foreach (var publication in this.Publications)
                    {
                        // var bookValidator = ValidationFactory.CreateValidator<Book>();
                        // ValidationResults valResults = bookValidator.Validate(publication.Book);
                        foreach (var domain in publication.Book.Domains)
                        {
                            domains.Add(domain.Name);
                        }
                    }

                    if (domains.Count < 2)
                    {
                        validationResults.AddResult(new ValidationResult("Cannot withdraw more than 3 books if the books are not from 2 distinct categories", this, "Withdrawal", "ValidatePublicationsDomain", null));
                    }
                }
            }

            if (this.RentedDate >= this.DueDate)
            {
                validationResults.AddResult(new ValidationResult("Invalid rented and due dates", this, "Withdrawal", "ValidatePublicationsDate", null));
            }

            {
                var orderedExtentions = this.Extensions.Where(element => element.CreationDate >= DateTime.Today.AddMonths(-3)).Count();
                if (orderedExtentions > maxExtensions)
                {
                    validationResults.AddResult(new ValidationResult($"Number of extensions in last 3 months cannot be grater than {maxExtensions}", this, "Withdrawal", "ValidateExtentions", null));
                }
            }

            {
                foreach (var publication in this.Publications)
                {
                    if (publication.Stock.InitialStock == publication.Stock.NumberOfBooksForLecture ||
                        (publication.Stock.InitialStock - publication.Stock.NumberOfBooksForLecture - publication.Stock.RentedStock) < (publication.Stock.InitialStock / 10))
                    {
                        validationResults.AddResult(new ValidationResult("Publication has insufficient stock", this, "Withdrawal", "ValidatePublicationStock", null));
                    }
                }
            }

            {
                var numberOfBooksRentedInPeriod = 0;
                foreach (var withdrawal in this.Reader.Withdrawals)
                {
                    if ((withdrawal.DueDate - withdrawal.RentedDate).TotalDays > rentedBookPeriod)
                    {
                        numberOfBooksRentedInPeriod += withdrawal.Publications.Count;
                    }
                }

                if (numberOfBooksRentedInPeriod + this.Publications.Count > maxBooksPerInterval)
                {
                    validationResults.AddResult(new ValidationResult($"Number of rented books within the period {rentedBookPeriod} cannot be grater than {maxBooksPerInterval}", this, "Withdrawal", "ValidatePublicationsBook", null));
                }
            }

            {
                Dictionary<string, uint> domains = new Dictionary<string, uint>();
                foreach (var withdrawal in this.Reader.Withdrawals)
                {
                    if (withdrawal.RentedDate <= DateTime.Today.AddMonths(-ApplicationOptions.Options.L))
                    {
                        foreach (var publication in withdrawal.Publications)
                        {
                            foreach (var domain in publication.Book.Domains)
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
                }

                foreach (var domain in domains)
                {
                    if (domain.Value > maxBooksPerDomain)
                    {
                        validationResults.AddResult(new ValidationResult($"Cannot borrow more than {maxBooksPerDomain} books that are from the same domain within a period {ApplicationOptions.Options.L}", this, "Withdrawal", "ValidateWithdrawalBookDomain", null));
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

                if (numberOfBooksRentedInPeriod + this.Publications.Count > maxBooksPerDay)
                {
                    validationResults.AddResult(new ValidationResult($"Number of rented books within one day cannot be grater than {maxBooksPerDay}", this, "Withdrawal", "ValidateWithdrawalBookDay", null));
                }
            }

            {
                HashSet<string> booksThatHaveNotPassedTheGracePeriod = new HashSet<string>();
                foreach (var withdrawal in this.Reader.Withdrawals)
                {
                    foreach (var publication in withdrawal.Publications)
                    {
                        if ((DateTime.Today - withdrawal.RentedDate).TotalDays < bookGracePeriod)
                        {
                            booksThatHaveNotPassedTheGracePeriod.Add(publication.Book.Name);
                        }
                    }
                }

                foreach (var publication in this.Publications)
                {
                    if (booksThatHaveNotPassedTheGracePeriod.Contains(publication.Book.Name))
                    {
                        validationResults.AddResult(new ValidationResult($"A book cannot be borrowed another time within a grace period {bookGracePeriod}", this, "Withdrawal", "ValidateWithdrawalSameBookGraceDate", null));
                    }
                }
            }
        }
    }
}
