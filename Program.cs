using System.Diagnostics;

namespace CS2Launcher
{
    class Program
    {
        private static string EnvironmentVariableName => "CS2Launcher_Path";
        private static string ScriptFileName => "csgo_sl_hide.vbs";
        private static bool IsDebug { get; set; }
        private static string DebugArg => "-debug";
        private static string UserInputMessage => $"Please enter the full path to your script directory, eg.: {Path.Combine("C:", "Program Files (x86)", "CSGO Stretched Launcher", "scripts")}, which contains {ScriptFileName} file.";
        private static string PathNotFound => "No valid path found.";
        private static string ProcessSuccess => "Starting...";
        private static string ExitMessage => "Exiting the program...";
        private static string ProcessException => "Exception occurred, failed to launch: ";
        private static string DebugMessagePrefix => "DEBUG: ";
        private static string ExceptionHandling => "Press Esc to exit or Enter to provide a new script path.";
        private static string PathSavedMessage => $"{DebugMessagePrefix}saved path to environment variable: ";
        
        private static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == DebugArg)
            {
                IsDebug = true;
            }
            
            string path = GetScriptPathFromEnvVar();
            
            Launch(path);
        }
        
        private static string GetScriptPathFromEnvVar()
        {
            string? path = Environment.GetEnvironmentVariable(EnvironmentVariableName, EnvironmentVariableTarget.User);

            if (!string.IsNullOrEmpty(path) && Validate(path))
                return path;

            return GetNewPath(false);
        }
        
        private static bool Validate(string? path)
        {
            if (Directory.Exists(path))
            {
                if (IsDebug)
                    Console.WriteLine($"{DebugMessagePrefix}provided directory exists.");
                
                if (File.Exists(Path.Combine(path, ScriptFileName)))
                {
                    Console.WriteLine($"{DebugMessagePrefix}{ScriptFileName} exists in the directory.");
                    return true;
                }
            }
            
            return false;
        }
        
        private static void Launch(string scriptPath)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(scriptPath, ScriptFileName),
                UseShellExecute = true,
                CreateNoWindow = true,
                WorkingDirectory = scriptPath
            };

            try
            {
                var process = Process.Start(processInfo);
                Console.WriteLine(ProcessSuccess);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }
        
        private static string GetNewPath(bool newAttempt)
        {
            if (!newAttempt)
            {
                Console.WriteLine(PathNotFound);
                Thread.Sleep(200);
            }
            Console.WriteLine(UserInputMessage);
            
            string? path = Console.ReadLine();
            
            while (!Validate(path))
            {
                Console.WriteLine(PathNotFound);
                Thread.Sleep(200);
                Console.WriteLine(UserInputMessage);
                path = Console.ReadLine();
            }
            
            SaveNewPath(path);
            return path;
        }
        
        private static void SaveNewPath(string newPath)
        {
            Environment.SetEnvironmentVariable(EnvironmentVariableName, newPath, EnvironmentVariableTarget.User);
            if (IsDebug)
                Console.WriteLine($"{PathSavedMessage}{newPath}");
        }
        
        private static void HandleException(Exception e)
        {
            Console.WriteLine($"{ProcessException}{e.Message}");
            Thread.Sleep(200);
            Console.WriteLine(ExceptionHandling);
            
            while (true)
            {
                var key = Console.ReadKey(intercept: true).Key;

                if (key == ConsoleKey.Escape)
                {
                    Console.WriteLine(ExitMessage);
                    Environment.Exit(0);
                }
                else if (key == ConsoleKey.Enter)
                {
                    string p = GetNewPath(true);
                    Launch(p);
                }
            }
        }
    }
}
