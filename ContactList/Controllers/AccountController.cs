using System.Web.Http;
using System.Web.Http.Results;
using DrinkAPI.Data.Account;

namespace DrinkAPI.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        [HttpPost]
        public void Login(string email, string password)
        {
            
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("~/account/register")]
        public JsonResult<string> Register(string email, string password, string firstName, string lastName)
        {
            var userService = new UserService();
            var existingUser = userService.GetUser(email);

            if (existingUser != null)
            {
                return Json("");
            }

            return null;
        }
    }
}
