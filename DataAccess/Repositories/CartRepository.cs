﻿using Core.Models;
using Core.Repositories;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

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
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Cart WHERE CartId = @CartId";
            var parameters = new { CartId = cartId };
            var cart = await connection.QuerySingleOrDefaultAsync<Cart>(query, parameters);

            if (cart != null)
            {
                var cartItems = await GetCartItemsAsync(cartId);
                cart.SetCartItems(cartItems);
            }
            else
            {
                // No cart found for the provided CartId, create a new empty one
                cart = new Cart(cartId, new List<CartItem>());
            }

            return cart;
        }


        private async Task<List<CartItem>> GetCartItemsAsync(string cartId)
        {
            var cartItems = new List<CartItem>();

            using var connection = new SqlConnection(_connectionString);
            var query = @"SELECT CartItems.*, Products.* 
                  FROM CartItems 
                  INNER JOIN Products ON CartItems.ProductId = Products.Id 
                  WHERE CartItems.CartId = @CartId";
            var parameters = new { CartId = cartId };

            var result = await connection.QueryAsync<CartItem, Product, CartItem>(
                query,
                (cartItem, product) =>
                {
                    cartItem.SetProduct(product);
                    return cartItem;
                },
                parameters,
                splitOn: "Id");

            cartItems = result.ToList();

            return cartItems;
        }


        private async Task<Product> GetProductByIdAsync(int productId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Products WHERE Id = @ProductId";
            var parameters = new { ProductId = productId };

            using var reader = await connection.ExecuteReaderAsync(query, parameters);
            if (await reader.ReadAsync())
            {
                return new Product(
                    reader.GetInt32(reader.GetOrdinal("Id")),
                    reader.GetString(reader.GetOrdinal("Name")),
                    reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                    reader.IsDBNull(reader.GetOrdinal("Category")) ? null : reader.GetString(reader.GetOrdinal("Category")),
                    reader.GetDecimal(reader.GetOrdinal("Price")),
                    reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("ImageUrl"))
                );
            }

            return null;
        }

        public async Task CreateAsync(Cart cart)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "INSERT INTO Cart (CartId) VALUES (@CartId)";
            var parameters = new { CartId = cart.CartId };
            await connection.ExecuteAsync(query, parameters);

            foreach (var cartItem in cart.CartItems)
            {
                await AddCartItemAsync(connection, cart.CartId, cartItem);
            }
        }

        public async Task UpdateAsync(Cart cart)
        {
            using var connection = new SqlConnection(_connectionString);

            await RemoveAllCartItemsAsync(connection, cart.CartId);

            foreach (var cartItem in cart.CartItems)
            {
                await AddCartItemAsync(connection, cart.CartId, cartItem);
            }
        }

        public static async Task AddCartItemAsync(SqlConnection connection, string cartId, CartItem cartItem)
        {
            var query = "INSERT INTO CartItems (CartId, ProductId) VALUES (@CartId, @ProductId)";
            var parameters = new { CartId = cartId, ProductId = cartItem.Product.Id };
            await connection.ExecuteAsync(query, parameters);
        }

        public static async Task RemoveAllCartItemsAsync(SqlConnection connection, string cartId)
        {
            var query = "DELETE FROM CartItems WHERE CartId = @CartId";
            var parameters = new { CartId = cartId };
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
