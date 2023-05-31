﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? ImageUrl { get; private set; }
        public decimal Price { get; private set; }
        public string? Category { get; private set; }

        private Product() { }

        private Product(string name, string? description, string? imageUrl, decimal price, string? category)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            Category = category;
        }

        public class Builder
        {
            private string? _name;
            private string? _description;
            private string? _imageUrl;
            private decimal? _price;
            private string? _category;

            public Builder WithName(string name)
            {
                _name = name;
                return this;
            }

            public Builder WithDescription(string? description)
            {
                _description = description;
                return this;
            }

            public Builder WithImageUrl(string? imageUrl)
            {
                _imageUrl = imageUrl;
                return this;
            }

            public Builder WithPrice(decimal price)
            {
                _price = price;
                return this;
            }

            public Builder WithCategory(string? category)
            {
                _category = category;
                return this;
            }

            public Product Build()
            {
                if (_name == null || _price == null)
                {
                    throw new InvalidOperationException("Name and price must be set.");
                }
                return new Product(_name, _description, _imageUrl, _price.Value, _category);
            }
        }
    }
}
