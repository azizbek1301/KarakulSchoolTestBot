using CheckTestBot.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace CheckTestBot.Domain.Services
{
    public class ConfigureWebHook : IHostedService
    {
       
        private readonly IServiceProvider _serviceProvider;
        private readonly BotConfiguration? _botConfig;

        public ConfigureWebHook(IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
          
            _serviceProvider = serviceProvider;
            _botConfig = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
            
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();


            var webhookAddress = $@"{_botConfig.HostAddress}/bot/{_botConfig.Token}";



            await botClient.SendTextMessageAsync(
               chatId: 1455580577,
               text: "Webhook o'rnatilmoqda");

            await botClient.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: cancellationToken);

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

           

            await botClient.SendTextMessageAsync(
                chatId: 1455580577,
                text: "Bot Uxlamoqda");
        }
    }
}
