using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class HomeController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        public HomeController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public string Get()
        {
            userRepository.DeleteUser(4);
            return "Hello World!";
        }
    }
}