using CoreApiWithImage.Models.Domain;

namespace CoreApiWithImage.Repository.Abstract
{
    public interface IProductRepository
    {
        bool Add(Product model);
    }
}
