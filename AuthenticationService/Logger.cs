namespace AuthenticationService
{
    public class Logger : ILogger
    {
        private static string _path;

        public void WriteEvent(string eventMessage)
        {
            File.AppendAllText(System.IO.Path.Combine(_path, "events.txt"), eventMessage);
            Console.WriteLine(eventMessage);
        }

        public void WriteError(string errorMessage)
        {
            File.AppendAllText(System.IO.Path.Combine(_path, "errors.txt"), errorMessage);
            Console.WriteLine(errorMessage);
        }

        public static void CreateLogDirectory()
        {
            _path = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

            if (Directory.Exists(_path))
                Directory.Delete(_path, true);

            Directory.CreateDirectory(_path);
        }
    }
}
