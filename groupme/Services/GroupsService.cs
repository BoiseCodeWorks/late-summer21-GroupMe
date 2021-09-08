using System;
using System.Collections.Generic;
using groupme.Models;
using groupme.Repositories;

namespace groupme.Services
{
  public class GroupsService
  {
    private readonly GroupsRepository _groupRepo;

    public GroupsService(GroupsRepository groupRepo)
    {
      _groupRepo = groupRepo;
    }

    internal List<Group> GetGroups()
    {
      return _groupRepo.GetAll();
    }

    internal Group CreateGroup(Group groupData)
    {
      var group = _groupRepo.Create(groupData);
      return group;
    }

    internal Group GetGroup(int id)
    {
      return _groupRepo.GetById(id);
    }


    internal Group EditGroup(Group groupData, string userId)
    {
      var group = _groupRepo.GetById(groupData.Id);
      if (group == null)
      {
        throw new Exception("nope bad id");
      }
      if (group.CreatorId != userId)
      {
        throw new Exception("nope wrong user NOT YOURS");
      }
      group.Name = groupData.Name ?? group.Name;
      group.Img = groupData.Img ?? group.Img;
      group.Description = groupData.Description ?? group.Description;

      return _groupRepo.Update(group);
    }
  }
}