using System;
using GalJson2Text;


Console.WriteLine("Choose a mode\n1.Convert json file to text.\n2.Convert text to yml.");
var mode = Console.ReadLine();

Console.WriteLine("Please input a path.");
var path = Console.ReadLine();

int.TryParse(mode ?? "1", out int modeInt);

string result = null;

switch (modeInt)
{
    case 1:
        result = Utils.ConvertJsonToText(path);
        break;
    case 2:
        result = Utils.ConvertTextToYml(path);
        break;
    default:
        Console.WriteLine("UnKnow");
        return;
}

Console.WriteLine(result);
