using Microsoft.AspNetCore.Mvc;
using sk.Core.Exceptions;
using sk.Core.Interfaces;
using sk.Core.Models.UserModels;
using System.Threading.Tasks;

namespace sk.Modules.AccessManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IAuthService _authService;
        private readonly IDataBase<User> _db;
        public AuthController(IDataBase<User> db)
        {
            _db= db;
        }

        ///public async Task<IActionResult> Login(LoginRequest loginRequest)
        ///{
        ///    bool flag = await _db.IsExist(loginRequest);
        ///    if (!flag)
        ///    {
        ///        throw new UserNotFoundException();
        ///    }

        ///    return Ok();
        ///}
    }
}


