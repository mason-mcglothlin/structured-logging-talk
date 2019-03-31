using System;
using System.Linq;
using Serilog.Context;

namespace CafeDataSet.Simulation.Integration
{
    class UpdateProductCatalog : SimulationEvent
    {
        public UpdateProductCatalog(SimulationEnvironment environment)
            : base(environment, new IntervalSchedule(TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(1), environment.Clock.UtcNow, environment.Random))
        {
        }

        protected override void Execute(SimulationState state, DateTime utcNow)
        {
            using (LogContext.PushProperty("BatchId", Guid.NewGuid()))
            {
                Log.Information("Updating catalog information from {ImportUrl}",
                    "https://pricing-05835n6X.dddcafe/api/catalog");

                try
                {
                    state.Products = state.Products.Skip(1).Concat(state.Products.Take(1)).ToArray();
                    if (utcNow.Millisecond > 700)
                    {
                        var p = state.Products[0];
                        var newPrice = p.ListPrice + 0.1m;
                        Log.Information("Updating product {ProductId} price from {OldPrice} to {NewPrice}", p.Id, p.ListPrice, newPrice);
                        p.SetListPrice(newPrice);
                    }

                    Log.Information("Catalog update completed");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Catalog update failed");
                }
            }
        }
    }
}
