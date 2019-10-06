using System.Threading;

namespace StashBot.Module
{
    class Program
    {
        static void Main(string[] args)
        {
            StashBot stashBot = new StashBot();
            stashBot.Start();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
