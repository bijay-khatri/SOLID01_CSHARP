using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResposibility.Utility
{
    public interface ILogger
    {
        void Log<T>(T message);
    }
}
