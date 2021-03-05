using shopAZ_API.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.Interfaces
{
   public interface IJwtGenerator
    {
        public string CreateToken(User user);
    }
}
