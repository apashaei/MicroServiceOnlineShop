namespace Microservice.Admin.FrontEnd.Utilities
{
    public class ReadImageSrc
    {
        public async Task<string> GetImageSrcAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            // Read the uploaded image into a byte array
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();

                // Convert byte array to Base64 string
                var base64String = Convert.ToBase64String(imageBytes);

                // Get the content type (MIME type)
                var contentType = imageFile.ContentType; // Example: "image/png" or "image/jpeg"

                // Create the image src (data URI format)
                var imageSrc = $"data:{contentType};base64,{base64String}";

                return imageSrc; // Return the image src to be used in an <img> tag
            }
        }
    }
}
