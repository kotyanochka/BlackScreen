using BlackWindow.RabbitMQ.Core.Implementations;
var prod = new Producer();
await prod.Publish("TEXT TEXT");
await Task.Delay(5000);
await prod.Publish("TEX");
await Task.Delay(5000);
await prod.Publish("TEXwwww");
await Task.Delay(5000);
