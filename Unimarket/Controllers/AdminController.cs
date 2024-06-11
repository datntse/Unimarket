using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Schema;
using Unimarket.API.Helper;
using Unimarket.API.Services;
using Unimarket.Core.Constants;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = AppRole.Admin)]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;
        

        public AdminController(IJwtTokenService jwtTokenService, IUserService userService, ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllUser([FromQuery] DefaultSearch defaultSearch)
        {
            var result = await _userService.GetAll();
            var data = result.Sort(string.IsNullOrEmpty(defaultSearch.sortBy) ? "UserName" : defaultSearch.sortBy
                      , defaultSearch.isAscending)
                      .ToPageList(defaultSearch.currentPage, defaultSearch.perPage).AsNoTracking().ToList();
            return Ok(new { total = result.ToList().Count, data, page = defaultSearch.currentPage });
        }
    }
}
