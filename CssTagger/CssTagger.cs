using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CssTagger
{
    public class CssTagger
    {
        public static void Main(string[] args)
        {
            if (args.Count() != 3) 
            {
                PrintUsageMessage();
            }
            else 
            {
                var runner = new Runner();
                var result = runner.RunTagging(args[0], args[1], args[2]);
                Console.WriteLine(result);
            }
        }

        private static void PrintUsageMessage()
        {
            Console.WriteLine("3 parameters are required: 1) Path to file containing keywords to locate, 2) String to tag keywords with, 3) Path to locate keywords in");
        }
    }
}