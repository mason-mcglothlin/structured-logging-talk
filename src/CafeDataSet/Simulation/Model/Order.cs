using System;
using System.Linq;

namespace CafeDataSet.Simulation.Model
{
    class Order
    {
        public Order(OrderItem[] items, string customerId)
        {
            Items = items;
            CustomerId = customerId;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public OrderItem[] Items { get; set; }
        public string CustomerId { get; set; }
        public decimal Total => Items.Sum(i => i.Quantity * i.UnitPrice);
    }
}
