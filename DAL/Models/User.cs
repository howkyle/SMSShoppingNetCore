using Microsoft.AspNetCore.Identity;

namespace DAL.Models
{
    
    public class User : IdentityUser
    {
        override public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }
        //public string Password { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
