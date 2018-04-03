using System;
using System.Text;

namespace TotalExercism.Utility.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. ListProjectFolders();
            //2. 
            CreateProjectFolders();
            //3. ListProjectFiles();
            //4.
            //GenerateProjectFile(generateExampleProject: true);
            //GenerateProjectFile(generateExampleProject: false);
        }

        static void ListProjectFolders()
        {
            string rootFolder = @"G:\Learning\exercism\csharp\exercises";
            foreach (var folder in (new System.IO.DirectoryInfo(rootFolder)).GetDirectories())
            {
                Console.WriteLine($"{folder}");
            }
        }

        static void CreateProjectFolders()
        {
            var fp = @"G:\Learning\exercism\csharp\exercises\--- All Projects Sources in One Solution\TotalExercism.Utility.ConsoleApp\Project Folders.txt";
            string rootFolder = @"G:\Learning\exercism\csharp\exercises\--- All Projects Sources in One Solution\TotalExercism";
            foreach (var line in System.IO.File.ReadAllLines(fp))
            {
                var parts = line.Split(new char[] { '|' });
                string folderName = parts[0];
                string folderToCreate = System.IO.Path.Combine(rootFolder, folderName);
                if (!System.IO.Directory.Exists(folderToCreate))
                    System.IO.Directory.CreateDirectory(folderToCreate);

                string placeHolderFileName = "! Project Folder Placeholder.md";
                string pl = System.IO.Path.Combine(folderToCreate, placeHolderFileName);
                if (!System.IO.File.Exists(pl))
                    System.IO.File.WriteAllText(pl, "# Place Holder file to get project's folder committed", Encoding.UTF8);

            }
        }

        static void ListProjectFiles()
        {
            var sb = new StringBuilder();
            var fp = @"G:\Learning\exercism\csharp\exercises\--- All Projects Sources in One Solution\TotalExercism.Utility.ConsoleApp\Project Folders.txt";
            string rootFolder = @"G:\Learning\exercism\csharp\exercises";
            foreach (var line in System.IO.File.ReadAllLines(fp))
            {
                var parts = line.Split(new char[] { '|' });
                string folderName = parts[0];
                string suffix = parts.Length > 1 ? " ***" + parts[1] : "";
                sb.AppendLine($"- {folderName}{suffix}");
                string folder = System.IO.Path.Combine(rootFolder, folderName);
                foreach (var file in (new System.IO.DirectoryInfo(folder)).GetFiles())
                {
                    sb.AppendLine($"    {file.Name}");
                }
            }

            string outputFile = @"G:\Learning\exercism\csharp\exercises\--- All Projects Sources in One Solution\TotalExercism.Utility.ConsoleApp\Projects and files.txt";
            System.IO.File.WriteAllText(outputFile, sb.ToString());

        }

        static void GenerateProjectFile(bool generateExampleProject = false)
        {
            var fp = @"G:\Learning\exercism\csharp\exercises\--- All Projects Sources in One Solution\TotalExercism.Utility.ConsoleApp\Project Folders.txt";
            string rootFolder = @"G:\Learning\exercism\csharp\exercises";

            var projectFileTemplateFullPath = @"G:\Learning\exercism\csharp\exercises\--- All Projects Sources in One Solution\TotalExercism.Utility.ConsoleApp\ProjectFileTemplate.txt";
            var projectFileTemplate = System.IO.File.ReadAllText(projectFileTemplateFullPath);

            var csFiles = new StringBuilder();
            var folders = new StringBuilder();
            var projectsAndReadmes = new StringBuilder();

            foreach (var line in System.IO.File.ReadAllLines(fp))
            {
                var parts = line.Split(new char[] { '|' });
                if (parts.Length > 1)
                {
                    if (parts[1] == "skip" ||
                        parts[1] == "errors" || 
                        ((parts[1] == "all-in-one compilation errors" || parts[1] == "all-in-one examples compilation errors") && generateExampleProject) ||
                        (parts[1] == "all-in-one compilation errors" && !generateExampleProject)
                        )
                    {
                        System.Console.WriteLine($"*** Skipped {line} ***");
                        continue;
                    }
                }

                string folderName = parts[0];

                folders.AppendLine($"    <Folder Include=\"{folderName}\\\" />");

                string folder = System.IO.Path.Combine(rootFolder, folderName);
                foreach (var file in (new System.IO.DirectoryInfo(folder)).GetFiles())
                {
                    if (System.IO.Path.GetExtension(file.Name) == ".csproj" ||
                        System.IO.Path.GetExtension(file.Name) == ".md")
                    {
                        projectsAndReadmes.AppendLine($"    <None Include=\"..\\..\\{folderName}\\{file.Name}\" Link=\"{folderName}\\{file.Name}\" />"); 
                    }

                    if (System.IO.Path.GetExtension(file.Name) == ".cs")
                    {
                        Func<string> makeLine = new Func<string>(()=> $"    <Compile Include=\"..\\..\\{folderName}\\{file.Name}\" Link=\"{folderName}\\{file.Name}\" />");
                        if (generateExampleProject)
                        {
                            if (System.IO.Path.GetFileNameWithoutExtension(file.Name).Equals("example", StringComparison.InvariantCultureIgnoreCase))
                                csFiles.AppendLine(makeLine());
                            else if (System.IO.Path.GetFileNameWithoutExtension(file.Name).Equals(folderName.Replace("-", ""), StringComparison.InvariantCultureIgnoreCase))
                            {
                                // skip 
                            }
                            else csFiles.AppendLine(makeLine());
                        }
                        else  // !generateExampleProject
                        {
                            if (System.IO.Path.GetFileNameWithoutExtension(file.Name).Equals("example", StringComparison.InvariantCultureIgnoreCase))
                            {
                                // skip
                            }
                            else if (System.IO.Path.GetFileNameWithoutExtension(file.Name).Equals(folderName.Replace("-", ""), StringComparison.InvariantCultureIgnoreCase))
                            {
                                csFiles.AppendLine(makeLine());
                            }
                            else csFiles.AppendLine(makeLine());
                        }
                    }
                }
            }

            projectFileTemplate = projectFileTemplate
              .Replace("{{CSFiles}}", csFiles.ToString().TrimEnd())
              .Replace("{{Folders}}", folders.ToString().TrimEnd())
              .Replace("{{ProjectsAndReadmes}}", projectsAndReadmes.ToString().TrimEnd())
              ;

            string outputFolder = @"G:\Learning\exercism\csharp\exercises\--- All Projects Sources in One Solution\TotalExercism\";
            string outputFile = generateExampleProject? "TotalExercism_Generated_Examples.csproj" : "TotalExercism_Generated.csproj";
            System.IO.File.WriteAllText(System.IO.Path.Combine(outputFolder, outputFile), projectFileTemplate);

        }




    }
}
