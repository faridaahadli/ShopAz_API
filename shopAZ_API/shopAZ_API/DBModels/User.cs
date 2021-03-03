using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopAZ_API.DBModels
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public virtual IEnumerable<UserRolePivot> UserRolePivots { get; set; }
        public virtual IEnumerable<Basket> Baskets { get; set; }
    }
}
