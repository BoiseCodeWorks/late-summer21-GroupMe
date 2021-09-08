using System;
using groupme.Models;
using groupme.Repositories;

namespace groupme.Services
{
  public class GroupMembersService
  {
    private readonly GroupMembersRepository _gmr;
    private readonly GroupsRepository _gr;

    public GroupMembersService(GroupMembersRepository gmr, GroupsRepository gr)
    {
      _gmr = gmr;
      _gr = gr;
    }

    internal GroupMember Create(GroupMember newGM)
    {
      return _gmr.Create(newGM);
    }

    private GroupMember GetById(int id)
    {
      GroupMember found = _gmr.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal string Delete(int id, string accountId)
    {
      GroupMember gm = GetById(id);
      if (gm.AccountId == accountId)
      {
        _gmr.Delete(id);
        return "Successfully left group";
      }
      Group g = _gr.GetById(gm.GroupId);
      if (g.CreatorId == accountId)
      {
        _gmr.Delete(id);
        return "Member has been removed from group";
      }
      throw new Exception("Invalid Access To Remove This Member");
    }
  }
}