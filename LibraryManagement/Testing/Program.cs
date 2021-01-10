// ***********************************************************************
// Assembly         : Testing
// Author           : Radu Lucian Andrei
// Created          : 01-03-2021
//
// Last Modified By : Radu Lucian Andrei
// Last Modified On : 01-03-2021
// ***********************************************************************
// <copyright file="Program.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary>Main class.</summary>
// ***********************************************************************

namespace Testing
{
    using DomainModel.Options;

    /// <summary>
    /// Class Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            // ApplicationOptions options = new ApplicationOptions();
            // options.NCZ = 10;
            // options.DOM = 2;
            // options.NMC = 5;
            // options.PER = 10;
            // options.C = 5;
            // options.D = 3;
            // options.L = 2;
            // options.LIM = 10;
            // options.DELTA = 31;
            // options.PERSIMP = 10;

            // serialize JSON to a string and then write string to a file
            // File.WriteAllText(@"D:\options.json", JsonConvert.SerializeObject(options));
            _ = ApplicationOptions.Options.L;
            _ = ApplicationOptions.Options.NCZ;
            _ = ApplicationOptions.Options.DELTA;
            _ = ApplicationOptions.Options.PERSIMP;
        }
    }
}
