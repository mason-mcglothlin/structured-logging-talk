using System;
using System.Linq;
using CafeDataSet.Simulation.Generators;
using CafeDataSet.Simulation.Model;

namespace CafeDataSet.Simulation.Website
{
    class WebsiteStartRequest : SimulationEvent
    {
        readonly IdGen _email = new RotatingId("aalexandr@example.com", "bbryce@demo.example.com", "cconner@example.net", "ddavis@example.io", "eellis@example.com");


        public WebsiteStartRequest(SimulationEnvironment environment, bool realTime)
            : base(environment, new IntervalSchedule(
                now => realTime ?
                    TimeSpan.FromMilliseconds(1000) :
                    TimeSpan.FromMilliseconds(TimeSpan.FromHours(1).TotalMilliseconds / Math.Abs((now.ToLocalTime().TimeOfDay - TimeSpan.FromHours(10)).TotalMinutes)),
                realTime ? TimeSpan.FromMilliseconds(300) : TimeSpan.FromSeconds(10),
                environment.Clock.UtcNow,
                environment.Random))
        {
        }

        protected override void Execute(SimulationState state, DateTime utcNow)
        {
            WebRequest request;
            if (utcNow.Millisecond > 900)
            {
                request = new WebRequest(utcNow, "/api/products/list", "GET");
                if (utcNow.Millisecond > 990)
                    request.StatusCode = 500;
            }
            else if (utcNow.Millisecond > 400)
            {
                var customer = new Customer("customers-" + Random.Next());
                state.Customers = state.Customers.Concat(new [] {customer}).OrderBy(c => Guid.NewGuid()).Take(10).ToArray();
                request = new WebRequest(utcNow, "/api/customers", "POST");
                if (utcNow.Millisecond > 700)
                    request.StatusCode = 400;
                else
                    Log.ForContext("RequestId", request.Id)
                        .ForContext("SourceContext", "SeqCafe.Web.CustomersController")
                        .Information("Created new customer {CustomerId} email {EmailAddress}", customer.Id, _email.Next());
            }
            else
            {
                if (state.Customers.Length == 0)
                    return;
                var customer = state.Customers[utcNow.Millisecond % state.Customers.Length];
                var items = state.Products.Take(utcNow.Millisecond % 5)
                    .Select((p, i) => new OrderItem(p.Code, p.ListPrice, (utcNow.Millisecond ^ i) % 3))
                    .Where(i => i.Quantity > 0)
                    .ToArray();

                if (items.Length == 0 && utcNow.Millisecond < 390)
                    return;

                var order = new Order(items, customer.Id);
                request = new WebRequest(utcNow, "/api/orders", "POST", order);

                Log.ForContext("RequestId", request.Id)
                    .ForContext("SourceContext", "SeqCafe.Web.OrdersController")
                    .ForContext("OrderItems", items, true)
                    .Information("Received order {OrderId} total ${Total:0.00} for {CustomerId}", order.Id, order.Total, customer.Id);
            }

            state.Requests.Add(request);
        }
    }
}
