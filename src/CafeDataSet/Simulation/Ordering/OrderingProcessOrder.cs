using System;
using System.Linq;
using Serilog.Context;

namespace CafeDataSet.Simulation.Ordering
{
    class OrderingProcessOrder : SimulationEvent
    {
        public OrderingProcessOrder(SimulationEnvironment environment) : base(environment, new ReactiveSchedule())
        {
        }

        protected override void Execute(SimulationState state, DateTime utcNow)
        {
            while (state.Messages.Any())
            {
                var msg = state.Messages.Dequeue();
                using (LogContext.PushProperty("MessageId", msg.Id))
                {
                    Log.ForContext("SourceContext", "SeqCafe.Messaging.MessageBus")
                        .Information("Dispatching message {MessageId}", msg.Id);

                    using (LogContext.PushProperty("SourceContext", "SeqCafe.Ordering.OrderProcessor"))
                    {
                        if (msg.Order.Items.Any())
                        {
                            foreach (var item in msg.Order.Items)
                            {
                                Log.Information("Sending {Quantity}x {ProductCode} to barista for {OrderId}",
                                    item.Quantity, item.ProductCode, msg.Order.Id);
                            }
                        }
                        else
                        {
                            Log.Error("Order {OrderId} is empty", msg.Order.Id);
                        }
                    }
                }
            }
        }
    }
}
