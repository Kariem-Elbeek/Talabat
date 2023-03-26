using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            // 1. Get Basket From Basket Repo
            var basket = await _basketRepository.GetBasketAsync(basketId);

            // 2. Get selected Items at Basket from Products Repo
            var orderItems = new List<OrderItem>();

            foreach (var item in orderItems)
            {
                var product = await _unitOfWork.Reopsitory<Product>().GetByIdAsync(item.Id);
                var productOrdered = new ProductItemOrdered()
                {
                    ProductId = item.Id,
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl,
                };
                var orderItem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    Price = product.Price,
                    Product = productOrdered
                };
                orderItems.Add(orderItem);
            }

            // 3. Calculate Subtotal
            var subTotal = orderItems.Sum(O => O.Price * O.Quantity);

            // 4. Get Delivery Method from Delivery Methods Repo
            var deliveryMethod = await _unitOfWork.Reopsitory<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // 5. Create Order
            var resultOrder = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal
               );

            // 6. Save to Databse [ToDo]
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            return resultOrder;
            //throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
