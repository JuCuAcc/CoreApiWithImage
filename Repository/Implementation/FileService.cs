using CoreApiWithImage.Repository.Abstract;
using System.IO;
namespace CoreApiWithImage.Repository.Implementation
{
    public class FileService : IFileService
    {
        private IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment env)
        {
            this.environment = env;
        }
        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwwPath = this.environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads\\", imageFileName);
                if (File.Exists(path)) // using System.IO.File;
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Tuple<int, string> SaveImage(IFormFile imageFIle)
        {
            try
            {
                var contentPath = this.environment.ContentRootPath;
                var path = Path.Combine(contentPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                
                // Check the allowed extensions
                var ext = Path.GetExtension(imageFIle.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg", ".pdf", ".doc"}; // .pdf & .doc is for other file type
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }

                string uniqueString = Guid.NewGuid().ToString();
                // Creating unique file name
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFIle.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception)
            {

                return new Tuple<int, string>(0, "Error occured.");
            }
        }
    }
}
