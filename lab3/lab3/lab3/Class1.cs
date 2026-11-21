using System;
using System.IO;
using System.Linq;
using System.Text;

public class FileMerger
{
    public static int MergeFiles(string sourceFolder, string outputPath)
    {
        if (!Directory.Exists(sourceFolder))
            throw new DirectoryNotFoundException("Указанная папка не существует.");

        string fullOutputPath = Path.GetFullPath(outputPath);

        string[] files = Directory.GetFiles(
            sourceFolder,
            "*.txt",
            SearchOption.AllDirectories
        )
        .Where(file => Path.GetFullPath(file) != fullOutputPath)
        .OrderBy(file => file)
        .ToArray();

        if (files.Length == 0)
            throw new FileNotFoundException("В указанной папке не найдено TXT-файлов.");

        using (StreamWriter writer = new StreamWriter(fullOutputPath, false, Encoding.UTF8))
        {
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);

                writer.WriteLine($"{fileName}f");
                writer.WriteLine();

                using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
                {
                    string content = reader.ReadToEnd();
                    writer.WriteLine(content);
                }

                writer.WriteLine();
            }
        }

        return files.Length;
    }
}
