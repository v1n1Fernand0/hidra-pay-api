namespace HidraPay.Messaging.Interfaces
{
    /// <summary>
    /// Contrato para assinar eventos do bus de mensagens.
    /// </summary>
    public interface IEventSubscriber
    {
        Task SubscribeAsync<TEvent>(Func<TEvent, Task> handler, CancellationToken cancellationToken = default);
    }
}
