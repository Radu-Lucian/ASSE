using DataMapper.Repository;
using DomainModel.Model;
using DomainModel.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            //ApplicationOptions options = new ApplicationOptions();
            //options.NCZ = 10;
            //options.DOM = 2;
            //options.NMC = 5;
            //options.PER = 10;
            //options.C = 5;
            //options.D = 3;
            //options.L = 2;
            //options.LIM = 10;
            //options.DELTA = 31;
            //options.PERSIMP = 10;


            //// serialize JSON to a string and then write string to a file
            //File.WriteAllText(@"D:\options.json", JsonConvert.SerializeObject(options));

            _ = ApplicationOptions.Options.L;
            _ = ApplicationOptions.Options.NCZ;
            _ = ApplicationOptions.Options.DELTA;
            _ = ApplicationOptions.Options.PERSIMP;
        }
    }
}
