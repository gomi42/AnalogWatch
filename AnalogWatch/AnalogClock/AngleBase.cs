using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AnalogWatch.AnalogClock
{
    public enum NumberRotation
    {
        Horizontal,
        Tangent,
        TangentUpside
    }

    public class AngleBase : Control
    {
        double prevAngle = 0.0;

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            protected set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(AngleBase), new PropertyMetadata(0.0));

        public double NumberAngle
        {
            get { return (double)GetValue(NumberAngleProperty); }
            protected set { SetValue(NumberAngleProperty, value); }
        }

        public static readonly DependencyProperty NumberAngleProperty =
            DependencyProperty.Register("NumberAngle", typeof(double), typeof(AngleBase), new PropertyMetadata(0.0));

        public NumberRotation NumberRotation
        {
            get { return (NumberRotation)GetValue(NumberRotationProperty); }
            set { SetValue(NumberRotationProperty, value); }
        }

        public static readonly DependencyProperty NumberRotationProperty =
            DependencyProperty.Register("NumberRotation", typeof(NumberRotation), typeof(AngleBase), new PropertyMetadata(NumberRotation.Horizontal, NumberRotationChanged));

        private static void NumberRotationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AngleBase)d).OnNumberRotationChanged();
        }

        protected double GetAngle(double second)
        {
            var angle = second * 6;

            if (angle > 360)
            {
                angle -= 360;
            }

            angle -= 90;

            if (angle < 0)
            {
                angle += 360;
            }

            if (angle > 180)
            {
                angle -= 360;
            }

            return angle;
        }

        protected void SetAngle(double second)
        {
            var angle = GetAngle(second);

            Angle = angle;
            BeginAnimation(NumberAngleProperty, null);
            NumberAngle = GetNumberAngle(angle);
        }

        protected void AnimateAngle(double second, double duration)
        {
            var angle = GetAngle(second);

            if (angle < 0 && prevAngle > 0)
            {
                prevAngle -= 360;
            }

            DoubleAnimation animation1 = new DoubleAnimation(prevAngle, angle, new Duration(TimeSpan.FromMilliseconds(duration)));
            BeginAnimation(AngleProperty, animation1);

            if (NumberRotation == NumberRotation.Horizontal)
            {
                DoubleAnimation animation2 = new DoubleAnimation(-prevAngle, -angle, new Duration(TimeSpan.FromMilliseconds(duration)));
                BeginAnimation(NumberAngleProperty, animation2);
            }

            prevAngle = angle;
        }

        private void OnNumberRotationChanged()
        {
            BeginAnimation(NumberAngleProperty, null);
            NumberAngle = GetNumberAngle(Angle);
        }

        private double GetNumberAngle(double angle)
        {
            double numberAngle = 0;

            switch (NumberRotation)
            {
                case NumberRotation.Tangent:
                    numberAngle = 90;
                    break;

                case NumberRotation.Horizontal:
                    numberAngle = -angle;
                    break;

                case NumberRotation.TangentUpside:
                    numberAngle = 90;

                    if (angle > 0 && angle < 180)
                    {
                        numberAngle = -90;
                    }

                    break;
            }

            return numberAngle;
        }
    }

    public abstract class HourHandBase : AngleBase
    {
        public abstract double Hour { get; set; }
    }

    public abstract class MinuteHandBase : AngleBase
    {
        public abstract double Minute { get; set; }
    }

    public abstract class SecondHandBase : AngleBase
    {
        public abstract double Second { get; set; }

        public abstract int Duration { get; set; }
    }

    public abstract class HourTickBase : AngleBase
    {
        public abstract int Hour { get; set; }
    }

    public abstract class MinuteTickBase : AngleBase
    {
        public abstract int Minute { get; set; }
    }

    public abstract class SecondMajorTickBase : AngleBase
    {
        public abstract int Second { get; set; }
    }
}
