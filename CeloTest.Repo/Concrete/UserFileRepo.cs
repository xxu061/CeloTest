using CeloTest.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using CeloTest.Repo.Context;

namespace CeloTest.Repo
{
    public class UserFileRepo : IUserRepo
    {
        private List<User> _users;
        private IFileContext _fileLoader;
        public UserFileRepo(IFileContext fileLoader)
        {
            _fileLoader = fileLoader;
            _users = _fileLoader.Read();
        }
        public async Task Delete(User t)
        {
            await Task.Run(() =>
            {
                _users.Remove(t);
                _fileLoader.Write(_users);
            });
        }

        public async Task<IList<User>> GetAll()
        {
            return await Task.FromResult(_users);
        }

        public async Task<IEnumerable<User>> Get(Func<User, bool> filter, int? take = null, int? skip = null)
        {
            var result = await Task.FromResult(_users.Where(filter).AsQueryable());

            if (take.HasValue)
            {
                result = result.Take(take.Value);
            }

            if (skip.HasValue)
            {
                result = result.Skip(skip.Value);
            }

            return result;
        }

        public async Task Update(User t)
        {
            await Task.Run(() =>
            {
                _users.Remove(t);
                _users.Add(t);
                _fileLoader.Write(_users);

            });
        }

        public void Dispose()
        {
            _users = null;
        }
    }
}
