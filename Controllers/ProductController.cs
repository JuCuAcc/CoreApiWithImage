using CoreApiWithImage.Models.DTO;
using CoreApiWithImage.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApiWithImage.Models.Domain;

namespace CoreApiWithImage.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IProductRepository _productRepo;
        public ProductController(IFileService fs, IProductRepository productRepo)
        {
            this._fileService = fs;
            this._productRepo = productRepo;
        }
        [HttpPost]
        public IActionResult Add([FromForm]Product model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {

                status.StatusCode = 0;
                status.Message = "Please pass the valid data.";
                return Ok(status);
            }
            if (model.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1==1)
                {
                    model.ProductImage = fileResult.Item2; // Getting name of the image.
                }
                var productResult = _productRepo.Add(model);
                if (productResult)
                {
                    status.StatusCode = 1;
                    status.Message = "Added Successfully.";
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on adding product.";
                }
            }
            
            return Ok(status);
        }
    }
}
