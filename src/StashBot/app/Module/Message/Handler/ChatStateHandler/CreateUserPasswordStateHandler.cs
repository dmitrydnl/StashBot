﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using StashBot.Module.User;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class CreateUserPasswordStateHandler : IChatStateHandler
    {
        private delegate void Command(long chatId, IChatStateHandlerContext context);
        private readonly Dictionary<string, Command> commands;

        internal CreateUserPasswordStateHandler()
        {
            commands = new Dictionary<string, Command>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            commands.Add("/cancel", Cancel);
        }

        public void StartStateMessage(long chatId)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            const string warningMessage = "Input your password or /cancel";
            messageManager.SendMessage(chatId, warningMessage);
        }

        public void HandleUserMessage(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            if (IsCommand(message.Message))
            {
                commands[message.Message](message.ChatId, context);
            }
            else
            {
                if (!string.IsNullOrEmpty(message.Message))
                {
                    RegistrationUser(message, context);
                }
            }
        }

        private bool IsCommand(string message)
        {
            return !string.IsNullOrEmpty(message) && commands.ContainsKey(message);
        }

        private void RegistrationUser(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            if (CheckPassword(message.ChatId, message.Message))
            {
                userManager.CreateNewUser(message.ChatId, message.Message);
                string successMessage = "Success!\nNow you can auth with password";
                messageManager.SendMessage(message.ChatId, successMessage);
                context.ChangeChatState(message.ChatId, Session.ChatSessionState.Start);
            }
        }

        private bool CheckPassword(long chatId, string password)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            if (string.IsNullOrEmpty(password))
            {
                const string warningMessage = "Input password";
                messageManager.SendMessage(chatId, warningMessage);
                return false;
            }

            if (password.Length < 12)
            {
                const string warningMessage = "Password min length 12!";
                messageManager.SendMessage(chatId, warningMessage);
                return false;
            }

            if (password.Length > 25)
            {
                const string warningMessage = "Password max length 25!";
                messageManager.SendMessage(chatId, warningMessage);
                return false;
            }

            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~]+$"))
            {
                const string warningMessage = "Password can contain only letters, numbers and special characters!";
                messageManager.SendMessage(chatId, warningMessage);
                return false;
            }

            return true;
        }

        private void Cancel(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, Session.ChatSessionState.Start);
        }
    }
}