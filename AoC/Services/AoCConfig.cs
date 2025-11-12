namespace AoC.Services
{
    public class AoCConfig
    {
        private static string? _sessionCookie;
        private static readonly string ConfigFilePath = ".aoc-session";

        public static string? GetSessionCookie()
        {
            if (_sessionCookie != null)
                return _sessionCookie;

            // Try to load from file in project root
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", ConfigFilePath);
            if (File.Exists(rootPath))
            {
                _sessionCookie = File.ReadAllText(rootPath).Trim();
                return _sessionCookie;
            }

            // Try to load from current directory
            if (File.Exists(ConfigFilePath))
            {
                _sessionCookie = File.ReadAllText(ConfigFilePath).Trim();
                return _sessionCookie;
            }

            // Try environment variable
            _sessionCookie = Environment.GetEnvironmentVariable("AOC_SESSION");
            return _sessionCookie;
        }

        public static void SetSessionCookie(string sessionCookie)
        {
            _sessionCookie = sessionCookie;
            
            // Save to file in project root
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", ConfigFilePath);
            var directory = Path.GetDirectoryName(rootPath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllText(rootPath, sessionCookie);
            Console.WriteLine($"Session cookie saved to: {Path.GetFullPath(rootPath)}");
        }

        public static bool HasSessionCookie()
        {
            return !string.IsNullOrWhiteSpace(GetSessionCookie());
        }
    }
}
