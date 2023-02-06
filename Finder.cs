using System.Collections.Concurrent;
using System.Text;

public enum FinderType
{
    FILE,
    DIRECTORY,
    TEXT,
    FILES,
}


public class Finder
{
    public FinderType Type;
    public string? Dir;
    public string SearchTarget = "";
    public bool CaseSensitive = false;

    public void Start()
    {
        if (String.IsNullOrEmpty(SearchTarget))
        {
            Utils.PrintColored("|the search target is an empty string|");
            return;
        }
        if (Dir is null)
        {
            Dir = Directory.GetCurrentDirectory();
        }
        else if (Type == FinderType.FILE || Type == FinderType.DIRECTORY || Type == FinderType.FILES)
        {
            if (!Directory.Exists(Dir))
            {
                Utils.PrintColored($"|{Dir} does not exists|");
                return;
            }
        }
        else
        {
            if (!File.Exists(Dir))
            {
                Utils.PrintColored($"|{Dir} does not exists|");
                return;
            }
        }
        List<string> result = new();
        switch (Type)
        {
            case FinderType.FILE:
                result = FindFile(Dir, SearchTarget);
                break;
            case FinderType.FILES:
                result = FindInFiles(Dir, SearchTarget);
                break;
            case FinderType.DIRECTORY:
                result = FindDir(Dir, SearchTarget);
                break;
            case FinderType.TEXT:
                result = FindInFile(Dir, SearchTarget);
                break;
        }
        foreach (string res in result)
        {
            Utils.PrintColored(res);
        }
    }
    List<string> FindFile(string directory, string file_name)
    {
        var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
        ConcurrentBag<string> bag = new();
        Parallel.ForEach(files, file =>
        {
            if (FuzzyIn(file_name, file)) bag.Add(file);
        });
        return bag.ToList();
    }
    List<string> FindInFiles(string directory, string text)
    {
        var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
        List<string> res = new();
        foreach (var file in files)
        {
            if (GetEncoding(file) != Encoding.UTF8) continue;
            res.AddRange(FindInFile(file, text));
        };
        return res;
    }
    List<string> FindDir(string directory, string dir_name)
    {
        var dirs = Directory.GetDirectories(directory, "*", SearchOption.AllDirectories);
        ConcurrentBag<string> bag = new();
        Parallel.ForEach(dirs, dir =>
        {
            if (FuzzyIn(dir_name, dir)) bag.Add(dir);
        });
        return bag.ToList();
    }
    List<string> FindInFile(string path, string text)
    {
        ConcurrentBag<string> bag = new();
        var file = Path.GetFileName(path);
        Parallel.ForEach(File.ReadLines(path), (line, _, i) =>
        {
            if (FuzzyIn(text, line))
            {
                bag.Add($"*{file}:{i}:* {line}");
            }
        });
        return bag.ToList();
    }
    static Encoding GetEncoding(string filename)
    {
        using (var reader = new StreamReader(filename, Encoding.Default, true))
        {
            if (reader.Peek() >= 0)
                reader.Read();

            return reader.CurrentEncoding;
        }
    }


    public bool FuzzyIn(string s1, string s2)
    {
        var i = 0;
        if (s1 == s2) { return true; }
        else if (s2.Length > s1.Length)
        {
            for (var j = 0; j < s2.Length; j++)
            {
                if (CaseSensitive ? s1[i] == s2[j] : char.ToUpper(s1[i]) == char.ToUpper(s2[j]))
                {
                    i++;
                }
                if (i == s1.Length) break;
            }
            return i > (3 * s1.Length / 4);
        }


        return false;
    }

}
