using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace telegit.TelegramClient;

public static class TelegramClient
{
    private static TelegramBotClient? _botClient;
    private static string? _chatId;
    public static async void Init(string? token, string? chatId)
    {
        _botClient = new TelegramBotClient(token ?? throw new Exception("No token provided"));
        _chatId = chatId ?? throw new Exception("No chat id provided");
        
        var me = await _botClient.GetMeAsync();
        Console.WriteLine(
            $"Bot with id {me.Id} connected at {DateTime.Now}"
        );
    }
    
    public static async Task SendMessageAsync(string message)
    {
        if (_botClient == null)
        {
            throw new Exception("Telegram client not initialized");
        }
        
        await _botClient.SendTextMessageAsync(
            chatId: _chatId!,
            text: message,
            parseMode: ParseMode.Html
        );
    }
}