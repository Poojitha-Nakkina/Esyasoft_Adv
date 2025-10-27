namespace CollegeApp
{
    public interface IMylogger
    {
        void Log(string message);
    }

    public class LogtoFile : IMylogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("LogtoFile");
        }
    }
}
