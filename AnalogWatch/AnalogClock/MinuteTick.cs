using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AnalogWatch.AnalogClock
{
    public class MinuteTick : MinuteTickBase
    {
        public override int Minute
        {
            get { return (int)GetValue(MinuteProperty); }
            set { SetValue(MinuteProperty, value); }
        }

        public static readonly DependencyProperty MinuteProperty =
            DependencyProperty.Register("Minute", typeof(int), typeof(MinuteTick), new PropertyMetadata(-42, MinuteChanged));

        private static void MinuteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MinuteTick)d).OnMinuteChanged();
        }

        private void OnMinuteChanged()
        {
            SetAngle(Minute);
        }
    }
}
