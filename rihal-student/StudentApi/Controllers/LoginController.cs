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



    [HttpPost("Register")]
    public async Task<ActionResult<UserDTO>> Register([FromBody] User userRegister)
    {
        try
        {
            if (userRegister == null || !ModelState.IsValid)
                return BadRequest();
            UserDTO newUser = await _userService.GetUserByName(userRegister.UserName);
            //return newUser;
            if (newUser is UserDTO)
                return StatusCode(StatusCodes.Status409Conflict, "user exist");
            userRegister = await _userService.Insert(userRegister);
            return CreatedAtAction(nameof(GetUserById), new { id = userRegister.ID }, userRegister);
        }
        catch (System.Exception ex)
        {
            return BadRequest();
        }
    }

    [AllowAnonymous]
    [Route("login")]
    [HttpPost]
    public async Task<ActionResult<UserDTO>> Login([FromBody] UserDTO userModel)
    //ActionResult Login([FromBody] UserLogin userLogin)
    {
        if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
        {
            return (BadRequest());
        }
        var validUser = await _userService.GetUserByName(userModel.UserName);
        // return validUser;
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
    [HttpGet("{id:int}")] // GET /api/test2/int/3
    public async Task<ActionResult<UserDTO>> GetUserById(int id)
    {
        return await _userService.GetUserById(id);
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<List<User>>> GetAll()
    {
        try
        {
            return await _userService.GetAll();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
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

