using System;

namespace jcIDS.Service {
    public class Program {
        public static void Main(string[] args) {
            var mainService = new MainService();

            if (!mainService.Initialize()) {
                Console.WriteLine("Failed to Initialize - shutting down");

                return;
            }

            mainService.Run();
        }
    }
}