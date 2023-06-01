using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Core.Models;
using Core.Repositories;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users", connection);
                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    users.Add(new User(
                        reader.GetString(0), // Id
                        reader.GetString(1), // Email
                        reader.GetString(2), // PasswordHash
                        reader.GetString(3), // FirstName
                        reader.GetString(4)  // LastName
                    ));
                }
            }

            return users;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    user = new User(
                        reader.GetString(0), // Id
                        reader.GetString(1), // Email
                        reader.GetString(2), // PasswordHash
                        reader.GetString(3), // FirstName
                        reader.GetString(4)  // LastName
                    );
                }
            }

            return user;
        }

        public async Task CreateAsync(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(
                    "INSERT INTO Users(Id, Email, PasswordHash, FirstName, LastName) VALUES (@id, @email, @password, @firstName, @lastName)",
                    connection);
                command.Parameters.AddWithValue("@id", user.Id);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@password", user.PasswordHash);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(
                    "UPDATE Users SET Email = @email, PasswordHash = @password, FirstName = @firstName, LastName = @lastName WHERE Id = @id",
                    connection);
                command.Parameters.AddWithValue("@id", user.Id);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@password", user.PasswordHash);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Users WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", user.Id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Email = @email", connection);
                command.Parameters.AddWithValue("@email", email);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    user = new User(
                        reader.GetString(0), // Id
                        reader.GetString(1), // Email
                        reader.GetString(2), // PasswordHash
                        reader.GetString(3), // FirstName
                        reader.GetString(4)  // LastName
                    );
                }
            }

            return user;
        }
    }
}
