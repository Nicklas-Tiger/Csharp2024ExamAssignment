using Newtonsoft.Json;
using Resources.Interfaces;
using Resources.Models;


namespace Resources.Services;

public class ProductService : IProductService<Product, Product>
{
    private readonly IFileService _fileService;
    private List<Product> _products;

    public ProductService(IFileService fileService)
    {
        _fileService = fileService;
        _products = [];
        GetAllProducts();
    }

    //CREATE
    public ResponseResult<Product> CreateProduct(Product product)
    {
        if (product == null || string.IsNullOrEmpty(product.ProductId))
        {
            return new ResponseResult<Product> { Success = false, Message = "Invalid product information." };
        }

        try
        {
            if (!_products.Any(x => x.ProductId == product.ProductId))
            {
                _products.Add(product);
                var json = JsonConvert.SerializeObject(_products);
                var result = _fileService.SaveToFile(json);

                if (result.Success)
                    return new ResponseResult<Product> { Success = true, Message = "Product was added successfully!", Result = product };
                else
                    return new ResponseResult<Product> { Success = false, Message = "Product was not added successfully!" };
            }
                return new ResponseResult<Product> { Success = false, Message = "Product with the same ID already exists!" };
        }
        catch (Exception ex)
        {
            return new ResponseResult<Product> { Success = false, Message = ex.Message};

        }
    }
        //READ
    public ResponseResult<Product> GetOneProduct(string name)
    {
        try
        {
            var result = _fileService.GetFromFile();
            if (result.Success)
            {
                _products = JsonConvert.DeserializeObject<List<Product>>(result.Result!)!;
                var product = _products.FirstOrDefault(x => x.ProductName == name);
                if (product != null)
                {
                    return new ResponseResult<Product> { Success = true, Result = product };
                }
                else
                {
                    return new ResponseResult<Product> { Success = false, Message = "Product not found!" };
                }
            }
            else
                return new ResponseResult<Product> { Success = false, Message = result.Message };
        }
        catch (Exception ex)
        {
            return new ResponseResult<Product> { Success = false, Message = ex.Message };
        }
    }

    public ResponseResult<IEnumerable<Product>> GetAllProducts()
    {
        try
        {
            var result = _fileService.GetFromFile();
            if (result.Success)
            {
                _products = JsonConvert.DeserializeObject<List<Product>>(result.Result!)!;
                return new ResponseResult<IEnumerable<Product>> { Success = true };
            }
            else
                return new ResponseResult<IEnumerable<Product>> { Success = false, Message = result.Message };
        }
        catch (Exception ex)
        {
            return new ResponseResult<IEnumerable<Product>> { Success = false, Message = ex.Message };
        }
    }


        //UPDATE
    public ResponseResult<Product> UpdateProduct(string id, Product updatedProduct)
    {
        throw new NotImplementedException();
    }
        //DELETE
    public ResponseResult<Product> DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }
}
