using shopAZ_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.Interfaces
{
    interface IPayment
    {
        public Task<bool> MakePayment(CardData model,long value);
    }
}
