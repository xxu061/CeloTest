using CeloTest.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CeloTest.Service
{
    public interface IUserService
    {
        Task<User> GetSingle(string firstName, string lastName, string email, string phoneNumber, string dateOfBirth, int? take, int? skip);
        Task<IList<User>> Filter(string firstName, string lastName, string email, string phoneNumber, string dateOfBirth, int? take, int? skip);
        Task<IList<User>> GetAll();
        Task Update(User user);
        Task Delele(User user);
    }
}
