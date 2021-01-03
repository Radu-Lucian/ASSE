// <copyright file="ApplicationOptions.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> ApplicationOptions class. </summary>
namespace DomainModel.Options
{
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// ApplicationOptions class.
    /// </summary>
    public class ApplicationOptions
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static ApplicationOptions instance = null;

        /// <summary>
        /// Prevents a default instance of the <see cref="ApplicationOptions"/> class from being created.
        /// </summary>
        private ApplicationOptions()
        {
            var definition = new { NCZ = 0, DOM = 0, NMC = 0, PER = 0, C = 0, D = 0, L = 0, LIM = 0, DELTA = 0, PERSIMP = 0 };

            var loaded = JsonConvert.DeserializeAnonymousType(File.ReadAllText("options.json"), definition);

            this.NCZ = loaded.NCZ;
            this.DOM = loaded.DOM;
            this.NMC = loaded.NMC;
            this.PER = loaded.PER;
            this.C = loaded.C;
            this.D = loaded.D;
            this.L = loaded.L;
            this.LIM = loaded.LIM;
            this.DELTA = loaded.DELTA;
            this.PERSIMP = loaded.PERSIMP;
        }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public static ApplicationOptions Options
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationOptions();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the Maximum number of books per day.
        /// </summary>
        /// <value>
        /// The Maximum number of books per day.
        /// </value>
        public int NCZ { get; private set; }

        /// <summary>
        /// Gets the Maximum number of books domains.
        /// </summary>
        /// <value>
        /// The Maximum number of books domains.
        /// </value>
        public int DOM { get; private set; }

        /// <summary>
        /// Gets the Maximum number of books per interval.
        /// </summary>
        /// <value>
        /// The Maximum number of books per interval.
        /// </value>
        public int NMC { get; private set; }

        /// <summary>
        /// Gets the Days interval.
        /// </summary>
        /// <value>
        /// The Days interval.
        /// </value>
        public int PER { get; private set; }

        /// <summary>
        /// Gets the Maximum number of books per borrow.
        /// </summary>
        /// <value>
        /// The Maximum number of books per borrow.
        /// </value>
        public int C { get; private set; }

        /// <summary>
        /// Gets the Maximum number of books per domain.
        /// </summary>
        /// <value>
        /// The Maximum number of books per domain.
        /// </value>
        public int D { get; private set; }

        /// <summary>
        /// Gets the Months interval for borrowing.
        /// </summary>
        /// <value>
        /// The Months interval for borrowing.
        /// </value>
        public int L { get; private set; }

        /// <summary>
        /// Gets the Maximum number of extensions.
        /// </summary>
        /// <value>
        /// The Maximum number of extensions.
        /// </value>
        public int LIM { get; private set; }

        /// <summary>
        /// Gets the Grace Period.
        /// </summary>
        /// <value>
        /// The Grace Period.
        /// </value>
        public int DELTA { get; private set; }

        /// <summary>
        /// Gets the Maximum granted books per day for librarian.
        /// </summary>
        /// <value>
        /// The Maximum granted books per day for librarian.
        /// </value>
        public int PERSIMP { get; private set; }
    }
}
