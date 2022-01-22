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

            // Save

            var enviroment = System.Environment.CurrentDirectory;

            var path = Directory.GetParent(enviroment)!.Parent!.Parent!.FullName;
            
            path = Path.Combine(path, "lake");

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, "spaceCom");

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            path = Path.Combine(path, name);

            File.WriteAllText(path, s);
        }
    }
}
