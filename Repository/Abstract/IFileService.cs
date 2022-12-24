namespace CoreApiWithImage.Repository.Abstract
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile imageFIle);
        public bool DeleteImage(string imageFileName);
    }
}
