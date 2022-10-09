using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StudentApi.Core.Services;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers;

[ApiController]
[Route("student/[controller]")]
public class StudentController : ControllerBase
{

    private readonly ILogger<StudentController> _logger;
    private readonly ITokenService _tokenService;
    private readonly IStudentService _studentservice;
    private readonly IConfiguration _config;
    private string generatedToken = null;

    public StudentController(
        ILogger<StudentController> logger,
    IConfiguration config, ITokenService tokenService, IStudentService studentservice
    )
    {
        _logger = logger;
        _tokenService = tokenService;
        _studentservice = studentservice;
        _config = config;
    }

    [HttpGet(Name = "name")]
    public async Task<ActionResult<MyType[]>> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new MyType
        {
            name = Random.Shared.Next(-20, 55).ToString()

        })
        .ToArray();
    }
    public class MyType
    {
        public string name { get; set; }
    }


    [AllowAnonymous]
    [Route("login")]
    [HttpPost]
    public IActionResult Login(UserModel userModel)
    {
        if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
        {
            return (RedirectToAction("Error"));
        }
        IActionResult response = Unauthorized();
        var validUser = GetUser(userModel);

        if (validUser != null)
        {
            generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), validUser);
            if (generatedToken != null)
            {
                HttpContext.Session.SetString("Token", generatedToken);
                return RedirectToAction("MainWindow");
            }
            else
            {
                return (RedirectToAction("Error"));
            }
        }
        else
        {
            return (RedirectToAction("Error"));
        }
    }

    private UserDTO GetUser(UserModel userModel)
    {
        // Write your code here to authenticate the user     
        // return _studentservice.GetById(userModel);

        return new UserDTO { UserName = "ali" };
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

