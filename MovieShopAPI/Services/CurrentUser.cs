using System.Security.Claims;

namespace MovieShopAPI.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        public bool IsAuthenticated => _contextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public int UserId => Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public string Email => _contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Email).Value;

        public string FirstName => _contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.GivenName).Value;

        public string LastName => _contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Surname).Value;

        public bool IsAdmin => throw new NotImplementedException();

        public List<string> Roles => throw new NotImplementedException();

        public string IpAdress => _contextAccessor.HttpContext?.Connection.RemoteIpAddress.ToString();
    }
}
