using BlackWindow.RabbitMQ.Core.Data;
using BlackWindow.RabbitMQ.Core.Implementations;
var prod = new Producer();

await prod.Publish(ImageSamples.ImagePng);
await Task.Delay(10000);
