namespace groupme.Models
{
  public class Profile
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }

  }

  public class GroupMemberProfileViewModel : Profile
  {
    public int GroupMemberId { get; set; }
  }

}