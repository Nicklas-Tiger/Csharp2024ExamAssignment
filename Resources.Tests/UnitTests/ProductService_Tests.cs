using Moq;
using Resources.Interfaces;
using Resources.Models;
using Resources.Services;



namespace Resources.Tests.UnitTests
{
    public class ProductService_Tests
    {
        private readonly Mock<IProductService<Product, Product>> _productServic = new();


        [Fact]
        public void CreateProduct__ShouldReturnSuccess_WhenProductIsCreated()
        {
            var product = new Product 
            {
                ProductId = Guid.NewGuid().ToString(),
                ProductName = "Test Product",
                Price = 100.0M,

                ProductCategory = new Category
                  {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Printers"
                  }
            };
            var expectedResponse = new ResponseResult<Product> { Success = true, Result = product, Message = "Contact was created successfully" };

            ProductService(productService => productService.CreateProduct(product)).Returns(expectedResponse);
            var contactService = _mockFileService.Object;

            // act
            var response = _productService.CreateProduct(product);

            // assert
            Assert.True(response.Success);
            Assert.Equal(product, response.Result);

        }

        [Fact]
        public void CreateProduct__ShouldReturnFalse_WhenProductIsInvalid()
        {
            // ARRANGE
            var invalidProduct = new Product
            {
                ProductId = null,
                ProductName = "Test Product",
                Price = 100.0M,

                ProductCategory = new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Printers"
                }
            };
            var productService = new ProductService(_mockFileService.Object);

            // act
            var response = productService.CreateProduct(invalidProduct);

            // assert
            Assert.False(response.Success);
            Assert.Equal("Invalid product information!", response.Message);
        }

        //[Fact]
        //public void CreateProduct_ShouldReturnSuccess_WhenProductIsValid()
        //{
        //    // Arrange
        //    var newProduct = new Product
        //    {
        //        ProductId = Guid.NewGuid().ToString(),
        //        ProductName = "Test Product",
        //        Price = 100.0M,

        //        ProductCategory = new Category
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            Name = "Printers"
        //        }
        //    };
        //    _mockFileService.Setup(fs => fs.SaveToFile(It.IsAny<string>()))
        //        .Returns(new ResponseResult<string> { Success = true });

        //    // Act
        //    var result = _productService.CreateProduct(newProduct);

        //    // Assert
        //    Assert.True(result.Success);
        //    Assert.Equal("Product was added successfully!", result.Message);

        //    // Verify that the SaveToFile method was called
        //    _mockFileService.Verify(fs => fs.SaveToFile(It.IsAny<string>()), Times.Once);
        //}

    }
}
    
