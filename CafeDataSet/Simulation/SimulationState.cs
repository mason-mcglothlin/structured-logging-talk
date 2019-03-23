using System.Collections.Generic;
using CafeDataSet.Simulation.Messages;
using CafeDataSet.Simulation.Model;
using CafeDataSet.Simulation.Website;

namespace CafeDataSet.Simulation
{
    class SimulationState
    {
        public Product[] Products { get; set; }
        public Customer[] Customers { get; set; } = new Customer[0];
        public List<WebRequest> Requests { get; } = new List<WebRequest>();
        public Queue<PlaceOrderCommand> Messages { get; set; } = new Queue<PlaceOrderCommand>();

        public SimulationState()
        {
            Products = new[]
            {
                new Product("product-13",  "SOL100", "Espresso", 3m),
                new Product("product-15",  "DOP499", "Espresso Doppio", 3.5m),
                new Product("product-643", "CAP001", "Cappuccino", 4.2m),
                new Product("product-32",  "LAT001", "Cafe Latte", 4.2m),
                new Product("product-11",  "CRO102", "Almond Croissant", 5m),
                new Product("product-15",  "DOP499", "Espresso Doppio", 3.5m),
                new Product("product-210", "AME994", "Cafe Americano", 3.5m),
            };
        }
    }
}
