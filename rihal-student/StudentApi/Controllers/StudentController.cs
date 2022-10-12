using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StudentApi.Core.Services;
using StudentApi.Models;
using StudentApi.Services;
using StudentApi.Core.Services.Auth;

namespace StudentApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{

    private readonly ILogger<StudentController> _logger;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IStudentService _studentservice;
    private readonly IConfiguration _config;
    private string generatedToken = null;

    public StudentController(
        ILogger<StudentController> logger, IConfiguration config, ITokenService tokenService, IUserService userService, IStudentService studentservice
    )
    {
        _logger = logger;
        _config = config;
        _tokenService = tokenService;
        _userService = userService;
        _studentservice = studentservice;
    }

    //[Route("Admins")]
    //[Authorize(Roles = "Admin")]
    [HttpGet(Name = "name")]
    public async Task<ActionResult<MyType[]>> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new MyType
        {
            name = Random.Shared.Next(-20, 55).ToString()
        }).ToArray();
    }


    public class MyType
    {
        public string name { get; set; }
    }

}

