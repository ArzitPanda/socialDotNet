using AutoMapper;
using sample_one.models;
using sample_one.models.dto;

namespace sample_one
{

public class AutoMapper: Profile {
  public AutoMapper() {
    CreateMap < User,UserResponseDto > ();
    CreateMap < UserRequestDto,User > ();
    CreateMap<User,LoginUserResponseDto > (); 
    CreateMap<PostRequestDto,Post> ();
    CreateMap<CommentRequestDto,Comment> ();
    CreateMap<SubCommentRequestDto,SubComment> ();
  }
}


    
    
}