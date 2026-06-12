using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    // dumping everything in here so we can write the file all at once instead of spamming the disk
    static StringBuilder report = new StringBuilder();

    static void Main()
    {
        // hardcoding my path for now. if someone else pulls this, they can change it lol
        string path = @"C:\Users\Levib\Downloads\projects";

        // making the terminal look cool so I feel like a hacker
        Console.WriteLine("Scanning repository... please hold...\n");
        report.AppendLine("# Repository Analyzer Report\n");

        // kick off the recursion starting at level 0
        Explore(path, 0);

        // spit out the final markdown file. boom, resume material.
        string reportPath = Path.Combine(path, "report.md");
        File.WriteAllText(reportPath, report.ToString());

        Console.WriteLine($"\nDone! Successfully exported the markdown report to: {reportPath}");
    }

    static void Explore(string path, int depth)
    {
        // just need the actual folder name, not the massive C:\ path
        string folderName = Path.GetFileName(path);

        // basically the "do not enter" list. if I let this thing scan node_modules my laptop will actually explode.
        string[] ignoredFolders = { "bin", "obj", ".git", ".vs", "node_modules" };

        // if it's junk, just bail out immediately
        if (ignoredFolders.Contains(folderName.ToLower()))
        {
            return;
        }

        // spacing things out based on how deep we are so it looks like an actual tree structure and not a wall of text
        string indent = new string(' ', depth * 2);

        string[] files = Directory.GetFiles(path);
        string[] folders = Directory.GetDirectories(path);

        // tracking the stats. dictionary is clutch here for counting extensions.
        Dictionary<string, int> extensionCounts = new Dictionary<string, int>();
        long largestFileSize = 0;
        string largestFileName = "None";
        long totalFolderSize = 0; // gotta know how much disk space my dead projects are eating up

        // looping through every file in the current folder
        foreach (string file in files)
        {
            FileInfo fileInfo = new FileInfo(file);
            long size = fileInfo.Length;

            totalFolderSize += size;

            // forcing lowercase so .CS and .cs don't get logged as two different things
            string extension = fileInfo.Extension.ToLower();

            // catch weird files without extensions so they don't crash my dictionary
            if (string.IsNullOrEmpty(extension))
                extension = "[no ext]";

            // tally it up
            if (extensionCounts.ContainsKey(extension))
            {
                extensionCounts[extension]++;
            }
            else
            {
                extensionCounts[extension] = 1;
            }

            // looking for the absolute unit of a file in this folder
            if (size > largestFileSize)
            {
                largestFileSize = size;
                largestFileName = fileInfo.Name;
            }
        }

        // --- Formatting the Output ---
        // converting raw bytes to MB because nobody can read a 10-digit byte count
        Console.WriteLine($"{indent}📂 {folderName} ({(totalFolderSize / 1024.0 / 1024.0):F2} MB)");

        // adding the exact same thing to the markdown string
        report.AppendLine(
            $"{indent}- 📂 **{folderName}** ({(totalFolderSize / 1024.0 / 1024.0):F2} MB)"
        );

        if (files.Length > 0)
        {
            report.AppendLine(
                $"{indent}  - 📄 Largest File: `{largestFileName}` ({(largestFileSize / 1024.0):F2} KB)"
            );
            report.AppendLine($"{indent}  - 📊 File Breakdown:");

            // LINQ saves the day. sorting by most common extension so it actually looks professional
            var sortedExtensions = extensionCounts.OrderByDescending(kvp => kvp.Value);

            foreach (var kvp in sortedExtensions)
            {
                report.AppendLine($"{indent}    - `{kvp.Key}`: {kvp.Value}");
            }
        }

        // recursion time. going deeper and passing depth + 1 so the spaces increase.
        foreach (var folder in folders)
        {
            try
            {
                Explore(folder, depth + 1);
            }
            catch (UnauthorizedAccessException)
            {
                // sweeping windows access denied errors under the rug. ignorance is bliss.
            }
        }
    }
}
