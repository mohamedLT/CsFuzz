using System.Drawing;
using Colorful;
using Console = Colorful.Console;

public class Utils
{
    public static void PrintColored(string text)
    {
        var style = new StyleSheet(Color.Gray);
        style.AddStyle("[|].+[|]", Color.Red, m => m.Replace("|", ""));
        style.AddStyle(@"[\*].+[\*]", Color.Purple, m => m.Replace("*", ""));
        Console.WriteLineStyled(text, style);
    }
}
