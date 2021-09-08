using System;
using System.Threading.Tasks;
using groupme.Models;
using groupme.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace groupme.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Authorize]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly GroupsService _gs;

    public AccountController(AccountService accountService, GroupsService gs)
    {
      _accountService = accountService;
      _gs = gs;
    }

    [HttpGet]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("groups")]
    public async Task<ActionResult<List<GroupMemberViewModel>>> GetGroups()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<GroupMemberViewModel> groups = _gs.GetGroupsForAccount(userInfo.Id);
        return Ok(groups);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}