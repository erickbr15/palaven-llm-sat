using Palaven.Model.Chat;

namespace Palaven.Chat;

public interface IChatService
{
    Task<string> GetChatResponseAsync(ChatMessage chatMessage, CancellationToken cancellationToken);
}
