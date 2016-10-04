using System.IO;

using jcIDS.Library.Common;

namespace jcIDS.Service.Library.Helpers {
    public class Logger {
        private readonly string _filePath;

        public Logger(string filePath) {
            _filePath = filePath;
        }

        public void WriteMessage(string message, bool writeToScreen = true) {
            File.AppendAllText(_filePath, message);

            if (writeToScreen) {
                //Console.WriteLine(message);
            }
        }

        public void WriteMessage(JCIDSException exception) {
            WriteMessage(exception.ToString());
        }
    }
}