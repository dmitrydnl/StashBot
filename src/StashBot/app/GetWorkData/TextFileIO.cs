using System.IO;

namespace StashBot.GetWorkData
{
    internal static class TextFileIO
    {
        internal static string Read(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return text;
        }
    }
}
