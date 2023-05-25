using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;
using Core.Services;

namespace DataAccess.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> CreateOrderAsync(Order order)
        {
            try
            {
                await _orderRepository.CreateAsync(order);
                return true;
            }
            catch
            {
                // Afhankelijk van jouw behoeften, kan je hier specifiekere foutafhandeling toevoegen.
                return false;
            }
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                await _orderRepository.DeleteAsync(order);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId)
        {
            // Dit is een gesimplificeerde implementatie en werkt mogelijk niet zoals je verwacht.
            // Je moet ervoor zorgen dat je IOrderRepository.GetOrdersByUserIdAsync methode correct is geïmplementeerd.
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            try
            {
                await _orderRepository.UpdateAsync(order);
                return true;
            }
            catch
            {
                // Afhankelijk van jouw behoeften, kan je hier specifiekere foutafhandeling toevoegen.
                return false;
            }
        }
    }
}
