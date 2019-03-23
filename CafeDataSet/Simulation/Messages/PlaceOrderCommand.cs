using System;
using CafeDataSet.Simulation.Model;

namespace CafeDataSet.Simulation.Messages
{
    class PlaceOrderCommand
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Order Order { get; }

        public PlaceOrderCommand(Order order)
        {
            Order = order;
        }
    }
}
