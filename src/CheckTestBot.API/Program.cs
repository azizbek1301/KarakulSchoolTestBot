using CheckTestBot.Domain.Data;
using CheckTestBot.Domain.Models;
using CheckTestBot.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Polling;

var builder = WebApplication.CreateBuilder(args);

var botConfig = builder.Configuration.GetSection("BotConfiguration")
    .Get<BotConfiguration>();

builder.Services.AddHttpClient("webhook")
    .AddTypedClient<ITelegramBotClient>(httpClient
        => new TelegramBotClient(botConfig.Token, httpClient));


builder.Services.AddHostedService<ConfigureWebHook>();
builder.Services.AddScoped<HandleUpdateService>();
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<BotDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")));

var app = builder.Build();

app.UseRouting();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    var token = botConfig.Token;

    endpoints.MapControllerRoute(
        name: "webhook",
        pattern: $"bot/{token}",
        new { controller = "Bot", action = "Post" });

    endpoints.MapControllers();
});

app.Run();