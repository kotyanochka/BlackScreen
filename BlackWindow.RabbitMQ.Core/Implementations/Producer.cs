using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
namespace BlackWindow.RabbitMQ.Core.Implementations
{
    public class Producer : IProducer
    {
        public Task Publish(string text)
        {
            using var bus = RabbitHutch.CreateBus("host=localhost");
            return bus.PubSub.PublishAsync(text);
        }
    }
}
