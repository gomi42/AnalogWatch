using System;
using System.Windows.Threading;

namespace AnalogWatch.AnalogClock
{
    public class PositionSecondTimer
    {
        DispatcherTimer timer;
        bool setIntervalToOneSecond;

        public int Position { get; set; }

        public event Action<DateTime> Tick;

        public void Start()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= OnTimer;
                timer = null;
            }

            timer = new DispatcherTimer();
            timer.Tick += OnTimer;
            var now = DateTime.Now;

            SetTimerToBeginOfInterval(now);
            timer.Start();
        }

        public void Stop()
        {
            timer?.Stop();
            timer = null;
        }

        private void OnTimer(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var diffms = now.Millisecond;


            if (diffms < Position - 20 || diffms > Position + 20)
            {
                SetTimerToBeginOfInterval(now);
            }
            else
            if (setIntervalToOneSecond)
            {
                timer.Interval = TimeSpan.FromMilliseconds(1000);
                setIntervalToOneSecond = false;
            }

            Tick(now);
        }

        private void SetTimerToBeginOfInterval(DateTime now)
        {
            var diff = Position - now.Millisecond;

            if (diff < 0)
            {
                diff += 1000;
            }

            var corr = TimeSpan.FromMilliseconds(diff);
            timer.Interval = corr;
            setIntervalToOneSecond = true;
        }
    }
}
