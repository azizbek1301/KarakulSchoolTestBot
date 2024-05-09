using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace CheckTestBot.Domain.Services
{
    public class HandleUpdateService 
    {
        private readonly ILogger<ConfigureWebHook> _logger;
        private readonly ITelegramBotClient _botClient;

        public HandleUpdateService(ILogger<ConfigureWebHook> logger, ITelegramBotClient botClient)
        {
            _logger = logger;
            _botClient = botClient;
        }



        public Task HandleErrorAsync(Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            _logger.LogInformation("HandleError : {ErrorMessage}", ErrorMessage);
            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(Update update,CancellationToken cancellationToken)
        {

            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            await Console.Out.WriteLineAsync($"Received a '{messageText}' message in chat {chatId}.");

            var button = new KeyboardButton("Ona tili");

            if(message.Text.Contains("dotnet"))
            {
                Message sentMessage = await _botClient.SendTextMessageAsync(
                    chatId:chatId,
                    text: "You said:\n" + messageText,
                    replyMarkup: new ReplyKeyboardMarkup(button),
                    cancellationToken: cancellationToken);
            }
            //var handler = update.Type switch
            //{
            //    UpdateType.Message => HandleMessageAsync(_botClient, update, cancellationToken),
            //    UpdateType.EditedMessage => HandleEditedMessageAsync(_botClient, update, cancellationToken),
            //    _ => HandleUnknownUpdateType(_botClient, update, cancellationToken),
            //};

            //try
            //{
            //    await handler; 
            //}
            //catch(Exception ex)
            //{
            //    await Console.Out.WriteLineAsync($"Error chiqdi {ex.Message}");
            //}

            //var handler= update switch
            //{ 
            //    { Message: { } message }                       => BotOnMessageReceived(message, cancellationToken),
            //    { EditedMessage: { } message }                 => BotOnMessageReceived(message, cancellationToken),
            //    { CallbackQuery: { } callbackQuery }           => BotOnCallbackQueryReceived(callbackQuery, cancellationToken),
            //    { InlineQuery: { } inlineQuery }               => BotOnInlineQueryReceived(inlineQuery, cancellationToken),
            //    { ChosenInlineResult: { } chosenInlineResult } => BotOnChosenInlineResultReceived(chosenInlineResult, cancellationToken),
            //    _ => UnknownUpdateHandlerAsync(update, cancellationToken)
            //};

            //await handler;
        }

        private async Task HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            
            var handler = message.Type switch
            {
                MessageType.Text => HandleTextMessageAsync(botClient, update, cancellationToken),
                MessageType.Video => HandleVideoMessageAsync(botClient, update, cancellationToken),
                _ => HandleUnknownMessageTypeAsync(botClient, update, cancellationToken),
            };
        }

        private async Task HandleVideoMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await botClient.SendPhotoAsync(
                chatId: update.Message.Chat.Id,
                photo: InputFile.FromUri("https://shotkit.com/wp-content/uploads/2023/03/adobe-photoshop.jpg"),
                cancellationToken: cancellationToken);
        }

        private async Task HandleUnknownUpdateType(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task HandleEditedMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        private async Task HandleUnknownMessageTypeAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task HandleTextMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
        {

            if (message.Text != null)
            {
                var chatId = message.Chat.Id;

                // Create the inline keyboard with four buttons
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Button 1", "button1"),
                    InlineKeyboardButton.WithCallbackData("Button 2", "button2")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Button 3", "button3"),
                    InlineKeyboardButton.WithCallbackData("Button 4", "button4")
                }
            });

                await _botClient.SendTextMessageAsync(chatId, "Choose an option:", replyMarkup: inlineKeyboard, cancellationToken: cancellationToken);
            }
            //_logger.LogInformation("Receive message type: {MessageType}", message.Type);
            //if (message.Text is not { } messageText)
            //    return;

            //var action = messageText.Split(' ')[0] switch
            //{
            //    "/inline_keybord" => SendInlineKeyboard(_botClient, message, cancellationToken),
            //    "/keybord" => SendReplyKeyboard(_botClient, message, cancellationToken),
            //    "/remove" => RemoveKeyboard(_botClient, message, cancellationToken),
            //    "/photo" => SendFile(_botClient, message, cancellationToken),
            //    "/request" => RequestContactAndLocation(_botClient, message, cancellationToken),
            //    "/inline_mode" => StartInlineQuery(_botClient, message, cancellationToken),
            //    _ => Usage(_botClient, message, cancellationToken),
            //};
            //Message sentMessage = await action;
            //_logger.LogInformation("The message was sent with id: {SentMessageId}", sentMessage.MessageId);

        }

        static async Task<Message> SendInlineKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await botClient.SendChatActionAsync(
                chatId: message.Chat.Id,
                chatAction: ChatAction.Typing,
                cancellationToken: cancellationToken);

            // Simulate longer running task
            await Task.Delay(500, cancellationToken);

            InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1.1", "11"),
                        InlineKeyboardButton.WithCallbackData("1.2", "12"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("2.1", "21"),
                        InlineKeyboardButton.WithCallbackData("2.2", "22"),
                    },
                });

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Choose",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }

        static async Task<Message> SendReplyKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(
                new[]
                {
                        new KeyboardButton[] { "1.1", "1.2" },
                        new KeyboardButton[] { "2.1", "2.2" },
                })
            {
                ResizeKeyboard = true
            };

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Choose",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);
        }

        static async Task<Message> RemoveKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Removing keyboard",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
        }

        static async Task<Message> SendFile(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await botClient.SendChatActionAsync(
                message.Chat.Id,
                ChatAction.UploadPhoto,
                cancellationToken: cancellationToken);

            const string filePath = "Files/tux.png";
            await using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();

            return await botClient.SendPhotoAsync(
                chatId: message.Chat.Id,
                photo: new InputFileStream(fileStream, fileName),
                caption: "Nice Picture",
                cancellationToken: cancellationToken);
        }

        static async Task<Message> RequestContactAndLocation(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            ReplyKeyboardMarkup RequestReplyKeyboard = new(
                new[]
                {
                    KeyboardButton.WithRequestLocation("Location"),
                    KeyboardButton.WithRequestContact("Contact"),
                });

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Who or Where are you?",
                replyMarkup: RequestReplyKeyboard,
                cancellationToken: cancellationToken);
        }

        static async Task<Message> Usage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            const string usage = "Usage:\n" +
                                 "/inline_keyboard - send inline keyboard\n" +
                                 "/keyboard    - send custom keyboard\n" +
                                 "/remove      - remove custom keyboard\n" +
                                 "/photo       - send a photo\n" +
                                 "/request     - request location or contact\n" +
                                 "/inline_mode - send keyboard with Inline Query";

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: usage,
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
        }

        static async Task<Message> StartInlineQuery(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            InlineKeyboardMarkup inlineKeyboard = new(
                InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("Inline Mode"));

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Press the button to start Inline Query",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }


        // Process Inline Keyboard callback data
        private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

            await _botClient.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQuery.Id,
                text: $"Received {callbackQuery.Data}",
                cancellationToken: cancellationToken);

            await _botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: $"Received {callbackQuery.Data}",
                cancellationToken: cancellationToken);
        }
        private async Task BotOnInlineQueryReceived(InlineQuery inlineQuery, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received inline query from: {InlineQueryFromId}", inlineQuery.From.Id);

            InlineQueryResult[] results = {
            // displayed result
            new InlineQueryResultArticle(
                id: "1",
                title: "TgBots",
                inputMessageContent: new InputTextMessageContent("hello"))
            };
            await _botClient.AnswerInlineQueryAsync(
            inlineQueryId: inlineQuery.Id,
            results: results,
            cacheTime: 0,
            isPersonal: true,
            cancellationToken: cancellationToken);

        }
        private async Task BotOnChosenInlineResultReceived(ChosenInlineResult chosenInlineResult, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received inline result: {ChosenInlineResultId}", chosenInlineResult.ResultId);

            await _botClient.SendTextMessageAsync(
                chatId: chosenInlineResult.From.Id,
                text: $"You chose result with Id: {chosenInlineResult.ResultId}",
                cancellationToken: cancellationToken);
        }

        private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
            return Task.CompletedTask;
        }
    }
    
}