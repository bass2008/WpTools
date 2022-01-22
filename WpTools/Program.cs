using System;

namespace WpTools
{
    class Program
    {
        static void Main(string[] args)
        {
            var link = "https://www.space.com/20930-dark-matter.html";
            var name = "20930-dark-matter.html";

            var downloader = new SpaceComDownloader();
            downloader.Run(link, name);

            Console.WriteLine("Done");
        }
    }
}
