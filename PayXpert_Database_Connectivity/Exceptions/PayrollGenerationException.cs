using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert_Database_Connectivity.Exceptions
{
    public class PayrollGenerationException:Exception
    {
        public PayrollGenerationException()
        {
            
        }
        public PayrollGenerationException(string message) : base(message) 
        {

        }
    }
}
