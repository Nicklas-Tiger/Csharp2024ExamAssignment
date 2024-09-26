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
    }

    //CREATE
    public ResponseResult<Product> CreateProduct(Product product)
    {
        if (string.IsNullOrEmpty(product.ProductId))
        {
            return new ResponseResult<Product> { Success = false, Message = "Invalid product information." };
        }

        try
        {
            if (!_products.Any(x => x.ProductName == product.ProductName))
            {
                _products.Add(product);
                var json = JsonConvert.SerializeObject(_products);
                var result = _fileService.SaveToFile(json);

                if (result.Success)
                    return new ResponseResult<Product> { Success = true, Message = "Product was added successfully!\n", Result = product };
                else
                    return new ResponseResult<Product> { Success = false, Message = "Product was not added successfully!\n" };
            }
                return new ResponseResult<Product> { Success = false, Message = "Product with the same name already exists!\n" };
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
            var content = _fileService.GetFromFile();

            if (content.Success)
            {
                _products = JsonConvert.DeserializeObject<List<Product>>(content.Result!)!;
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
                return new ResponseResult<Product> { Success = false, Message = content.Message };
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
            var content = _fileService.GetFromFile();
            if (content.Success)
            {
                _products = JsonConvert.DeserializeObject<List<Product>>(content.Result!)!;
                return new ResponseResult<IEnumerable<Product>> { Success = true, Result = _products};
            }
            else
                return new ResponseResult<IEnumerable<Product>> { Success = false, Message = content.Message };
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
