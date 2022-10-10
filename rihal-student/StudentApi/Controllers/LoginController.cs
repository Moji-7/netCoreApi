using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StudentApi.Core.Services;
using StudentApi.Models;
using StudentApi.Services;
using StudentApi.Core.Services.Auth;

namespace StudentApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{

    private readonly ILogger<StudentController> _logger;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _config;
    private string generatedToken = null;

    public LoginController(
        ILogger<StudentController> logger, IConfiguration config, ITokenService tokenService, IUserService userService)
    {
        _logger = logger;
        _config = config;
        _tokenService = tokenService;
        _userService = userService;
    }



    [AllowAnonymous]
    [Route("login")]
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] User userModel)
    //ActionResult Login([FromBody] UserLogin userLogin)
    {
        if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
        {
            return (BadRequest());
        }
        IActionResult response = Unauthorized();
        var validUser = await GetUser(userModel);
        //return validUser;
        if (validUser != null)
        {
            generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), validUser);


            if (generatedToken != null)
            {
                HttpContext.Session.SetString("Token", generatedToken);
                return Ok(generatedToken);
            }
            else
            {
                return (BadRequest("Error"));
            }
        }
        else
        {
            return (BadRequest("Error"));
        }
    }

    private async Task<UserDTO> GetUser(User userModel)
    {
        // Write your code here to authenticate the user     
        // return _studentservice.GetById(userModel);
        return await _userService.GetUser(userModel);

    }

    // [Authorize]
    // [Route("mainwindow")]
    // [HttpGet]
    // public IActionResult MainWindow()
    // {
    //     string token = HttpContext.Session.GetString("Token");
    //     if (token == null)
    //     {
    //         return (RedirectToAction("Index"));
    //     }
    //     if (!_tokenService.IsTokenValid(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), token))
    //     {
    //         return (RedirectToAction("Index"));
    //     }
    //     ViewBag.Message = BuildMessage(token, 50);
    //     return View();
    // }

    // public IActionResult Error()
    // {
    //   //  ViewBag.Message = "An error occured...";
    //    // return View();
    // }

    private string BuildMessage(string stringToSplit, int chunkSize)
    {
        var data = Enumerable.Range(0, stringToSplit.Length / chunkSize).Select(i => stringToSplit.Substring(i * chunkSize, chunkSize));
        string result = "The generated token is:";
        foreach (string str in data)
        {
            result += Environment.NewLine + str;
        }
        return result;
    }
}

