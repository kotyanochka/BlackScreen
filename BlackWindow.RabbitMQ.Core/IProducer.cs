using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackWindow.RabbitMQ.Core;

public interface IProducer
{
    Task Publish(string text);
}
