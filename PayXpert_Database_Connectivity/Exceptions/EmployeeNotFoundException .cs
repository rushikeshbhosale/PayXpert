﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert_Database_Connectivity.Exceptions
{
    public class EmployeeNotFoundException:Exception
    {
        public EmployeeNotFoundException()
        {
            
        }
        public EmployeeNotFoundException(string message): base(message)
        {

        }
    }
}
