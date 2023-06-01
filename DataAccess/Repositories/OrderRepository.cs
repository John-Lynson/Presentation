using Core.Models;
using Core.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Orders", connection);
                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Order order = CreateOrderFromReader(reader);
                    orders.Add(order);
                }
            }

            return orders;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            Order order = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    order = CreateOrderFromReader(reader);
                }
            }

            return order;
        }

        public async Task CreateAsync(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(
                    "INSERT INTO Orders (UserId, OrderDate) VALUES (@userId, @orderDate)",
                    connection);
                command.Parameters.AddWithValue("@userId", order.UserId);
                command.Parameters.AddWithValue("@orderDate", order.OrderDate);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(
                    "UPDATE Orders SET UserId = @userId, OrderDate = @orderDate WHERE Id = @id",
                    connection);
                command.Parameters.AddWithValue("@id", order.Id);
                command.Parameters.AddWithValue("@userId", order.UserId);
                command.Parameters.AddWithValue("@orderDate", order.OrderDate);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Orders WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", order.Id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Orders WHERE UserId = @userId", connection);
                command.Parameters.AddWithValue("@userId", userId);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Order order = CreateOrderFromReader(reader);
                    orders.Add(order);
                }
            }

            return orders;
        }

        private Order CreateOrderFromReader(SqlDataReader reader)
        {
            return new Order(
                id: reader.GetInt32(0),
                userId: reader.GetString(1),
                orderDate: reader.GetDateTime(2)
            );
        }
    }
}