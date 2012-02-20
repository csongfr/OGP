using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Log
{
    public interface ILogEntry
    {
        string GetStringMessage();
    }
}
