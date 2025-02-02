using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnalogWatch.AnalogClock
{
    public class SecondMajorTick : SecondMajorTickBase
    {
        public override int Second
        {
            get { return (int)GetValue(SecondProperty); }
            set { SetValue(SecondProperty, value); }
        }

        public static readonly DependencyProperty SecondProperty =
            DependencyProperty.Register("Second", typeof(int), typeof(SecondMajorTick), new PropertyMetadata(-42, SecondChanged));

        private static void SecondChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SecondMajorTick)d).OnSecondChanged();
        }

        private void OnSecondChanged()
        {
            SetAngle(Second);
        }
    }
}
