using shopAZ_API.DBModels;
using shopAZ_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.Models
{
    public class UserProfile
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
