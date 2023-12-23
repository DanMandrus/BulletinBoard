using AutoMapper;
using BulletinBoard.BL.Users;
using BulletinBoard.BL.User.Entities;
using BulletinBoard.Service.Controllers.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace BulletinBoard.Service.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    //REST Api
    //get all Users GET: Users/
    //get User details GET: Users/{id}
    //create User POST: Users + body 
    //edit User info PUT: Users/{id} + body (all data)
    //fire User DELETE: Users/{id}
    //not a REST Api:
    //GET: Users/get-User-details
    //POST: Users/save-User
    private readonly IUsersProvider _UsersProvider;
    private readonly IUsersManager _UsersManager;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public UsersController(IUsersProvider UsersProvider, IUsersManager UsersManager, IMapper mapper,
        ILogger logger)
    {
        _UsersManager = UsersManager;
        _UsersProvider = UsersProvider;
        _mapper = mapper;
        _logger = logger;
    }

    [Authorize]
    [HttpGet] //Users/
    public IActionResult GetAllUsers()
    {
        var Users = _UsersProvider.GetUsers();
        return Ok(new UsersListResponse()
        {
            Users = Users.ToList()
        });
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("filter")] //Users/filter?filter.sex=1&filter.age=20
    public IActionResult GetFilteredUsers([FromQuery] UsersFilter filter)
    {
        var Users = _UsersProvider.GetUsers(_mapper.Map<UsersModelFilter>(filter));
        return Ok(new UsersListResponse()
        {
            Users = Users.ToList()
        });
    }

    [HttpGet]
    [Route("{id}")] //Users/{id}
    public IActionResult GetUserInfo([FromRoute] Guid id)
    {
        try
        {
            var User = _UsersProvider.GetUserInfo(id);
            return Ok(User);
        }
        catch (ArgumentException ex)
        {
            _logger.Error(ex.ToString()); //stack trace + message
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserRequest request) //automatic validation
    {
        try
        {
            var User = _UsersManager.CreateUser(_mapper.Map<CreateUserModel>(request));
            return Ok(User);
        }
        catch (ArgumentException ex)
        {
            _logger.Error(ex.ToString());
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateUserInfo([FromRoute] Guid id, UpdateUserRequest request)
    {
        //validator for request
        //UpdateUser(_mapper)
        //put into exception block
        //return UserModel
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        //try catch
        //delete User
        return Ok();
    }
}