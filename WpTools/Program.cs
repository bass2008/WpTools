using System;

namespace WpTools
{
    class Program
    {
        private const string Link = "https://www.space.com/20930-dark-matter.html";

        private const string Name = "20930-dark-matter.html";

        static void Main(string[] args)
        {
            var downloader = new SpaceComDownloader();
            var text = downloader.Load(Link, Name);

            var translator = new SpaceComTranslator();
            var translated = translator.Translate(text);

            Console.WriteLine("Done");
        }
    }
}
