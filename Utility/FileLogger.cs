using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResposibility.Utility
{

    public class FileLogger : ILogger
    {
        public void Log(string message)
        {
            //TODO : File logging
            Console.WriteLine($"{message} -> logged successfully to file");
        }
    }

}
