using BlackWindow.RabbitMQ.Core;

namespace BlackWindow.Configuration;

internal class Settings : ISettings
{
    public string ConnectionString => Properties.Settings.Default.ConnectionString;
    public string QueueName => Properties.Settings.Default.QueueName;
    public int ShowTime => Properties.Settings.Default.ShowTime;
    public string ExchangeName => Properties.Settings.Default.ExchangeName;
}
