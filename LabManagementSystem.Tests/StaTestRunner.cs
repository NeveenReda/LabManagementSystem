using System.Runtime.ExceptionServices;
using System.Threading;

namespace LabManagementSystem.Tests
{
    internal static class StaTestRunner
    {
        public static void Run(Action action)
        {
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                action();
                return;
            }

            ExceptionDispatchInfo? captured = null;
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    captured = ExceptionDispatchInfo.Capture(ex);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            captured?.Throw();
        }
    }
}

