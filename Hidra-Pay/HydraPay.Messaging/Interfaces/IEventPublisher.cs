namespace HidraPay.Messaging.Interfaces
{
    /// <summary>
    /// Contrato para publicar eventos no bus de mensagens.
    /// </summary>
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event);
    }
}
