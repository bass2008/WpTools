using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json;
using RestSharp;

namespace WpTools
{
    public class SpaceComTranslator
    {
        public string Translate(string input)
        {
            // Extract title
            
            var doc = new HtmlDocument();
            doc.LoadHtml(input);
            var title = doc.DocumentNode.SelectSingleNode("//*[@id=\"main\"]/article/header/h1").InnerText;

            // Translate 
            var tTitle = YandexTranslate(title);
            
            var i = 1;
            while (true)
            {
                var p = doc.DocumentNode.SelectSingleNode($"//*[@id=\"article-body\"]/p[{i++}]");
                var tP = YandexTranslate(p.InnerHtml);
            }

            return tTitle;
        }

        private static string YandexTranslate(string title)
        {
            var client = new RestClient("https://translate.api.cloud.yandex.net/translate/v2/translate");
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization",
                "Bearer t1.9euelZrJkZCTnZKOyMzKxo2Zyc6Pje3rnpWai4uZz83MkZGSlMrPkZ2ZkYzl9PctAE5w-e9xXjqr3fT3bS5LcPnvcV46qw.VLBMgR6rG0WgIZNEScIK-8ETnf5prsoAj6cgeFUuo5r3cpe8Nuft1VGYRhk6wq0zaEf5hmoGcBKk68uMFdgFAA");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json",
                $"{{\r\n    \"folderId\": \"b1g5pkrqtur35j4hn7vh\",\r\n    \"texts\": [\"{title}\"],\r\n    \"targetLanguageCode\": \"ru\"\r\n}}",
                ParameterType.RequestBody);
            var response = client.Execute(request);
            var json = response.Content;

            var result = JsonConvert.DeserializeObject<YandexTranslateResponse>(json);

            return result?.Translations.SingleOrDefault()?.Text ?? string.Empty;
        }

        class YandexTranslateResponse
        {
            public Translation[] Translations { get; set; }

            public class Translation
            {
                public string Text { get; set; }
                public string DetectedLanguageCode { get; set; }
            }
        }
    }
}
