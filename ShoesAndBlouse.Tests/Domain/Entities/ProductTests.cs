using System.ComponentModel.DataAnnotations;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Tests.Domain.Entities
{
    public class ProductTests
    {
        [Fact]
        public void Product_With_Valid_Data_Should_Be_Valid()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Description",
                Price = new Money("zl", 299.99m),
                Categories = [new Category { Id = 1, Name = "Test Category" }],
                PhotoPath = "/path/to/photo.jpg"
            };

            // Act
            var validationContext = new ValidationContext(product);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, validationContext, validationResult, true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(validationResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Product_With_Invalid_Name_Should_Be_Invalid(string invalidName)
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = invalidName,
                Description = "Test Description",
                Price = new Money("zl", 299.99m),
                Categories = [new Category { Id = 1, Name = "Test Category" }],
                PhotoPath = "/path/to/photo.jpg"
            };

            // Act
            var validationContext = new ValidationContext(product);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, validationContext, validationResult, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(validationResult);
        }
    }
}
