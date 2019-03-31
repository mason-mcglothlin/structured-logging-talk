using System;

namespace CafeDataSet.Simulation.Model
{
    class Product
    {
        public Product(string id, string code, string description, decimal listPrice)
        {
            Id = id;
            Code = code;
            Description = description;
            ListPrice = listPrice;
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal ListPrice { get; set; }

        public void SetListPrice(decimal newPrice)
        {
            if (newPrice > 5m)
                throw new ArgumentException("That's too much to pay.");
            ListPrice = newPrice;
        }
    }
}