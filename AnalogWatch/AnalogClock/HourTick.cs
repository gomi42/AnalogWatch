using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AnalogWatch.AnalogClock
{
    public class HourTick : HourTickBase
    {
        public override int Hour
        {
            get { return (int)GetValue(HourProperty); }
            set { SetValue(HourProperty, value); }
        }

        public static readonly DependencyProperty HourProperty =
            DependencyProperty.Register("Hour", typeof(int), typeof(HourTick), new PropertyMetadata(-42, HourChanged));

        private static void HourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HourTick)d).OnHourChanged();
        }

        private void OnHourChanged()
        {
            int minute = Hour * 5;
            SetAngle(minute);
            SetValue(MinutePropertyKey, minute);
        }

        public int Minute
        {
            get { return (int)GetValue(MinuteProperty); }
        }

        public static readonly DependencyPropertyKey MinutePropertyKey =
            DependencyProperty.RegisterReadOnly("Minute", typeof(int), typeof(HourTick), new PropertyMetadata(0));

        public static readonly DependencyProperty MinuteProperty = MinutePropertyKey.DependencyProperty;
    }
}
