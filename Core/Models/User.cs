using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class User
    {
        public string Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public User(string id, string email, string passwordHash, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
