using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HighlightDatePickerDemo
{

    public static class CalandarHelper
    {
        public static readonly DependencyProperty SingleClickDefocusProperty =
            DependencyProperty.RegisterAttached("SingleClickDefocus", typeof(bool), typeof(Calendar)
            , new FrameworkPropertyMetadata(false, new PropertyChangedCallback(SingleClickDefocusChanged)));

        public static bool GetSingleClickDefocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(SingleClickDefocusProperty);
        }

        public static void SetSingleClickDefocus(DependencyObject obj, bool value)
        {
            obj.SetValue(SingleClickDefocusProperty, value);
        }

        private static void SingleClickDefocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Calendar)
            {
                Calendar calendar = d as Calendar;
                calendar.PreviewMouseDown += (a, b) =>
                {
                    if (Mouse.Captured is Calendar || Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
                    {
                        Mouse.Capture(null);
                    }
                };
            }
        }
    }

    public class CustomCalendar : Calendar
    {
        static CustomCalendar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomCalendar), new FrameworkPropertyMetadata(typeof(CustomCalendar)));
        }

        public static readonly DependencyProperty HighlightedDatesProperty = DependencyProperty.Register("HighlightedDates", typeof(IList<HighlightedDate>), typeof(CustomCalendar));

        public IList<HighlightedDate> HighlightedDates
        {
            get { return (IList<HighlightedDate>)GetValue(HighlightedDatesProperty); }
            set { SetValue(HighlightedDatesProperty, value); }
        }

        public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.Register("HighlightBrush", typeof(Brush),
            typeof(CustomCalendar), new PropertyMetadata(Brushes.Orange));

        public Brush HighlightBrush
        {
            get { return (Brush)GetValue(HighlightBrushProperty); }
            set { SetValue(HighlightBrushProperty, value); }
        }
    }

    public class HighlightDatePicker : Calendar
    {
        static HighlightDatePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HighlightDatePicker), new FrameworkPropertyMetadata(typeof(HighlightDatePicker)));
        }

        public static readonly DependencyProperty HighlightedDatesProperty = DependencyProperty.Register("HighlightedDates", typeof(IList<HighlightedDate>), typeof(HighlightDatePicker));

        public IList<HighlightedDate> HighlightedDates
        {
            get { return (IList<HighlightedDate>)GetValue(HighlightedDatesProperty); }
            set { SetValue(HighlightedDatesProperty, value); }
        }

        public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.Register("HighlightBrush", typeof(Brush),
            typeof(HighlightDatePicker), new PropertyMetadata(Brushes.Orange));

        public Brush HighlightBrush
        {
            get { return (Brush)GetValue(HighlightBrushProperty); }
            set { SetValue(HighlightBrushProperty, value); }
        }
    }

    public class HighlightedDate
    {
        public HighlightedDate(DateTime date, string description)
        {
            Date = date;
            Description = description;
        }

        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
