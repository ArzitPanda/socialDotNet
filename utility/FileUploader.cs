
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
// using Microsoft.VisualBasic;

namespace sample_one.utility
{

    public class FileUploader : IFileUploader
    {   


     
        

       


        public  string UploadImage(IFormFile file)
        {

            // Account account = new Account("dotnet_sample", "566762456861656", "vYNXOkanqyBMFdbAKAjYpcSduzw");
            Account account = new Account { Cloud="dotnet_sample",ApiSecret="vYNXOkanqyBMFdbAKAjYpcSduzw",ApiKey="566762456861656"};
              Cloudinary _cloudinary = new Cloudinary(account);


            if (file == null || file.Length == 0)
        {
            throw new  Exception("No file uploaded");
        }

        // Upload and compress the image
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Transformation = new Transformation().Quality("auto:low").FetchFormat("auto")
        };

        var uploadResult = _cloudinary.Upload(uploadParams);
        Console.WriteLine("line no");

        // Get the public URL of the uploaded image
        var imageUrl = uploadResult.Url.ToString();
        return imageUrl;
        }
    }




}