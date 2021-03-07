using shopAZ_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.Models
{
    public class OrderAmount 
    {
        public static decimal? GetTotal(float discount,float price,int count, bool Ismoney)
        {
            decimal? amount = 0;
            if (!Ismoney)
            {
                amount += (decimal)(price - (price * (discount / 100))*count);

            }
            else
            {
                amount += (decimal)(price -discount)*count;
            }
            return amount;
        }
    }
}
