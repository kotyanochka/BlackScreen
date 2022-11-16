namespace BlackWindow.RabbitMQ.Core;
//Интерфейс настроек
public interface ISettings
{
    string ConnectionString { get; } //Строка подключения
    string QueueName { get; } //Имя очереди 
    string ExchangeName { get; } //Имя обмена
    int ShowTime { get; }
}
