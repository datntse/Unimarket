using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackagesController(IPackageService packageService)
        {
            _packageService = packageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await Task.FromResult(_packageService.GetAll());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePackage([FromBody] PackageCM package)
        {
            var result = await _packageService.CreatePackage(package);
            if (result.Succeeded)
            {
                return Ok("Package created successfully.");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePackage([FromBody] PackageUM updatedPackage)
        {
            var result = await _packageService.UpdatePackage(updatedPackage);
            if (result.Succeeded)
            {
                return Ok("Package updated successfully.");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(Guid id)
        {
            var result = await _packageService.DeletePackage(id);
            if (result.Succeeded)
            {
                return Ok("Package deleted successfully.");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
