using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AnalogWatch.AnalogClock
{

    public class SecondHand : SecondHandBase
    {
        FrameworkElement rotate;
        double prevAngle = 0.0;

        public override double Second
        {
            get { return (double)GetValue(SecondProperty); }
            set { SetValue(SecondProperty, value); }
        }

        public static readonly DependencyProperty SecondProperty =
            DependencyProperty.Register("Second", typeof(double), typeof(SecondHand), new PropertyMetadata(0.0, SecondChanged));

        private static void SecondChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SecondHand)d).OnSecondChanged();
        }

        public override int Duration
        {
            get { return (int)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(int), typeof(SecondHand), new PropertyMetadata(0));

        private void OnSecondChanged()
        {
            if (rotate != null)
            {
                var angle = GetAngle(Second);

                if (angle < 0 && prevAngle > 0)
                {
                    prevAngle -= 360;
                }

                DoubleAnimation animation = new DoubleAnimation(prevAngle, angle, new Duration(TimeSpan.FromMilliseconds(Duration)));
                rotate.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
                prevAngle = angle;
            }
            else
            {
                AnimateAngle(Second, Duration);
            }

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            rotate = GetTemplateChild("PART_Rotate") as FrameworkElement;
            Angle = GetAngle(Second);
            prevAngle = Angle;

            if (rotate != null)
            {
                rotate.RenderTransform = new RotateTransform { Angle = this.Angle };
            }
        }
    }
}
