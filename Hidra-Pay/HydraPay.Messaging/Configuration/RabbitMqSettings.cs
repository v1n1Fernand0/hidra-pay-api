namespace HidraPay.Messaging.Configuration
{
    /// <summary>
    /// Configurações de conexão e filas/exchanges do RabbitMQ.
    /// </summary>
    public class RabbitMqSettings
    {
        public string HostName { get; set; } = default!;
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;

        public string ExchangeName { get; set; } = "hidra-pay.exchange";
        public string QueueName { get; set; } = "hidra-pay.queue";
    }
}
