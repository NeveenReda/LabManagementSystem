using System.Diagnostics;

namespace LabManagementSystem.Common
{
    public static class PerformanceLogger
    {
        public static IDisposable Measure(string operationName, Action<long>? onCompleted = null)
        {
            return new BenchmarkScope(operationName, onCompleted);
        }

        private class BenchmarkScope : IDisposable
        {
            private readonly string _operation;
            private readonly Action<long>? _callback;
            private readonly Stopwatch _sw;

            public BenchmarkScope(string operation, Action<long>? callback)
            {
                _operation = operation;
                _callback = callback;
                _sw = Stopwatch.StartNew();
            }

            public void Dispose()
            {
                _sw.Stop();
                long elapsed = _sw.ElapsedMilliseconds;
                Debug.WriteLine($"[PERF] {_operation} completed in {elapsed}ms");
                _callback?.Invoke(elapsed);
            }
        }
    }
}

