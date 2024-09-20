using Resources.Models;

namespace Resources.Interfaces;

public interface IProductService<T, TResult> where T : class where TResult : class
{
    ResponseResult<TResult> CreateProduct(T product);
    ResponseResult<TResult> GetOneProduct(string id);
    ResponseResult<IEnumerable<TResult>> GetAllProducts();
    ResponseResult<TResult> UpdateProduct(string id, Product updatedProduct);
    ResponseResult<TResult> DeleteProduct(string id);


}