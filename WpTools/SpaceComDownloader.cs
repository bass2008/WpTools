using System;
using System.IO;
using RestSharp;

namespace WpTools
{
    public class SpaceComDownloader
    {
        public void Run(string link, string name)
        {
            // Download
            var client = new RestClient(link);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var s = response.Content;

            Console.WriteLine($"{name} is downloaded");

            // Save
            var path = GetPath(name);
            File.WriteAllText(path, s);
        }

        public string Load(string link, string name)
        {
            var path = GetPath(name);
            if (File.Exists(path) == false)
                Run(link, name);

            if (File.Exists(path) == false)
                throw new Exception("File is not exist! Something wrong!");

            var text = File.ReadAllText(path);
            return text;
        }

        private static string GetPath(string name)
        {
            var path = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;

            path = Path.Combine(path, "lake");

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, "spaceCom");

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, name);
            return path;
        }
    }
}
