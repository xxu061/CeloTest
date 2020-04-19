using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CeloTest.Domain;
using CeloTest.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Route("User/GetSingle")]
        public async Task<User> GetSingle(string firstName, string lastName, string email, string phoneNumber, string dateOfBirth, int? take, int? skip)
        {
            return await _userService.GetSingle(firstName, lastName, email, phoneNumber, dateOfBirth, take, skip);
        }

        [HttpGet]
        [Route("User/GetAll")]
        public async Task<IList<User>> GetAll()
        {
            return await _userService.GetAll();
        }

        [HttpGet]
        [Route("User/Filter")]
        public async Task<IList<User>> Filter(string firstName, string lastName, string email, string phoneNumber, string dateOfBirth, int? take, int? skip)
        {
            return await _userService.Filter(firstName, lastName, email, phoneNumber, dateOfBirth, take, skip);
        }

        [HttpPatch]
        [Route("User/Update")]
        public async Task Update(User user)
        {
            if (user == null)
                throw new ArgumentException("Empty user payload");
            await _userService.Update(user);
        }

        [HttpDelete]
        [Route("User/Delete")]
        public async Task Delete(User user)
        {
            if (user == null)
                throw new ArgumentException("Empty user payload");
            await _userService.Delete(user);
        }
    }
}
