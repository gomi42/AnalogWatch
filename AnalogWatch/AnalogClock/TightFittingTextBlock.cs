using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace AnalogWatch.AnalogClock
{
    public class TightFittingTextBlock : FrameworkElement
    {
        /// <summary>
        /// DependencyProperty for <see cref="FontFamily" /> property.
        /// </summary>
        public static readonly DependencyProperty FontFamilyProperty =
                TextElement.FontFamilyProperty.AddOwner(typeof(TightFittingTextBlock));

        /// <summary>
        /// The FontFamily property specifies the name of font family.
        /// </summary>
        [Localizability(LocalizationCategory.Font)]
        [Bindable(true)]
        [Category("Font")]
        public FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        /// <summary>
        /// DependencyProperty setter for <see cref="FontFamily" /> property.
        /// </summary>
        /// <param name="element">The element to which to write the attached property.</param>
        /// <param name="value">The property value to set</param>
        public static void SetFontFamily(DependencyObject element, FontFamily value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// DependencyProperty getter for <see cref="FontFamily" /> property.
        /// </summary>
        /// <param name="element">The element from which to read the attached property.</param>
        public static FontFamily GetFontFamily(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return (FontFamily)element.GetValue(FontFamilyProperty);
        }

        /// <summary>
        /// DependencyProperty for <see cref="FontStyle" /> property.
        /// </summary>
        public static readonly DependencyProperty FontStyleProperty =
                TextElement.FontStyleProperty.AddOwner(typeof(TightFittingTextBlock));

        /// <summary>
        /// The FontStyle property requests normal, italic, and oblique faces within a font family.
        /// </summary>
        [Category("Font")]
        public FontStyle FontStyle
        {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        /// <summary>
        /// DependencyProperty setter for <see cref="FontStyle" /> property.
        /// </summary>
        /// <param name="element">The element to which to write the attached property.</param>
        /// <param name="value">The property value to set</param>
        public static void SetFontStyle(DependencyObject element, FontStyle value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(FontStyleProperty, value);
        }

        /// <summary>
        /// DependencyProperty getter for <see cref="FontStyle" /> property.
        /// </summary>
        /// <param name="element">The element from which to read the attached property.</param>
        public static FontStyle GetFontStyle(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return (FontStyle)element.GetValue(FontStyleProperty);
        }

        /// <summary>
        /// DependencyProperty for <see cref="FontWeight" /> property.
        /// </summary>
        public static readonly DependencyProperty FontWeightProperty =
                TextElement.FontWeightProperty.AddOwner(typeof(TightFittingTextBlock));

        /// <summary>
        /// The FontWeight property specifies the weight of the font.
        /// </summary>
        [Category("Font")]
        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        /// <summary>
        /// DependencyProperty setter for <see cref="FontWeight" /> property.
        /// </summary>
        /// <param name="element">The element to which to write the attached property.</param>
        /// <param name="value">The property value to set</param>
        public static void SetFontWeight(DependencyObject element, FontWeight value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(FontWeightProperty, value);
        }

        /// <summary>
        /// DependencyProperty getter for <see cref="FontWeight" /> property.
        /// </summary>
        /// <param name="element">The element from which to read the attached property.</param>
        public static FontWeight GetFontWeight(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return (FontWeight)element.GetValue(FontWeightProperty);
        }

        /// <summary>
        /// DependencyProperty for <see cref="FontStretch" /> property.
        /// </summary>
        public static readonly DependencyProperty FontStretchProperty =
                TextElement.FontStretchProperty.AddOwner(typeof(TightFittingTextBlock));

        /// <summary>
        /// The FontStretch property selects a normal, condensed, or extended face from a font family.
        /// </summary>
        [Category("Font")]
        public FontStretch FontStretch
        {
            get { return (FontStretch)GetValue(FontStretchProperty); }
            set { SetValue(FontStretchProperty, value); }
        }

        /// <summary>
        /// DependencyProperty setter for <see cref="FontStretch" /> property.
        /// </summary>
        /// <param name="element">The element to which to write the attached property.</param>
        /// <param name="value">The property value to set</param>
        public static void SetFontStretch(DependencyObject element, FontStretch value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(FontStretchProperty, value);
        }

        /// <summary>
        /// DependencyProperty getter for <see cref="FontStretch" /> property.
        /// </summary>
        /// <param name="element">The element from which to read the attached property.</param>
        public static FontStretch GetFontStretch(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return (FontStretch)element.GetValue(FontStretchProperty);
        }

        /// <summary>
        /// DependencyProperty for <see cref="FontSize" /> property.
        /// </summary>
        public static readonly DependencyProperty FontSizeProperty =
                TextElement.FontSizeProperty.AddOwner(
                        typeof(TightFittingTextBlock));

        /// <summary>
        /// The FontSize property specifies the size of the font.
        /// </summary>
        [TypeConverter(typeof(FontSizeConverter))]
        [Localizability(LocalizationCategory.None)]
        [Category("Font")]
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        /// <summary>
        /// DependencyProperty setter for <see cref="FontSize" /> property.
        /// </summary>
        /// <param name="element">The element to which to write the attached property.</param>
        /// <param name="value">The property value to set</param>
        public static void SetFontSize(DependencyObject element, double value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// DependencyProperty getter for <see cref="FontSize" /> property.
        /// </summary>
        /// <param name="element">The element from which to read the attached property.</param>
        [TypeConverter(typeof(FontSizeConverter))]
        public static double GetFontSize(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return (double)element.GetValue(FontSizeProperty);
        }

        /// <summary>
        /// DependencyProperty for <see cref="Foreground" /> property.
        /// </summary>
        [Category("Brush")]
        public static readonly DependencyProperty ForegroundProperty =
                TextElement.ForegroundProperty.AddOwner(
                        typeof(TightFittingTextBlock));

        /// <summary>
        /// The Foreground property specifies the foreground brush of an element's text content.
        /// </summary>
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        /// <summary>
        /// DependencyProperty setter for <see cref="Foreground" /> property.
        /// </summary>
        /// <param name="element">The element to which to write the attached property.</param>
        /// <param name="value">The property value to set</param>
        public static void SetForeground(DependencyObject element, Brush value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(ForegroundProperty, value);
        }

        /// <summary>
        /// DependencyProperty getter for <see cref="Foreground" /> property.
        /// </summary>
        /// <param name="element">The element from which to read the attached property.</param>
        public static Brush GetForeground(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return (Brush)element.GetValue(ForegroundProperty);
        }

        [Category("Common")]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TightFittingTextBlock), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        protected override Size MeasureOverride(Size availableSize)
        {
            formattedText = new FormattedText(Text,
                                                  CultureInfo.InvariantCulture,
                                                  FlowDirection.LeftToRight,
                                                  new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                                                  FontSize,
                                                  Foreground,
                                                  96);

            return new Size(formattedText.Width - formattedText.OverhangLeading - formattedText.OverhangTrailing, formattedText.Extent);
        }

        FormattedText formattedText;

        protected override void OnRender(DrawingContext dc)
        {
            //dc.DrawRectangle(null, new Pen(Brushes.DarkMagenta, 1), new Rect(0, 0, formattedText.Width - formattedText.OverhangLeading - formattedText.OverhangTrailing, formattedText.Extent));
            dc.DrawText(formattedText, new Point(-formattedText.OverhangLeading, -(formattedText.Height - formattedText.Extent + formattedText.OverhangAfter)));
        }
    }
}
