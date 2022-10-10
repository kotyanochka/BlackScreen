using BlackWindow.RabbitMQ.Core.Data;
using BlackWindow.RabbitMQ.Core.Implementations;
var prod = new Producer();
    
for (int i = 0; i < 100; i++)
{
    await prod.Publish(ImageSamples.ImagePng);
    await Task.Delay(5000);
}
