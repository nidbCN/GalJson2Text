using System;
using System.IO;
using System.Text.Json;

namespace GalJson2Text
{
    public static class Utils
    {
        public static string ConvertTextToYml(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            var result = "categories:\n- atri \nconversations: \n";

            Console.WriteLine("Starting read file.");

            var content = File.ReadAllLines(path);

            foreach (var line in content)
            {
                if (!line.Contains('\"'))
                {
                    continue;
                }
                var data = GetMiddle(line);
                if (line.StartsWith("アトリ"))
                {
                    result += $"  - {data}";
                }
                else
                {
                    result += $"- - {data}";
                }

                result += "\n";
            }

            return result;
        }


        /// <summary>
        /// 将Gal的JSON转化为文本
        /// </summary>
        public static string ConvertJsonToText(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            string result = null;

            Console.WriteLine("Start scanning this dir.");

            var dirInfo = new DirectoryInfo(path);
            var fileInfoList = dirInfo.GetFiles("*.json");

            Console.WriteLine("Find files:");
            foreach (var fileInfo in fileInfoList)
            {
                Console.WriteLine(fileInfo.Name);
                var jsonStr = File.ReadAllText(fileInfo.FullName);
                var jsonObj = JsonSerializer.Deserialize<Model>(jsonStr);

                if (jsonObj == null) return null;

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

                                result = spraker + dialog;
                            }
                        }
                    }

                }
            }

            return result;
        }


        private static string GetMiddle(string input)
        {
            var firstIndex = input.IndexOf('\"') + 1;
            var length = input.LastIndexOf('\"') - firstIndex;
            return input.Substring(firstIndex, length);
        }

    }
}
