using BlackWindow.RabbitMQ.Core;

namespace BlackWindow.Configuration;

internal class Settings : ISettings
{
    public string ConnectionString => Properties.Settings.Default.ConnectionString;
    public string SubscriptionId => Properties.Settings.Default.SubscriptionId;
    public int ShowTime => Properties.Settings.Default.ShowTime;
}
