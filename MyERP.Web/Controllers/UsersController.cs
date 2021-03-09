using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyERP.Application.Exceptions;
using MyERP.Application.Services;
using MyERP.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyERP.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await _userService.GetAsync(id);
                return Ok(user);
            }
            catch (NotFoundException e)
            {
                return NotFound($"User with id {id} was not found in the system.");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto[] users)
        {
            await _userService.CreateOrUpdate(users);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserDto user)
        {
            await _userService.UpdateAsync(user.UserId, user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }
    }
}
