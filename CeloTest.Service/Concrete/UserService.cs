using CeloTest.Domain;
using CeloTest.Repo;
using CeloTest.Service.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeloTest.Service
{
    /// <summary>
    /// Business logics should happen here
    /// </summary>
    public class UserService : IUserService
    {
        private IUserRepo _repo;
        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }
        public async Task Delete(User user)
        {
            await _repo.Delete(user);
        }

        public async Task<IList<User>> Filter(string firstName, string lastName, string email, string phoneNumber, string dateOfBirth, int? take, int? skip)
        {
            var searchCriteria = ConvertSearchParameter(firstName, lastName, email, phoneNumber, dateOfBirth, take, skip);

            var result = await _repo.Get(u => u.IsMatch(searchCriteria), take, skip);

            return result.ToList();
        }

        public async Task<IList<User>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<User> GetSingle(string firstName, string lastName, string email, string phoneNumber, string dateOfBirth, int? take, int? skip)
        {
            var searchCriteria = ConvertSearchParameter(firstName, lastName, email, phoneNumber, dateOfBirth, take, skip);

            var result = await _repo.Get(u => u.IsMatch(searchCriteria), take, skip);

            return result.SingleOrDefault();
        }

        public async Task Update(User user)
        {
            await _repo.Update(user);
        }

        private UserSearchCriteria ConvertSearchParameter(string firstName, string lastName, string email, string phoneNumber, string dateOfBirth, int? take, int? skip)
        {
            DateTime dob = new DateTime();
            bool dobValid = false;
            if (!string.IsNullOrEmpty(dateOfBirth))
            {
                dobValid = DateTime.TryParse(dateOfBirth, out dob);
                if (!dobValid)
                {
                    throw new ArgumentException("Invalid format for date of birth");
                }
            }

            return new UserSearchCriteria()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                DateOfBirth = dobValid ? dob : new DateTime?(),
                Take = take,
                Skip = skip
            };
        }
    }
}
