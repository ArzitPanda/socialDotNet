namespace sample_one.utility
{

public  interface IFileUploader
{

    public  Task<string> UploadImage(IFormFile file);



}


    
}