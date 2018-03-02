using MyProfileTrail.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyProfileTrail.FutureSparkUtility
{
    public class CustomerDAL
    {
        public static bool IsCustomer(DbSet<Customer> cusSet, LoginModel checkCus, out string cusName)
        {
            bool result = false;
            cusName = string.Empty;
            foreach (Customer cus in cusSet)
            {
                if (cus.Email.Equals(checkCus.Email, StringComparison.InvariantCultureIgnoreCase)
                 && AesEncryptamajig.Decrypt(cus.Password, AesEncryptamajig.getKey()).Equals(checkCus.Password, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = true;
                    cusName = string.Join(" ", cus.CFirstName, cus.CLastName);
                    break;
                }
            }

            return result;
        }
    }
}