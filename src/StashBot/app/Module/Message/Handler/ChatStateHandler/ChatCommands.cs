using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;
using static StashBot.Module.Message.Handler.ChatStateHandler.IChatCommands;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class ChatCommands : IChatCommands
    {
        private readonly Dictionary<string, Command> commands;

        internal ChatCommands()
        {
            commands = new Dictionary<string, Command>();
        }

        public void Add(string name, Command command)
        {
            if (!string.IsNullOrEmpty(name) && command != null && !ContainsCommand(name))
            {
                commands.Add(name, command);
            }
        }

        public Command Get(string name)
        {
            if (!string.IsNullOrEmpty(name) && ContainsCommand(name))
            {
                return commands[name];
            }
            return null;
        }

        public bool ContainsCommand(string name)
        {
            return commands.ContainsKey(name);
        }

        public ReplyKeyboardMarkup CreateReplyKeyboard()
        {
            if (commands == null || commands.Count == 0)
            {
                return null;
            }

            int i = 0;
            KeyboardButton[] buttons = new KeyboardButton[commands.Count];
            foreach (var command in commands)
            {
                buttons[i] = new KeyboardButton(command.Key);
                i++;
            }

            return new ReplyKeyboardMarkup(buttons);
        }
    }
}
