using BlackWindow.RabbitMQ.Console;
using BlackWindow.RabbitMQ.Core.Data;
using BlackWindow.RabbitMQ.Core.Implementations;

var prod = new Producer(new Settings());
    
for (int i = 0; i < 100; i++)
{
    await prod.Publish(ImageSamples.ImageJpg);
    await Task.Delay(2000);
}
