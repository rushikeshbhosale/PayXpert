using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert_Database_Connectivity.Exceptions
{
    public class InvalidInputException:Exception
    {
        public InvalidInputException()
        {
            
        }
        public InvalidInputException(string message) : base(message)
        { 

        }
    }
}
