using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyERP.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyERP.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly UserService _userService;

        public MetricsController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("retention/{days}")]
        public async Task<IActionResult> GetRollingRetention([FromRoute]int days)
        {
            return Ok(await _userService.CalculateRollingRetention(days));
        }

        [HttpGet("lifetime")]
        public async Task<IActionResult> GetUserLifeTimeDistribution()
        {
            return Ok(await _userService.GetUserLifeTimeDistribution());
        }
    }
}
