using System;
using StashBot.Module;
using StashBot.Module.Message;
using StashBot.BotResponses;
using StashBot.CallbackQueryHandler;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace StashBot
{
    internal class StashBot
    {
        private readonly ITelegramUserMessageFactory telegramUserMessageFactory;

        internal StashBot()
        {
            telegramUserMessageFactory = new TelegramUserMessageFactory();
            TextResponse.SetUpResponses();
            WriteBotStatus();
        }

        private void WriteBotStatus()
        {
            ITelegramBotClient telegramBotClient = ModulesManager.GetTelegramBotClient();

            #if DEBUG

            Telegram.Bot.Types.User me = telegramBotClient.GetMeAsync().Result;
            Console.WriteLine(DateTime.Now + " - Bot set up!");
            Console.WriteLine($"Id: {me.Id}");
            Console.WriteLine($"Login: {me.Username}");
            Console.WriteLine($"Name: {me.FirstName}");

            #endif
        }

        internal void Start()
        {
            ITelegramBotClient telegramBotClient =  ModulesManager.GetTelegramBotClient();

            telegramBotClient.OnMessage += OnMessage;
            telegramBotClient.OnCallbackQuery += OnCallbackQuery;
            telegramBotClient.StartReceiving();
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();

            ITelegramUserMessage userMessage = telegramUserMessageFactory.Create(e.Message);
            messageManager.HandleUserMessage(userMessage);
        }

        private void OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            ITelegramBotClient telegramBotClient = ModulesManager.GetTelegramBotClient();

            if (string.IsNullOrEmpty(e.CallbackQuery.Data))
            {
                return;
            }

            string[] queryArray = e.CallbackQuery.Data.Split(":");
            if (queryArray.Length == 0)
            {
                return;
            }

            ICallbackQueryHandler callbackQueryHandler;
            switch (queryArray[0])
            {
                case "delete_message":
                    callbackQueryHandler = new DeleteMessageHandler();
                    break;
                default:
                    return;
            }

            callbackQueryHandler.Handle(queryArray, e.CallbackQuery.Message.MessageId);
            telegramBotClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
        }
    }
}
