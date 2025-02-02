using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnalogWatch.AnalogClock
{
    public class MinuteHand : MinuteHandBase
    {
        public override double Minute
        {
            get { return (double)GetValue(MinuteProperty); }
            set { SetValue(MinuteProperty, value); }
        }

        public static readonly DependencyProperty MinuteProperty =
            DependencyProperty.Register("Minute", typeof(double), typeof(MinuteHand), new PropertyMetadata(double.NaN, MinuteChanged));

        private static void MinuteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MinuteHand)d).OnMinuteChanged();
        }

        private void OnMinuteChanged()
        {
            SetAngle(Minute);
        }
    }
}
