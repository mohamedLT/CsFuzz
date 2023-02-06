

public static class CsFuzz
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            ShowHelp();
            return;
        }

        Finder finder = new();

        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];

            switch (arg)
            {
                case "-h" or "-help":
                    ShowHelp();
                    return;

                case "-f":
                    finder.Type = FinderType.FILE;
                    if (args.Length > i + 1) finder.SearchTarget = args[i + 1];
                    break;

                case "-d":
                    finder.Type = FinderType.DIRECTORY;
                    if (args.Length > i + 1) finder.SearchTarget = args[i + 1];
                    break;

                case "-T":
                    finder.Type = FinderType.FILES;
                    if (args.Length > i + 1) finder.SearchTarget = args[i + 1];
                    break;

                case "-t":
                    finder.Type = FinderType.TEXT;
                    if (args.Length > i + 1) finder.SearchTarget = args[i + 1];
                    break;

                case "-s":
                    finder.CaseSensitive = true;
                    break;

                case "-D":
                    if (args.Length > i + 1 && args[i + 1] != ".")
                    {
                        finder.Dir = args[i + 1];
                        finder.Start();
                    }
                    break;

                default:
                    if (i == 0)
                        ShowHelp();
                    break;

            }

        }
    }


    static void ShowHelp()
    {
        Console.WriteLine(@"
CsFuzz is a fuzzy finder made in Csharp
Usage:
	CsFuzz <args> -[t|f|d] string -D [directory|file name] 

options :
	-t 	Search for text in file ( used with file path)
	-T 	Search for text in every file in directory 
	-f 	Search for file in directory and sub directorys
	-d 	Search for directory in directory and sub directorys

	-D 	Path to directory or . for current directory
args:	
	-s 	Case sensitive search 
");
    }
}
