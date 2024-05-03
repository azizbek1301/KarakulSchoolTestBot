using Microsoft.Extensions.Logging;
using Telegram.Bot;

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
    }
}
