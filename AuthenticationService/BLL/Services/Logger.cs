namespace AuthenticationService.BLL.Services
{
    public class Logger : ILogger
    {
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private string rootLogDir { get; set; }

        public Logger()
        {
            rootLogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "_logs", DateTime.Now.ToString("dd_MM_yy HH-mm-ss"));

            if (!Directory.Exists(rootLogDir))
                Directory.CreateDirectory(rootLogDir);
        }

        public void WriteEvent(string eventMessage)
        {
            _lock.EnterWriteLock();
            try
            {
                using (StreamWriter sw = new StreamWriter(rootLogDir + "/events.txt", append: true))
                    sw.WriteLine(eventMessage);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void WriteError(string errorMessage)
        {
            _lock.EnterWriteLock();
            try
            {
                using (StreamWriter sw = new StreamWriter(rootLogDir + "/errors.txt", append: true))
                    sw.WriteLine(errorMessage);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
