using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Xml;

namespace GalJson2Text
{
    class Program
    {
        static void Main(string[] args)
        {
            var outputOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            Console.WriteLine("Please input a path.");
            var pathInput = Console.ReadLine() ?? ".";

            Console.WriteLine("Start scanning this dir.");
            var dirInfo = new DirectoryInfo(pathInput);
            var fileInfoList = dirInfo.GetFiles("*.json");

            Console.WriteLine("Find files:");
            foreach (var fileInfo in fileInfoList)
            {
                Console.WriteLine(fileInfo.Name);
                var jsonStr = File.ReadAllText(fileInfo.FullName);
                var jsonObj = JsonSerializer.Deserialize<Model>(jsonStr);

                if (jsonObj == null) return;

                foreach (var scene in jsonObj.scenes)
                {
                    var textObj = scene.texts;
                    if (textObj != null)
                    {
                        foreach (var textEach in textObj)
                        {
                            // TextEach
                            /*
                            [
                               "夏生",
                               null,
                               [
                                [
                               null,
                               "（色々決意したはいいが、\\n今日からどうやって暮らしていこう……）",
                               "（色々決意したはいいが、今日からどうやって暮らしていこう……）",
                               "（色々決意したはいいが、今日からどうやって暮らしていこう……）"
                               ],
                               [
                               "Natsuki",
                               "(It's all well and good making my own decision, but how do I actually support myself from here on out?)"
                               ],
                               [
                               "夏生",
                               "\"（虽然是下定决心了，\\n可今后的日子要怎么过呢……）\"",
                               "\"（虽然是下定决心了，可今后的日子要怎么过呢……）\"",
                               "\"（虽然是下定决心了，可今后的日子要怎么过呢……）\""
                               ],
                               [
                               "夏生",
                               "\"（雖然是下定決心了，\\n可今後的日子要怎麼過呢……）\"",
                               "\"（雖然是下定決心了，可今後的日子要怎麼過呢……）\"",
                               "\"（雖然是下定決心了，可今後的日子要怎麼過呢……）\""
                               ]
                               ],
                             */

                            if (textEach.Length > 3 && textEach[0] != null)
                            {
                                var spraker = textEach[0].ToString();

                                var chineseDialog = textEach[2].ToString();

                                var dialogArray = JsonSerializer.Deserialize<string[][]>(chineseDialog);


                                var dialog = dialogArray[2][1];

                                Console.WriteLine($"{spraker}: {dialog}|");
                            }
                        }
                    }

                }
            }

        }
    }
}
