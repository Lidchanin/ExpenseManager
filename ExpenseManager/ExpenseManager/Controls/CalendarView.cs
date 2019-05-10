using ExpenseManager.Extensions;
using System;
using Xamarin.Forms;

namespace ExpenseManager.Controls
{
    public class CalendarView : Grid
    {
        #region Temp Props

        private DateTime CurrentDate = new DateTime(2009, 2, 1);

        #endregion Temp Props

        public CalendarView()
        {
            //ColumnDefinitions = new ColumnDefinitionCollection
            //{
            //    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
            //    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
            //    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
            //    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
            //    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
            //    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
            //    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}
            //};

            //RowDefinitions = new RowDefinitionCollection
            //{
            //    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},

            //    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
            //    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
            //    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
            //};

            var startMonthDate = CurrentDate.ToFirstDayOfMonth();
            var currentDate = CurrentDate.Date;
            var endMonthDate = CurrentDate.ToLastDateOfMonth();

            var tempDate = startMonthDate;
            var previousDate = endMonthDate.AddMonths(-1).ToStartOfWeek(DayOfWeek.Sunday);
            var nextDate = startMonthDate.AddMonths(1);

            var tempLabel = new Label();

            for (var row = 0; row < 7; row++)
            {
                for (var column = 0; column < 7; column++)
                {
                    if (row == 0)
                    {
                        Label dayOfMonthLabel = new Label
                        {
                            Text = ((DayOfWeek) column).ToString(),
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            BackgroundColor = Color.CadetBlue
                        };
                        Children.Add(dayOfMonthLabel, column, column + 1, row, row + 1);
                    }
                    else
                    {
                        if ((DayOfWeek) column < startMonthDate.DayOfWeek && row == 1)
                        {
                            tempLabel.Text = previousDate.ToShortDateString();
                            tempLabel.BackgroundColor = Color.Red;
                            tempLabel.TextColor = Color.White;
                            previousDate = previousDate.AddDays(1);
                        }
                        else if (tempDate.ToPreviousDay().Month == startMonthDate.Month &&
                                 tempDate.ToPreviousDay().IsLastDayOfMonth() &&
                                 nextDate.IsFirstDayOfWeek(DayOfWeek.Sunday))
                        {
                            tempLabel.Text = "Deleted row";
                            tempLabel.BackgroundColor = Color.White;
                            tempLabel.TextColor = Color.Black;
                            //break;
                        }
                        else if (tempDate.ToPreviousDay().Month == startMonthDate.Month &&
                                 tempDate.ToPreviousDay().IsLastDayOfMonth())
                        {
                            tempLabel.Text = nextDate.ToShortDateString();
                            tempLabel.BackgroundColor = Color.Green;
                            tempLabel.TextColor = Color.White;
                            nextDate = nextDate.ToNextDay();
                        }
                        else
                        {
                            tempLabel.Text = tempDate.ToShortDateString();
                            tempLabel.BackgroundColor = Color.BlueViolet;
                            tempLabel.TextColor = Color.Black;
                            tempDate = tempDate.ToNextDay();
                        }

                        Children.Add(new StackLayout
                            {
                                Children =
                                {
                                    new Label
                                    {
                                        BackgroundColor = tempLabel.BackgroundColor,
                                        TextColor = tempLabel.TextColor,
                                        Text = tempLabel.Text
                                    }
                                }
                            },
                            column, column + 1, row, row + 1);
                    }
                }
            }
        }
    }
}
