using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWindow.RabbitMQ.Core.Implementations
{
    public class Consumer : IConsumer
    {
        public Task Subscribe(Action<string> action)
        {
            using var bus = RabbitHutch.CreateBus("host=localhost");
            return bus.PubSub.SubscribeAsync("my_subscription_id", action);
        }
    }
}
