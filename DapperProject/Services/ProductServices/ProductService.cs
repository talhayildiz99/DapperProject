using Dapper;
using DapperProject.Context;
using DapperProject.Dtos.ProductDots;
using Microsoft.CodeAnalysis;

namespace DapperProject.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly DapperContext _dapperContext;

        public ProductService(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            string query = "insert into TblProduct (Name,Stock,Price,CategoryId) values (@name,@stock,@price,@categoryId)";
            var parameters = new DynamicParameters();
            parameters.Add("@name", createProductDto.Name);
            parameters.Add("@stock", createProductDto.Stock);
            parameters.Add("@price", createProductDto.Price);
            parameters.Add("@categoryId", createProductDto.CategoryId);
            var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteProductAsync(int id)
        {
            string query = "Delete From TblProduct Where ProductId = @productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query,parameters);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = "Select * From TblProduct";
            var connection = _dapperContext.CreateConnection();
            var values = await connection.QueryAsync<ResultProductDto>(query);
            return values.ToList();
        }

        public async Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync()
        {
            string query = "SELECT dbo.TblProduct.ProductId, dbo.TblProduct.Name, dbo.TblProduct.Stock, dbo.TblProduct.Price, dbo.TblCategory.CategoryName FROM  dbo.TblCategory INNER JOIN dbo.TblProduct ON dbo.TblCategory.CategoryId = dbo.TblProduct.CategoryId";
            var connection = _dapperContext.CreateConnection();
            var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
            return values.ToList();
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(int id)
        {
            string query = "Select * From TblProduct Where ProductId = @productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            var connection = _dapperContext.CreateConnection();
            var values = await connection.QueryFirstOrDefaultAsync<GetByIdProductDto>(query,parameters);
            return values;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            string query = "Update TblProduct Set Name=@name, Stock=@stock, Price=@price, CategoryId=@categoryId Where ProductId = @productId";
            var parameters = new DynamicParameters();
            parameters.Add("@name", updateProductDto.Name);
            parameters.Add("@stock", updateProductDto.Stock);
            parameters.Add("@price", updateProductDto.Price);
            parameters.Add("@categoryId", updateProductDto.CategoryId);
            parameters.Add("@productId", updateProductDto.ProductId);
            var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
