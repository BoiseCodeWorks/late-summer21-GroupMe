using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using groupme.Models;
using groupme.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace groupme.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  [Authorize]
  public class GroupMembersController : ControllerBase
  {
    private readonly GroupMembersService _gms;

    public GroupMembersController(GroupMembersService gms)
    {
      _gms = gms;
    }


    [HttpPost]
    public async Task<ActionResult<GroupMember>> Create([FromBody] GroupMember newGM)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newGM.AccountId = userInfo.Id;
        GroupMember gm = _gms.Create(newGM);
        return Ok(gm);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        string resultStr = _gms.Delete(id, userInfo.Id);
        return Ok(resultStr);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

  }
}