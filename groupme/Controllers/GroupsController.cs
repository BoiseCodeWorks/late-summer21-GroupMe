
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using groupme.Models;
using groupme.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace groupme.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class GroupsController : ControllerBase
  {
    private readonly GroupsService _gs;
    private readonly AccountService _acts;

    public GroupsController(GroupsService gs, AccountService acts)
    {
      _gs = gs;
      _acts = acts;
    }

    [HttpGet]
    public ActionResult<List<Group>> Get()
    {
      try
      {
        List<Group> groups = _gs.GetGroups();
        return Ok(groups);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Group> GetOne(int id)
    {
      try
      {
        Group group = _gs.GetGroup(id);
        return Ok(group);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/members")]
    public ActionResult<List<GroupMemberProfileViewModel>> GetMembers(int id)
    {
      try
      {
        List<GroupMemberProfileViewModel> group = _acts.GetGroupMembers(id);
        return Ok(group);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Group>> Create([FromBody] Group groupData)
    {
      try
      {
        // what does this do?????
        // gets the bearer token from the request headers
        // asks auth0 if the bearer token is valid
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // NEVER EVER TRUST THE CLIENT
        groupData.CreatorId = userInfo.Id;

        var g = _gs.CreateGroup(groupData);
        return Ok(g);

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Group>> EditGroup([FromBody] Group groupData, int id)
    {
      try
      {
        var userInfo = await HttpContext.GetUserInfoAsync<Account>();
        groupData.Id = id;
        Group g = _gs.EditGroup(groupData, userInfo.Id);
        return Ok(g);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }

    }



  }
}