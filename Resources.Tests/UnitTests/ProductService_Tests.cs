using Moq;
using Newtonsoft.Json;
using Resources.Interfaces;
using Resources.Models;
using Resources.Services;



namespace Resources.Tests.UnitTests
{
    public class ProductService_Tests
    {
        private readonly Mock<IProductService<Product, Product>> _mockproductService = new();


        [Fact]
        public void CreateProduct__ShouldReturnSuccess_WhenProductIsCreated()
        {
                // Arrange
                var mockFileService = new Mock<IFileService>();
                mockFileService.Setup(x => x.SaveToFile(It.IsAny<string>()))
                               .Returns(new ResponseResult<string> { Success = true });

                var productService = new ProductService(mockFileService.Object);
                var product = new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = "C280",
                    Price = 100,
                    ProductCategory = new Category
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Skrivare"
                    }
                };

                // Act
                var result = productService.CreateProduct(product);

                // Assert
                Assert.True(result.Success);
                Assert.Equal("Product was added successfully!", result.Message);
                Assert.Equal(product.ProductName, result.Result!.ProductName);
            
        }



        [Fact]
        public void CreateProduct__ShouldReturnFalse_WhenProductIsDuplicate()
        {
        }




    }
}
    
