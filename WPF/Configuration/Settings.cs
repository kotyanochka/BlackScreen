using BlackWindow.RabbitMQ.Core;

namespace BlackWindow.Configuration;
//Настройки
internal class Settings : ISettings
{
    //Строка подключения
    public string ConnectionString => Properties.Settings.Default.ConnectionString;
    //Имя очереди
    public string QueueName => Properties.Settings.Default.QueueName;
    //Время отображения картинки
    public int ShowTime => Properties.Settings.Default.ShowTime;
    //Имя обменника
    public string ExchangeName => Properties.Settings.Default.ExchangeName;
}
