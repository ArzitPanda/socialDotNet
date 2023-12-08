using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using sample_one.models.models;

namespace sample_one.models{



public class User
{       
    [Column(IsIdentity =true,IsPrimary =true)]
    public long Id { get; set; }

    public string UserName { get; set; } =string.Empty;
    public string LastName { get; set; } =string.Empty;
    public string FirstName { get; set; } =string.Empty;
    public string Email { get; set; } =string.Empty;
    
    public UserPass Passcode { get; set; } = new UserPass();
    
    public Bio Bio { get; set; } = new Bio();

      [Column(IsIgnore = true)]
      [Navigate(ManyToMany = typeof(Friend))]
       [JsonIgnore]
      public List<Friend> Friends { get; set; } =new List<Friend>();




}



}