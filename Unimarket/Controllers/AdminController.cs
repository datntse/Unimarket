using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Schema;
using Unimarket.API.Helper;
using Unimarket.API.Helpers;
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
        private readonly IRoleService _iRoleService;
        private readonly IJwtTokenService _jwtTokenService;
        

        public AdminController(IJwtTokenService jwtTokenService, IUserService userService,
            ICurrentUserService currentUserService,
            IMapper mapper, IRoleService iroleService)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _iRoleService = iroleService;
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

        [HttpGet("getRole")]
        public async Task<IActionResult> GetAccounts()
        {
            var result = await _iRoleService.GetRole();
            var listRoles = result.Select(_ => new
            {
                _.Id,
                _.Name
            });
            return Ok(listRoles);
        }

        [HttpGet("getRoleBy/{id}")]
        public async Task<IActionResult> GetRoleById(String id)
        {
            var result = await _iRoleService.GetRoleById(id);
            if (result != null) { return Ok(result); }
            return BadRequest("Cannot found");
        }

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(String roleName)
        {
            var result = await _iRoleService.CreateRole(roleName);
            return Ok(result);
        }

        [HttpPut("updateRole/{id}")]
        public async Task<IActionResult> UpdateRole(String roleName, String id)
        {
            var result = await _iRoleService.UpdateRole(roleName, id);
            if (result > 0) return Ok();
            return BadRequest("Cannot update");
        }

        [HttpDelete("deleteRole")]
        public async Task<IActionResult> DeleteRole(String roleId)
        {
            var result = await _iRoleService.DeleteRole(roleId);
            return Ok(result);
        }

        [HttpGet("getUserRole")]
        public async Task<IActionResult> GetListUsers()
        {
            var result = await _iRoleService.GetListUsers();
            return Ok(result);

        }
        [HttpGet("getUserRole/{userId}")]
        public async Task<IActionResult> GetUserRole(String userId)
        {
            var result = await _iRoleService.GetUserRole(userId);
            if (result != null) return Ok(result);
            return BadRequest("Cannot found");
        }


        [HttpPost("addUserRole")]
        public async Task<IActionResult> AddRoleUser(List<string> roleNames, String userId)
        {
            var result = await _iRoleService.AddRoleUser(roleNames, userId);
            return Ok(result);
        }

        //[HttpPost("changeUserStatus")]
        //public async Task<IActionResult> EnalbleUser(String userId)
        //{
        //    return Ok(await _iRoleService.EnalbleUser(userId));
        //}

    }
}
