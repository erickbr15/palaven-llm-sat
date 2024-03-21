namespace Liara.OpenAI.Model.Chat;

public class ChatCompletionBodyBuilder
{
    private ChatCompletionBody? _target;

    public ChatCompletionBodyBuilder NewWith(IEnumerable<Message> messages, ChatCompletionCreationModel model)
    {
        if (messages == null || !messages.Any())
        {
            throw new ArgumentNullException(nameof(messages));
        }

        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        _target = new ChatCompletionBody
        {
            Model = model.Model,
            ResponseFormat = model.ResponseFormat!,
            Temperature = model.Temperature,
        };
        _target.Messages.AddRange(messages);

        return this;
    }

    public ChatCompletionBody? Build()
    {
        return _target;
    }
}
