using System;
using System.Collections.Generic;
using System.Text;

namespace CeloTest.Domain
{
    public class UserSearchCriteria
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }
    }
}
