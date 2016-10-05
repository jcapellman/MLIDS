using System;

using jcIDS.Service.Library;

namespace jcIDS.Service.Core {
    public class Program {
        public static void Main(string[] args) {
            var mainService = new MainService();

            var initResult = mainService.Initialize();

            if (!initResult) {
                Console.WriteLine("Failed to initialize");

                return;
            }

            mainService.Run();
        }
    }
}