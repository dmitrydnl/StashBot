using System;
using StashBot.Module;
using StashBot.Module.Message;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace StashBot
{
    internal class StashBot
    {
        private ITelegramUserMessageFactory telegramUserMessageFactory;

        internal StashBot()
        {
            telegramUserMessageFactory = new TelegramUserMessageFactory();
            WriteBotStatus();
        }

        private void WriteBotStatus()
        {
            ITelegramBotClient telegramBotClient =
                ModulesManager.GetModulesManager().GetTelegramBotClient();

            Telegram.Bot.Types.User me = telegramBotClient.GetMeAsync().Result;
            Console.WriteLine(DateTime.Now + " - Bot set up!");
            Console.WriteLine($"Id: {me.Id}");
            Console.WriteLine($"Login: {me.Username}");
            Console.WriteLine($"Name: {me.FirstName}");
        }

        internal void Start()
        {
            ITelegramBotClient telegramBotClient = 
                ModulesManager.GetModulesManager().GetTelegramBotClient();

            telegramBotClient.OnMessage += OnMessage;
            telegramBotClient.StartReceiving();
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            IMessageManager messageManager =
                   ModulesManager.GetModulesManager().GetMessageManager();

            messageManager.HandleUserMessage(telegramUserMessageFactory.Create(e.Message));
        }
    }
}
