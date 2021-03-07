using Microsoft.AspNetCore.Mvc;
using shopAZ_API.Interfaces;
using shopAZ_API.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.Helpers
{
    public class Payment : IPayment
    {
        public async Task<bool> MakePayment(CardData model,long value)
        {
            try
            {
                var tokenopt = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = model.Number,
                        ExpMonth = model.ExpMonth,
                        ExpYear = model.ExpYear,
                        Cvc = model.Cvc
                    }
                };
                var serviceToken = new TokenService();
                Token stripeToken = await serviceToken.CreateAsync(tokenopt);
                var chargeopt = new ChargeCreateOptions
                {
                    Amount = value,
                    //en az 50 cent lazim oldugu ucun usd etdim
                    Currency = "usd",
                    Description = "test",
                    Source = stripeToken.Id    
                };
                var service = new ChargeService();
                Charge charge = await service.CreateAsync(chargeopt);
                if (!charge.Paid)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            
           
            return true;
        }
    }
}
