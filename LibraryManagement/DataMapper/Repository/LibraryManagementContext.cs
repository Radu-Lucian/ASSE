namespace DataMapper.Repository
{
    using DomainModel.Model;
    using System.Data.Entity;


    public class LibraryManagementContext : DbContext
    {
        public LibraryManagementContext() :
            base("LibraryManagement")
        {

        }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Domain> Domain { get; set; }

        public virtual DbSet<Extension> Extensions { get; set; }

        public virtual DbSet<Librarian> Librarians { get; set; }

        public virtual DbSet<Publication> Publications { get; set; }

        public virtual DbSet<Reader> Readers { get; set; }

        public virtual DbSet<Stock> Stocks { get; set; }

        public virtual DbSet<Withdrawal> Withdrawals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Stock>().HasRequired(s => s.Publication).WithRequiredDependent(p => p.Stock);
        }
    }
}
