using Core.Models;
using Core.Repositories;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly string _connectionString;

        public CartRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Cart WHERE CartId = @CartId";
                var parameters = new { CartId = cartId };
                var cart = await connection.QuerySingleOrDefaultAsync<Cart>(query, parameters);

                if (cart != null)
                {
                    var cartItems = await GetCartItemsAsync(cartId);
                    cart.SetCartItems(cartItems);
                }

                return cart;
            }
        }



        private async Task<List<CartItem>> GetCartItemsAsync(string cartId)
        {
            List<CartItem> cartItems = new List<CartItem>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM CartItems WHERE CartId = @cartId", connection);
                command.Parameters.AddWithValue("@cartId", cartId);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    CartItem item = new CartItem();
                    item.SetCartId(reader.GetString(0));
                    item.SetProduct(await GetProductByIdAsync(reader.GetInt32(1)));
                    cartItems.Add(item);
                }
            }

            return cartItems;
        }


        private async Task<Product> GetProductByIdAsync(int productId)
        {
            Product product = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", productId);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    product = new Product(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.IsDBNull(2) ? null : reader.GetString(2),
                        reader.IsDBNull(3) ? null : reader.GetString(3),
                        reader.GetDecimal(4),
                        reader.IsDBNull(5) ? null : reader.GetString(5)
                    );
                }
            }

            return product;
        }

        public Task CreateAsync(Cart cart)
        {
            // Implementatie voor het creëren van een nieuwe cart
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Cart cart)
        {
            // Implementatie voor het bijwerken van een bestaande cart
            throw new NotImplementedException();
        }
    }
}
