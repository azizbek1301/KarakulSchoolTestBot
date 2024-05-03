using CheckTestBot.Domain.Models;
using CheckTestBot.Domain.Services;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

var botConfig = builder.Configuration.GetSection("BotConfiguration")
    .Get<BotConfiguration>();

builder.Services.AddHttpClient("webhook")
    .AddTypedClient<ITelegramBotClient>(httpClient
        => new TelegramBotClient(botConfig.Token, httpClient));

builder.Services.AddHostedService<ConfigureWebHook>();
builder.Services.AddScoped<HandleUpdateService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    var token = botConfig.Token;

    endpoints.MapControllerRoute(
        name: "webhook",
        pattern: $"bot/{token}",
        new { controller = "Webhook", action = "Post" });

    endpoints.MapControllers();
});

app.Run();