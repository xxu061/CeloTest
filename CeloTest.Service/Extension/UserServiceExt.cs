using CeloTest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CeloTest.Service.Extension
{
    public static class UserServiceExt
    {
        public static bool IsMatch(this User user, UserSearchCriteria searchCriteria)
        {
            var result = false;
            if (!string.IsNullOrEmpty(searchCriteria.FirstName))
            {
                result = user.FirstName.ToLower() == searchCriteria.FirstName.ToLower();
            }

            if (!string.IsNullOrEmpty(searchCriteria.LastName))
            {
                result = user.LastName.ToLower() == searchCriteria.LastName.ToLower();
            }

            if (searchCriteria.DateOfBirth != null)
            {
                result = user.DateOfBirth == searchCriteria.DateOfBirth;
            }

            if (!string.IsNullOrEmpty(searchCriteria.Email))
            {
                result = user.Email.ToLower() == searchCriteria.Email.ToLower();
            }

            if (!string.IsNullOrEmpty(searchCriteria.PhoneNumber))
            {
                result = user.PhoneNumber == searchCriteria.PhoneNumber;
            }

            return result;
        }
    }
}
