namespace WebApi.Hub
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessage message);
    }
}
