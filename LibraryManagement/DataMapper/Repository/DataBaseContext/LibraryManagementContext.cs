// <copyright file="LibraryManagementContext.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> DbContext class. </summary>
namespace DataMapper.Repository.DataBaseContext
{
    using System.Data.Entity;
    using DomainModel.Model;

    /// <summary>
    /// LibraryManagementContext class.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class LibraryManagementContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryManagementContext"/> class.
        /// </summary>
        public LibraryManagementContext() :
            base("LibraryManagement")
        {
        }

        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        /// <value>
        /// The authors.
        /// </value>
        public virtual DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        public virtual DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        public virtual DbSet<Domain> Domain { get; set; }

        /// <summary>
        /// Gets or sets the extensions.
        /// </summary>
        /// <value>
        /// The extensions.
        /// </value>
        public virtual DbSet<Extension> Extensions { get; set; }

        /// <summary>
        /// Gets or sets the librarians.
        /// </summary>
        /// <value>
        /// The librarians.
        /// </value>
        public virtual DbSet<Librarian> Librarians { get; set; }

        /// <summary>
        /// Gets or sets the publications.
        /// </summary>
        /// <value>
        /// The publications.
        /// </value>
        public virtual DbSet<Publication> Publications { get; set; }

        /// <summary>
        /// Gets or sets the publishing companies.
        /// </summary>
        /// <value>
        /// The publishing companies.
        /// </value>
        public virtual DbSet<PublishingCompany> PublishingCompanies { get; set; }

        /// <summary>
        /// Gets or sets the readers.
        /// </summary>
        /// <value>
        /// The readers.
        /// </value>
        public virtual DbSet<Reader> Readers { get; set; }

        /// <summary>
        /// Gets or sets the stocks.
        /// </summary>
        /// <value>
        /// The stocks.
        /// </value>
        public virtual DbSet<Stock> Stocks { get; set; }

        /// <summary>
        /// Gets or sets the withdrawals.
        /// </summary>
        /// <value>
        /// The withdrawals.
        /// </value>
        public virtual DbSet<Withdrawal> Withdrawals { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuilder, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the data base ModelBuilder and data base ContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Stock>().HasRequired(s => s.Publication).WithRequiredDependent(p => p.Stock);
        }
    }
}
