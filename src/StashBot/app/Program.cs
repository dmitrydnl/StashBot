using System.Threading;
using StashBot.AppSetting;

namespace StashBot
{
    static class Program
    {
        static void Main(string[] args)
        {
            ParseArgs(args);
            StashBot stashBot = new StashBot();
            stashBot.Start();
            Thread.Sleep(Timeout.Infinite);
        }

        private static void ParseArgs(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return;
            }

            if (args.Length >= 1 && !string.IsNullOrEmpty(args[0]))
            {
                BotToken.Set(args[0]);
            }
        }
    }
}
