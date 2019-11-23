using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;
using static StashBot.Module.Message.Handler.ChatStateHandler.IChatCommands;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class ChatCommands : IChatCommands
    {
        private readonly Dictionary<string, Command> allCommands;
        private readonly Dictionary<string, Command> keyboardCommands;

        internal ChatCommands()
        {
            allCommands = new Dictionary<string, Command>();
            keyboardCommands = new Dictionary<string, Command>();
        }

        public void Add(string name, bool showOnKeyboard, Command command)
        {
            if (!string.IsNullOrEmpty(name) && command != null && !ContainsCommand(name))
            {
                allCommands.Add(name, command);

                if (showOnKeyboard)
                {
                    keyboardCommands.Add(name, command);
                }
            }
        }

        public Command Get(string name)
        {
            if (!string.IsNullOrEmpty(name) && ContainsCommand(name))
            {
                return allCommands[name];
            }
            return null;
        }

        public bool ContainsCommand(string name)
        {
            return allCommands.ContainsKey(name);
        }

        public ReplyKeyboardMarkup CreateReplyKeyboard()
        {
            if (keyboardCommands == null || keyboardCommands.Count == 0)
            {
                return null;
            }

            int i = 0;
            KeyboardButton[] buttons = new KeyboardButton[keyboardCommands.Count];
            foreach (var command in keyboardCommands)
            {
                buttons[i] = new KeyboardButton(command.Key);
                i++;
            }

            return new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };
        }
    }
}
