using System.ComponentModel.DataAnnotations;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Tests.Domain.Entities
{
    public class CategoryTests
    {
        [Fact]
        public void Category_With_Valid_Data_Should_Be_Valid()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Test Category",
                Products = [new Product { Id = 1, Name = "Test Product", Description = "Test Description", Price = new Money("zl", 299.99m) }]
            };

            // Act
            var validationContext = new ValidationContext(category);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(category, validationContext, validationResult, true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(validationResult);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Category_With_Invalid_Name_Should_Be_Invalid(string invalidName)
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = invalidName,
                Products = [new Product { Id = 1, Name = "Test Product", Description = "Test Description", Price = new Money("zl", 299.99m) }]
            };

            // Act
            var validationContext = new ValidationContext(category);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(category, validationContext, validationResult, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(validationResult);
        }
    }
}