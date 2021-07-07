using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearcher.Model
{
    class NullException : Exception
    {
        public string ErrorMessage { get; set; }

        public NullException(string msg): base(msg)
        {
            ErrorMessage = msg;
        }
    }
}
