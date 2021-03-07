using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.Models
{
    public class CardData
    {
      /*{
  "number": "4242424242424242",
  "expMonth": 7,
  "expYear": 2021,
  "cvc": "123"
       }*/
        public string Number { get; set; }
        public long ExpMonth { get; set; }
        public long ExpYear { get; set; }
        public string Cvc { get; set; }
    }
}
