using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
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
        private IUserRepository _userRepository;


        public UserController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }


        //only authorized user can access
        //we need to tell API app to look for JWT instead of cookie
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetUser(int Id)
        {
            var userDetail = await _userRepository.GetById(Id);
            if (userDetail == null)
            {
                return NotFound(new { error = $"Movie Not Found for id: {Id}" });
            }
            return Ok(userDetail);
        }

    


      [HttpPost]
      [Route("purchase-movie")]
        public async Task<IActionResult> PurchaseMovies(PurchaseRequestModel purchaseRequest, int userId)
        {
           var moviePurchased = await _userService.PurchaseMovie(purchaseRequest, userId);
            if (!ModelState.IsValid)
            {
            return BadRequest();
            }
            if (moviePurchased == null) return BadRequest();
            return Ok(moviePurchased);
        }



         [HttpGet]
        [Route("purchases")]
         public async Task<IActionResult> Purchases()
         {
           return Ok();
         }
    }
 } 

