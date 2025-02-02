using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnalogWatch.AnalogClock
{
    public class HourHand : HourHandBase
    {
        public override double Hour
        {
            get { return (double)GetValue(HourProperty); }
            set { SetValue(HourProperty, value); }
        }

        public static readonly DependencyProperty HourProperty =
            DependencyProperty.Register("Hour", typeof(double), typeof(HourHand), new PropertyMetadata(double.NaN, HourChanged));

        private static void HourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HourHand)d).OnHourChanged();
        }

        private void OnHourChanged()
        {
            SetAngle(Hour * 5);
        }
    }
}
