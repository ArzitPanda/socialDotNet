
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
// using Microsoft.VisualBasic;

namespace sample_one.utility
{

    public class FileUploader : IFileUploader
    {
        public async Task<string> UploadImage(IFormFile file)
        {


            // Account account = new Account("dotnet_sample", "566762456861656", "vYNXOkanqyBMFdbAKAjYpcSduzw");
            Account account = new Account("djaj72xyk", "566762456861656", "vYNXOkanqyBMFdbAKAjYpcSduzw");
            Cloudinary _cloudinary = new Cloudinary(account);
            var uploadResult = new ImageUploadResult();

            if (file == null || file.Length == 0)
            {
                throw new Exception("No file uploaded");
            }

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Quality("auto:low").FetchFormat("auto")
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);




            }








            // Upload and compress the image
            // var uploadParams = new ImageUploadParams
            // {
            //     File = new FileDescription(file.FileName, file.OpenReadStream()),
            //     // Transformation = new Transformation().Quality("auto:low").FetchFormat("auto")
            // };
            var imageUrl = uploadResult.Uri.ToString();
            Console.WriteLine(imageUrl);
            return imageUrl;
        }
    }




}