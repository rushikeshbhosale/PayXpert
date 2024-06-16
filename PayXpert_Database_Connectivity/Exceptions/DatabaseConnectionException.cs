using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert_Database_Connectivity.Exceptions
{
    public class DatabaseConnectionException:Exception
    {
        public DatabaseConnectionException()
        {
            
        }
        public DatabaseConnectionException(string message) : base(message)
        { 

        }
    }
}

