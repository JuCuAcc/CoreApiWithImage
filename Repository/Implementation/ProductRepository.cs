using CoreApiWithImage.Models.Domain;
using CoreApiWithImage.Repository.Abstract;

namespace CoreApiWithImage.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
		private readonly DatabaseContext _context;

		public ProductRepository(DatabaseContext context)
		{
			this._context = context;

        }
        public bool Add(Product model)
        {
			try
			{
				_context.Product.Add(model);
				_context.SaveChanges();
				return true;

			}
			catch (Exception)
			{
				return false;
			}
        }
    }
}
