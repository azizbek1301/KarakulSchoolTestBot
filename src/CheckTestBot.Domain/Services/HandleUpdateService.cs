using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CheckTestBot.Domain.Services
{
    public class HandleUpdateService
    {
        private readonly ILogger<ConfigureWebHook> _logger;
        private readonly ITelegramBotClient _botClient;

        public HandleUpdateService(ILogger<ConfigureWebHook> logger,ITelegramBotClient botClient )
        {
            _logger=logger;
            _botClient=botClient;
        }

        public async Task HandleUpdateAsync(Update update)
        {
            _logger.LogInformation("Botga message keldi");
            if(update.Message is not null)
            {
                await _botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Kutib turing");
            }
        }
    }
}
