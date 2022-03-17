using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // only authorized user can access 
        // we need to tell API app to look for JWT instead of cookie
        //[HttpGet]
        //[Route("details")]
        //public async Task<IActionResult> GetUserDetailById(int id)
        //{
        //    var userDetail= _userService.

        //}
        
        
        
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> Purchases()
        {
            return Ok();
        }
    }

}