using System;
using System.IO;

using jcIDS.Library.Common;

namespace jcIDS.Service.Library.Helpers {
    public class Logger {
        private readonly string _filePath;

        public Logger(string filePath) {
            _filePath = filePath;
        }

        private string getCurrentTime() => $"{DateTime.Now}";

        public void WriteMessage(string message, bool writeToScreen = true) {
            File.AppendAllText(_filePath, $"{getCurrentTime()} - {message}{Environment.NewLine}");

            if (writeToScreen) {
                Console.WriteLine(message);
            }
        }

        public void WriteMessage(JCIDSException exception) {
            WriteMessage(exception.ToString());
        }
    }
}