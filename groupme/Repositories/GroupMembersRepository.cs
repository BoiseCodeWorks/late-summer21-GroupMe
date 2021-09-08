using System.Data;
using Dapper;
using groupme.Models;

namespace groupme.Repositories
{
  public class GroupMembersRepository
  {
    private readonly IDbConnection _db;

    public GroupMembersRepository(IDbConnection db)
    {
      _db = db;
    }

    internal GroupMember Create(GroupMember newGM)
    {
      string sql = "INSERT INTO groupMembers (accountId, groupId) VALUES (@AccountId, @GroupId); SELECT LAST_INSERT_ID();";
      newGM.Id = _db.ExecuteScalar<int>(sql, newGM);
      return newGM;
    }

    internal GroupMember GetById(int id)
    {
      string sql = "SELECT * FROM groupMembers WHERE id = @id;";
      return _db.QueryFirstOrDefault<GroupMember>(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM groupMembers WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}