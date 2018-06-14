using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace HighlightDatePickerDemo
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private IList<HighlightedDate> _highlightedDates;

        public MainWindow()
        {
            SelectedDate = DateTime.Now.Date;
            HighlightedDates = new List<HighlightedDate>
            {
                new HighlightedDate(DateTime.Today.AddDays(-3), "My birthday"),
                new HighlightedDate(DateTime.Today.AddDays(-2), "Wedding anniversary")
            };

            InitializeComponent();
        }

        private string holidayName;
        private DateTime holidayNameDate;
        public string HolidayName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(holidayName) && DateTime.Compare(holidayNameDate.Date, selectedDate.Date) == 0)
                {
                    return holidayName;
                }
                holidayName = null;
                return _highlightedDates
                    .FirstOrDefault(h => 
                        DateTime.Compare(h.Date.Date, selectedDate.Date) == 0)?.Description ?? "";
            }
            set
            {
                holidayName = value;
                holidayNameDate = selectedDate;
                OnPropertyChanged(() => HolidayName);
            }
        }

        private DateTime selectedDate;
        public DateTime SelectedDate {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged(() => SelectedDate);
                OnPropertyChanged(() => HolidayName);
            }
        }

        public IList<HighlightedDate> HighlightedDates
        {
            get { return _highlightedDates; }
            set
            {
                _highlightedDates = value;
                OnPropertyChanged(() => HighlightedDates);
            }
        }

        private void RefreshHolidays_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedDate = DateTime.Today.Date;
        }

        private void SetHoliday_OnClick(object sender, RoutedEventArgs e)
        {
            var before = _highlightedDates.ToArray();
            var found = _highlightedDates.FirstOrDefault(h => DateTime.Compare(h.Date.Date, SelectedDate.Date) == 0);
            if (found == null)
            {
                HighlightedDates = new ObservableCollection<HighlightedDate>(before)
                {
                    new HighlightedDate(SelectedDate.Date,holidayName)
                };
            } else
            {
                found.Description = holidayName;
                HolidayName = null;
            }
        }

        private void DeleteHoliday_OnClick(object sender, RoutedEventArgs e)
        {
            var before = _highlightedDates.ToList();
            var found = _highlightedDates.FirstOrDefault(h => DateTime.Compare(h.Date.Date, SelectedDate.Date) == 0);
            if (found != null)
            {
                before.Remove(found);
                HighlightedDates = before;
                HolidayName = null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var body = propertyExpression.Body as MemberExpression;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(body.Member.Name));
        }
    }
}
