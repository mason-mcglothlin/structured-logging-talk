using System;
using CafeDataSet.Simulation.Model;

namespace CafeDataSet.Simulation.Website
{
    class WebRequest
    {
        public DateTime Started { get; }
        public string Path { get; }
        public string Method { get; }
        public Order PlacedOrder { get; }
        public string Id { get; } = Guid.NewGuid().ToString("n").Substring(16);
        public int StatusCode { get; set; } = 200;

        public WebRequest(DateTime started, string path, string method, Order placedOrder = null)
        {
            Started = started;
            Path = path;
            Method = method;
            PlacedOrder = placedOrder;
        }
    }
}
