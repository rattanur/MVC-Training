// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCGEX_TRAINING.Services
{
    public class LineLogWriter : ILogWriter
    {
        private const string TOKEN = "pj7jyyYfftID8bAe55D14UzvUYFzWPwz8yyOE1xJPbG";
        private LineNotifier line;

        public LineLogWriter()
        {
            line = new LineNotifier(TOKEN);
        }

        public void Write(string message)
        {
            line.Notify(message).GetAwaiter().GetResult();
        }
    }
}
