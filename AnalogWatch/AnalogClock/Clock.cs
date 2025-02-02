using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AnalogWatch.AnalogClock
{
    public enum SecondMode
    {
        Jump,
        Smooth,
        Continuous
    }

    public enum MinuteMode
    {
        Jump,
        Smooth
    }

    public class HourTickCollection : List<HourTickBase>
    { }

    public class Clock : Control
    {
        FrameworkElement surface;
        Panel ticksContainer;
        Panel secondsContainer;
        HourHandBase hourHand;
        MinuteHandBase minuteHand;
        SecondHandBase secondHand;
        PositionSecondTimer secondTickTimer;
        PositionSecondTimer smoothMovementAheadTickTimer;

        public Clock()
        {
            SizeChanged += OnSizeChanged;
        }

        public Style HourMajorTickStyle
        {
            get { return (Style)GetValue(HourMajorTickStyleProperty); }
            set { SetValue(HourMajorTickStyleProperty, value); }
        }

        public static readonly DependencyProperty HourMajorTickStyleProperty =
            DependencyProperty.Register("HourMajorTickStyle", typeof(Style), typeof(Clock), new PropertyMetadata(null));

        public Style HourTickStyle
        {
            get { return (Style)GetValue(HourTickStyleProperty); }
            set { SetValue(HourTickStyleProperty, value); }
        }

        public static readonly DependencyProperty HourTickStyleProperty =
            DependencyProperty.Register("HourTickStyle", typeof(Style), typeof(Clock), new PropertyMetadata(null));

        public Style MinuteTickStyle
        {
            get { return (Style)GetValue(MinuteTickStyleProperty); }
            set { SetValue(MinuteTickStyleProperty, value); }
        }

        public static readonly DependencyProperty MinuteTickStyleProperty =
            DependencyProperty.Register("MinuteTickStyle", typeof(Style), typeof(Clock), new PropertyMetadata(null));

        public Style SecondMajorTickStyle
        {
            get { return (Style)GetValue(SecondMajorTickStyleProperty); }
            set { SetValue(SecondMajorTickStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SecondMajorTickStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondMajorTickStyleProperty =
            DependencyProperty.Register("SecondMajorTickStyle", typeof(Style), typeof(Clock), new PropertyMetadata(null));


        public Style HourHandStyle
        {
            get { return (Style)GetValue(HourHandStyleProperty); }
            set { SetValue(HourHandStyleProperty, value); }
        }

        public static readonly DependencyProperty HourHandStyleProperty =
            DependencyProperty.Register("HourHandStyle", typeof(Style), typeof(Clock), new PropertyMetadata(null));

        public Style MinuteHandStyle
        {
            get { return (Style)GetValue(MinuteHandStyleProperty); }
            set { SetValue(MinuteHandStyleProperty, value); }
        }

        public static readonly DependencyProperty MinuteHandStyleProperty =
            DependencyProperty.Register("MinuteHandStyle", typeof(Style), typeof(Clock), new PropertyMetadata(null));

        public Style SecondHandStyle
        {
            get { return (Style)GetValue(SecondHandStyleProperty); }
            set { SetValue(SecondHandStyleProperty, value); }
        }

        public static readonly DependencyProperty SecondHandStyleProperty =
            DependencyProperty.Register("SecondHandStyle", typeof(Style), typeof(Clock), new PropertyMetadata(null));

        public MinuteMode MinuteMode
        {
            get { return (MinuteMode)GetValue(MinuteModeProperty); }
            set { SetValue(MinuteModeProperty, value); }
        }

        public static readonly DependencyProperty MinuteModeProperty =
            DependencyProperty.Register("MinuteMode", typeof(MinuteMode), typeof(Clock), new PropertyMetadata(MinuteMode.Smooth));

        public SecondMode SecondMode
        {
            get { return (SecondMode)GetValue(SecondModeProperty); }
            set { SetValue(SecondModeProperty, value); }
        }

        public static readonly DependencyProperty SecondModeProperty =
            DependencyProperty.Register("SecondMode", typeof(SecondMode), typeof(Clock), new PropertyMetadata(SecondMode.Smooth, SecondModeChanged));

        private static void SecondModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Clock)d).OnSecondModeChanged();
        }

        public bool ShowSecondHand
        {
            get { return (bool)GetValue(ShowSecondHandProperty); }
            set { SetValue(ShowSecondHandProperty, value); }
        }

        public static readonly DependencyProperty ShowSecondHandProperty =
            DependencyProperty.Register("ShowSecondHand", typeof(bool), typeof(Clock), new PropertyMetadata(true, ShowSecondHandChanged));

        private static void ShowSecondHandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Clock)d).OnShowSecondHandChanged();
        }

        public string ShowHourMajorTicks
        {
            get { return (string)GetValue(ShowHourMajorTicksProperty); }
            set { SetValue(ShowHourMajorTicksProperty, value); }
        }

        public static readonly DependencyProperty ShowHourMajorTicksProperty =
            DependencyProperty.Register("ShowHourMajorTicks", typeof(string), typeof(Clock), new PropertyMetadata(""));

        public string ShowHourTicks
        {
            get { return (string)GetValue(ShowHourTicksProperty); }
            set { SetValue(ShowHourTicksProperty, value); }
        }

        public static readonly DependencyProperty ShowHourTicksProperty =
            DependencyProperty.Register("ShowHourTicks", typeof(string), typeof(Clock), new PropertyMetadata("1,2,3,4,5,6,7,8,9,10,11,12"));

        public NumberRotation NumberRotation
        {
            get { return (NumberRotation)GetValue(NumberRotationProperty); }
            set { SetValue(NumberRotationProperty, value); }
        }

        public static readonly DependencyProperty NumberRotationProperty =
            DependencyProperty.Register("NumberRotation", typeof(NumberRotation), typeof(Clock), new PropertyMetadata(NumberRotation.Horizontal, NumberRotationChanged));

        private static void NumberRotationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Clock)d).OnNumberRotationChanged();
        }

        public DateTime DateTime
        {
            get { return (DateTime)GetValue(DateTimeProperty); }
            set { SetValue(DateTimeProperty, value); }
        }

        public static readonly DependencyProperty DateTimeProperty =
            DependencyProperty.Register("DateTime", typeof(DateTime), typeof(Clock), new PropertyMetadata(DateTime.MinValue));


        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (surface == null)
            {
                return;
            }

            SetSize();
        }

        private void SetSize()
        {
            var height = ((int)ActualHeight / 2) * 2;
            var width = ((int)ActualWidth / 2) * 2;

            if (width > height)
            {
                surface.Width = height;
                surface.Height = height;
            }
            else
            {
                surface.Width = width;
                surface.Height = width;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ticksContainer?.Children.Clear();
            secondsContainer?.Children.Clear();

            surface = GetTemplateChild("PART_Surface") as FrameworkElement;

            ticksContainer = GetTemplateChild("PART_TicksContainer") as Panel;
            ticksContainer.Children.Clear();

            secondsContainer = GetTemplateChild("PART_SecondsContainer") as Panel;

            if (secondsContainer == null)
            {
                secondsContainer = ticksContainer;
            }
            else
            {
                secondsContainer.Children.Clear();
            }

            SetSize();

            CreateTicks();
            CreateHourHand();
            CreateMinutHand();

            if (ShowSecondHand)
            {
                CreateSecondHand();
            }

            StartTimer();
        }

        private void CreateTicks()
        {
            var hourMajorTicks = GetTicks(ShowHourMajorTicks);
            var hourMinorTicks = GetTicks(ShowHourTicks);

            for (int i = 1; i <= 60; i++)
            {
                if (i % 5 == 0)
                {
                    var hour = i / 5;

                    if (HourMajorTickStyle != null && hourMajorTicks.Contains(hour))
                    {
                        var tick = new HourTick { Hour = hour };
                        tick.Style = HourMajorTickStyle;
                        tick.NumberRotation = NumberRotation;
                        ticksContainer.Children.Add(tick);
                    }
                    else
                    if (HourTickStyle != null && hourMinorTicks.Contains(hour))
                    {
                        var tick = new HourTick { Hour = hour };
                        tick.Style = HourTickStyle;
                        tick.NumberRotation = NumberRotation;
                        ticksContainer.Children.Add(tick);
                    }
                }
                else
                {
                    if (MinuteTickStyle != null)
                    {
                        var tick = new MinuteTick { Minute = i };
                        tick.Style = MinuteTickStyle;
                        tick.NumberRotation = NumberRotation;
                        ticksContainer.Children.Add(tick);
                    }
                }
            }

            if (SecondMajorTickStyle != null)
            {
                for (int i = 1; i <= 60; i++)
                {
                    if (i % 5 == 0)
                    {
                        var tick = new SecondMajorTick { Second = i };
                        tick.Style = SecondMajorTickStyle;
                        tick.NumberRotation = NumberRotation;
                        secondsContainer.Children.Add(tick);
                    }
                }
            }
        }

        private int[] GetTicks(string ticks)
        {
            var ticksStr = ticks.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var ticksInt = new int[ticksStr.Length];

            for (int i = 0; i < ticksStr.Length; i++)
            {
                ticksInt[i] = int.Parse(ticksStr[i]);
            }

            return ticksInt;
        }

        private void CreateHourHand()
        {
            hourHand = new HourHand();
            hourHand.NumberRotation = NumberRotation;

            if (HourHandStyle != null)
            {
                hourHand.Style = HourHandStyle;
            }

            ticksContainer.Children.Add(hourHand);
        }

        private void CreateMinutHand()
        {
            minuteHand = new MinuteHand();
            minuteHand.NumberRotation = NumberRotation;

            if (MinuteHandStyle != null)
            {
                minuteHand.Style = MinuteHandStyle;
            }

            ticksContainer.Children.Add(minuteHand);
        }

        private void CreateSecondHand()
        {
            secondHand = new SecondHand();
            secondHand.NumberRotation = NumberRotation;

            if (SecondHandStyle != null)
            {
                secondHand.Style = SecondHandStyle;
            }

            secondHand.Duration = GetSecondHandAnimationDuration();
            secondsContainer.Children.Add(secondHand);
        }

        private void OnNumberRotationChanged()
        {
            if (ticksContainer == null)
            {
                return;
            }

            foreach (var child in ticksContainer.Children)
            {
                var angle = child as AngleBase;

                if (angle != null)
                {
                    angle.NumberRotation = NumberRotation;
                }
            }
        }

        private void OnSecondModeChanged()
        {
            if (secondHand != null)
            {
                secondHand.Duration = GetSecondHandAnimationDuration();
                DestroyAheadTimer();
                InitAheadTimer();
            }
        }

        private void OnShowSecondHandChanged()
        {
            if (ShowSecondHand)
            {
                CreateSecondHand();
                SetInitialSecond(DateTime.Now);
                InitAheadTimer();
            }
            else
            {
                if (secondHand != null && ticksContainer.Children.Contains(secondHand))
                {
                    ticksContainer.Children.Remove(secondHand);
                    secondHand = null;
                    DestroyAheadTimer();
                }
            }
        }

        private void StartTimer()
        {
            SetInitialTime(DateTime.Now);

            secondTickTimer = new PositionSecondTimer();
            secondTickTimer.Position = 10;
            secondTickTimer.Tick += OnSecondTickTimer;
            secondTickTimer.Start();

            InitAheadTimer();
        }

        private void InitAheadTimer()
        {
            if (ShowSecondHand && SecondMode == SecondMode.Smooth)
            {
                smoothMovementAheadTickTimer = new PositionSecondTimer();
                smoothMovementAheadTickTimer.Position = 1000 - GetSecondHandAnimationDuration();
                smoothMovementAheadTickTimer.Tick += OnAheadTickTimer;
                smoothMovementAheadTickTimer.Start();
            }
        }

        private void DestroyAheadTimer()
        {
            if (smoothMovementAheadTickTimer != null)
            {
                smoothMovementAheadTickTimer.Stop();
                smoothMovementAheadTickTimer.Tick -= OnAheadTickTimer;
                smoothMovementAheadTickTimer = null;
            }
        }

        private void OnSecondTickTimer(DateTime time)
        {
            SetTime(time);
        }

        private void OnAheadTickTimer(DateTime time)
        {
            if (SecondMode != SecondMode.Smooth)
            {
                return;
            }

            var second = time.Second + 1;

            if (second >= 60)
            {
                second = 0;
            }

            secondHand.Second = second;
        }

        private void SetInitialTime(DateTime time)
        {
            SetHourMinute(time);
            SetInitialSecond(time);
        }

        private void SetTime(DateTime time)
        {
            SetHourMinute(time);
            SetSecond(time);
        }

        private void SetHourMinute(DateTime time)
        {
            DateTime = time;

            var second = (double)time.Second;
            var minute = (double)time.Minute;
            var hour = (double)time.Hour % 12;

            if (MinuteMode == MinuteMode.Smooth)
            {
                minute += second / 60.0;
            }

            hour += minute / 60.0;

            hourHand.Hour = hour;
            minuteHand.Minute = minute;
        }

        private void SetInitialSecond(DateTime time)
        {
            if (secondHand != null)
            {
                var second = (double)time.Second;

                secondHand.Duration = 0;
                secondHand.Second = second;
                secondHand.Duration = GetSecondHandAnimationDuration();
            }
        }

        private void SetSecond(DateTime time)
        {
            if (secondHand == null)
            {
                return;
            }

            switch (SecondMode)
            {
                case SecondMode.Jump:
                {
                    var second = (double)time.Second;
                    secondHand.Second = second;
                    break;
                }

                case SecondMode.Continuous:
                {
                    var second = time.Second + 1;

                    if (second >= 60)
                    {
                        second = 0;
                    }

                    secondHand.Second = second;
                    break;
                }
            }
        }

        private int GetSecondHandAnimationDuration()
        {
            int duration = 0;

            switch (SecondMode)
            {
                case SecondMode.Jump:
                    duration = 0;
                    break;

                case SecondMode.Smooth:
                    duration = 200;
                    break;

                case SecondMode.Continuous:
                    duration = 1000;
                    break;
            }

            return duration;
        }
    }
}
