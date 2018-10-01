using System;
using System.Runtime.Loader;

using jcIDS.library.core.Managers;

using NLog;

namespace jcIDS.service.app
{
    class Program
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private static CoreManager _coreManager;

        static void Main(string[] args)
        {
            AssemblyLoadContext.Default.Unloading += Default_Unloading;

            Console.CancelKeyPress += Console_CancelKeyPress;

            _coreManager = new CoreManager();
            
            if (!_coreManager.Initialize())
            {
                return;
            }

            _coreManager.StartService();
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Log.Info("Service Canceled");

            _coreManager?.StopService();

            Log.Info("Succesfully Canceled");
        }

        private static void Default_Unloading(AssemblyLoadContext obj)
        {
            Log.Info("Service unloading");

            _coreManager?.StopService();

            Log.Info("Succesfully Unloaded");
        }
    }
}