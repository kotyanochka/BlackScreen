namespace BlackWindow.RabbitMQ.Core;

/// <summary>
/// Настройки приложения
/// </summary>
public interface ISettings
{
    /// <summary>
    /// Строка подключения
    /// </summary>
    string ConnectionString { get; }
    /// <summary>
    /// Имя очереди RabbitMQ
    /// </summary>
    string QueueName { get; }
    /// <summary>
    /// Имя обмена RabbitMQ
    /// </summary>
    string ExchangeName { get; }
    /// <summary>
    /// Время отображения, сек
    /// </summary>
    int ShowTime { get; }
}
