using CeloTest.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace CeloTest.Repo
{
    /// <summary>
    /// This class is not been used, but only to show the application structure and the ability to switch between data providers. 
    /// </summary>
    public class UserSQLRepo : IUserRepo, IDisposable
    {
        SQLContext _context = new SQLContext();
        public async Task Delete(User t)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == t.Id);
            if (user != null)
            {
                _context.Users.Remove(t);

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public async Task<IEnumerable<User>> Get(Func<User, bool> filter, int? take, int? skip)
        {
            var result = await Task.FromResult(_context.Users.Where(filter).AsQueryable());

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

        public async Task<IList<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task Update(User t)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == t.Id);
            if (user != null)
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NullReferenceException(); 
            }
        }

        public async Task Add(User t)
        {
            if (t != null)
            {
                await _context.Users.AddAsync(t);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
