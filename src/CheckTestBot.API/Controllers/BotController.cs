using CheckTestBot.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace CheckTestBot.API.Controllers
{
    public class BotController : ControllerBase
    {
        private readonly HandleUpdateService _handleUpdateService;

        public BotController(HandleUpdateService handleUpdateService)
        {
            _handleUpdateService = handleUpdateService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            await _handleUpdateService.HandleUpdateAsync(update);
            return Ok();
        }

    }
}
