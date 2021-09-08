using System.Data;
using groupme.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace groupme.Repositories
{
  public class AccountsRepository
  {
    private readonly IDbConnection _db;

    public AccountsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Account GetByEmail(string userEmail)
    {
      string sql = "SELECT * FROM accounts WHERE email = @userEmail";
      return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
    }

    internal Account GetById(string id)
    {
      string sql = "SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Account>(sql, new { id });
    }

    internal List<GroupMemberProfileViewModel> GetGroupMembers(int groupId)
    {
      string sql = @"
      SELECT
        a.*,
        gm.id AS groupMemberId
      FROM groupMembers gm
      JOIN accounts a ON gm.accountId = a.id
      WHERE gm.groupId = @groupId;
      ";
      return _db.Query<GroupMemberProfileViewModel>(sql, new { groupId }).ToList();
    }

    internal Account Create(Account newAccount)
    {
      string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
      _db.Execute(sql, newAccount);
      return newAccount;
    }

    internal Account Edit(Account update)
    {
      string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
      _db.Execute(sql, update);
      return update;
    }
  }
}
