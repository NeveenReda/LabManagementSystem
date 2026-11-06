using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace LabManagementSystem.Common
{
    public class DebounceHelper : IDisposable
    {
        private readonly Timer _timer;
        private Action? _action;
        private CancellationTokenSource? _cts;

        public DebounceHelper(int intervalMs = 280)
        {
            _timer = new Timer { Interval = intervalMs };
            _timer.Tick += (s, e) =>
            {
                _timer.Stop();
                _action?.Invoke();
            };
        }

        public CancellationToken Debounce(Action action)
        {
            _timer.Stop();
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();

            _action = action;
            _timer.Start();

            return _cts.Token;
        }

        public void Cancel()
        {
            _timer.Stop();
            _cts?.Cancel();
        }

        public void Dispose()
        {
            _timer.Dispose();
            _cts?.Dispose();
        }
    }
}

