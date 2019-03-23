namespace CafeDataSet.Simulation.Model
{
    class OrderItem
    {
        public OrderItem(string productCode, decimal unitPrice, int quantity)
        {
            ProductCode = productCode;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}