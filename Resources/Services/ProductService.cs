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
        _products = new List<Product>();
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
            GetProductsFromFile();

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

    public ResponseResult<List<Product>> GetProductsFromFile()
    {
        var content = _fileService.GetFromFile();
        if (content.Success)
        {
            try
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(content.Result!)!;
                return new ResponseResult<List<Product>> { Success = true, Result = products };
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<Product>> { Success = false, Message = ex.Message };
            }
        }
        else
        {
            return new ResponseResult<List<Product>> { Success = false, Message = content.Message };
        }
    }   

    public ResponseResult<IEnumerable<Product>> GetAllProducts()
    {
        GetProductsFromFile();

        var response = GetProductsFromFile();
        if (response.Success)
        {
            return new ResponseResult<IEnumerable<Product>> { Success = true, Result = response.Result };
        }
        else
        {
            return new ResponseResult<IEnumerable<Product>> { Success = false, Message = response.Message };
        }
    }
    //UPDATE
    public ResponseResult<Product> UpdateProduct(string id, Product updatedProduct)
    {
        var response = GetProductsFromFile();
        if (response.Success)
        {
            var existingProduct = response.Result!.FirstOrDefault(x => x.ProductId == id);
            if (existingProduct != null)
            {
                existingProduct.ProductName = updatedProduct.ProductName;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.ProductCategory = updatedProduct.ProductCategory;
                existingProduct.ProductDescription = updatedProduct.ProductDescription;

                var json = JsonConvert.SerializeObject(response.Result);
                var result = _fileService.SaveToFile(json);

                if (result.Success)
                {
                    return new ResponseResult<Product> { Success = true, Message = "Product updated successfully", Result = existingProduct };
                }
                else
                {
                    return new ResponseResult<Product> { Success = false, Message = "Failed to update product." };
                }
            }
            else
            {
                return new ResponseResult<Product> { Success = false, Message = "Product not found." };
            }
        }
        else
        {
            return new ResponseResult<Product> { Success = false, Message = response.Message };
        }
    }
    //DELETE
    public ResponseResult<Product> DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }

}
