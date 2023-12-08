using FreeSql.DataAnnotations;

namespace sample_one.models
{


public class Bio{

    [Column(IsPrimary =true)]
    public long Id { get; set; }
    public string ImgUrl{ get; set; }=string.Empty;
    public string Description{ get; set; }=string.Empty;

    public string Website{ get; set; }=string.Empty;


}





}