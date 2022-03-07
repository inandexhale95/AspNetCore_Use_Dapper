using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Myweb.Second.Models.Login;
using System;
using System.Data.SqlClient;

namespace Myweb.Second.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm]UserModel model)
        {

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromForm]UserModel model)
        {
            // 중복 검사 등등
            try
            {
                // Request.Form 을 사용해 패스워드 검증을 하는 방법..
                string password2 = Request.Form["password2"];

                if (model.Password != password2)
                {
                    throw new Exception("패스워드가 다릅니다.");
                }

                model.ConvertPassword();
                model.Register(_configuration);
                return Redirect("Login");
            } 
            catch (Exception e)
            {
                // 실패
                ModelState.AddModelError(String.Empty, $"{e.Message} 회원가입에 실패했습니다.");
                return View(model);
            }
        }
    }
}
