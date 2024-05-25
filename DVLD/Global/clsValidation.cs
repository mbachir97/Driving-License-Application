﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVLD.Global
{
    internal class clsValidation
    {

        public static bool ValidateEmail(string EmailAddress)
        {
            var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(EmailAddress);
        }

        public static bool ValidateInteger(string Number)
        {
            var pattern = @"^[0-9]*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool ValidateFloat(string Number) {
        
             var Patren = @"^[0-9]*(?:\.[0-9]*)?$";


            var regex = new Regex(Patren);

            return regex.IsMatch(Number);


        }

        public static bool IsNumber(string Number) { 
        
            return ValidateInteger(Number)  || ValidateFloat(Number);
        }


    }
}
