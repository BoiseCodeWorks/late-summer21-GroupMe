using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using groupme.Models;

namespace groupme.Repositories
{
  public class GroupsRepository
  {
    private readonly IDbConnection _db;

    public GroupsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Group Create(Group data)
    {
      var sql = @"
            INSERT INTO groups(name, description, img, creatorId)
            VALUES(@Name, @Description, @Img, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public List<Group> GetAll()
    {
      string sql = @"
                SELECT 
                    g.*,
                    a.*
                FROM groups g
                JOIN accounts a ON g.creatorId = a.id;
            ";

      // [{g:Group, p: profile}].map(({g,p}) => g.creator = p)
      return _db.Query<Group, Profile, Group>(sql, (g, p) =>
      {
        g.Creator = p;
        return g;
      }, splitOn: "id").ToList();
    }

    public Group GetById(int id)
    {
      string sql = @"
                SELECT 
                    g.*,
                    a.*
                FROM groups g
                JOIN accounts a ON g.creatorId = a.id
                WHERE g.id = @id;
            ";

      return _db.Query<Group, Profile, Group>(sql, (g, p) =>
        {
          g.Creator = p;
          return g;
        }, new { id }).FirstOrDefault();
    }

    public Group Update(Group data)
    {
      var sql = @"
                UPDATE groups
                    SET
                    name = @Name,
                    img = @Img,
                    description = @Description
                WHERE id = @Id
                LIMIT 1;
            ";
      _db.Execute(sql, data);
      return GetById(data.Id);
    }
  }
}