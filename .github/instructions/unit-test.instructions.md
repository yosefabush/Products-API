---
applyTo: '**/*Tests.cs'
---
# Unit Test Instructions for .NET 8 Web API

> Generated by Copilot

## Frameworks & Tools
- Use **xUnit** for all unit tests.
- Use **Moq** for mocking dependencies.

## General Guidelines
- Each test class should target a single class (e.g., a controller, service, or repository).
- Name test classes as `{ClassName}Tests`.
- Name test methods using the pattern: `{MethodName}_Should_{ExpectedBehavior}_When_{Condition}`.
- Use the `[Fact]` attribute for single-scenario tests and `[Theory]` with `[InlineData]` for parameterized tests.
- Arrange-Act-Assert (AAA) pattern must be followed in all tests.
- Mock all dependencies using Moq and inject them via constructor.
- Do not test external dependencies (e.g., database, file system, HTTP calls) directly—mock them.
- Use dependency injection for all services and repositories in tests, mirroring production code.

## Example Structure

```csharp
public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task GetProductById_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var productId = 1;
        var expectedProduct = new Product { Id = productId, Name = "Test" };
        _productRepositoryMock.Setup(r => r.GetByIdAsync(productId))
            .ReturnsAsync(expectedProduct);

        // Act
        var result = await _productService.GetProductByIdAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedProduct.Id, result.Id);
    }
}
```

## Best Practices
- Keep tests isolated and independent.
- Use meaningful test data.
- Assert only one logical outcome per test.
- Use `async`/`await` for all asynchronous operations.
- Add the comment `Generated by Copilot` at the top of each test file.

## Directory Structure
- Place all test files in the corresponding `Tests` project/folder.
- Mirror the namespace and folder structure of the main project.