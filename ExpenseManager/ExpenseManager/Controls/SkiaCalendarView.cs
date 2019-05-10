using ExpenseManager.Extensions;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class SkiaCalendarView : SKCanvasView
    {
        #region Bindable Properties

        #region SeparatorColor property

        public static readonly BindableProperty SeparatorColorProperty = BindableProperty.Create(
            nameof(SeparatorColor),
            typeof(Color),
            typeof(SkiaCalendarView),
            Color.Transparent);

        public Color SeparatorColor
        {
            get => (Color) GetValue(SeparatorColorProperty);
            set => SetValue(SeparatorColorProperty, value);
        }

        #endregion SeparatorColor property

        #region SeparatorWidth property

        public static readonly BindableProperty SeparatorWidthProperty = BindableProperty.Create(
            nameof(SeparatorWidth),
            typeof(float),
            typeof(SkiaCalendarView),
            0f);

        public float SeparatorWidth
        {
            get => (float) GetValue(SeparatorWidthProperty);
            set => SetValue(SeparatorWidthProperty, value);
        }

        #endregion SeparatorWidth property

        #region PrimaryTextColor property

        public static readonly BindableProperty PrimaryTextColorProperty = BindableProperty.Create(
            nameof(PrimaryTextColor),
            typeof(Color),
            typeof(SkiaCalendarView),
            Color.Black);

        public Color PrimaryTextColor
        {
            get => (Color) GetValue(PrimaryTextColorProperty);
            set => SetValue(PrimaryTextColorProperty, value);
        }

        #endregion PrimaryTextColor property

        #region SecondaryTextColor property

        public static readonly BindableProperty SecondaryTextColorProperty = BindableProperty.Create(
            nameof(SecondaryTextColor),
            typeof(Color),
            typeof(SkiaCalendarView),
            Color.Gray);

        public Color SecondaryTextColor
        {
            get => (Color) GetValue(SecondaryTextColorProperty);
            set => SetValue(SecondaryTextColorProperty, value);
        }

        #endregion SecondaryTextColor property

        #region MinDate property

        public static readonly BindableProperty MinDateProperty = BindableProperty.Create(
            nameof(MinDate),
            typeof(DateTime),
            typeof(SkiaCalendarView),
            DateTime.MinValue);

        public DateTime MinDate
        {
            get => (DateTime) GetValue(MinDateProperty);
            set => SetValue(MinDateProperty, value);
        }

        #endregion MinDate property

        #region MaxDate property

        public static readonly BindableProperty MaxDateProperty = BindableProperty.Create(
            nameof(MaxDate),
            typeof(DateTime),
            typeof(SkiaCalendarView),
            DateTime.MaxValue);

        public DateTime MaxDate
        {
            get => (DateTime) GetValue(MaxDateProperty);
            set => SetValue(MaxDateProperty, value);
        }

        #endregion MaxDate property

        #region CurrentDate property

        public static readonly BindableProperty CurrentDateProperty = BindableProperty.Create(
            nameof(CurrentDate),
            typeof(DateTime),
            typeof(SkiaCalendarView),
            DateTime.Today,
            BindingMode.OneTime);

        public DateTime CurrentDate
        {
            get => (DateTime) GetValue(CurrentDateProperty);
            set => SetValue(CurrentDateProperty, value);
        }

        #endregion CurrentDate property

        #region SelectedDate property

        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(
            nameof(SelectedDate),
            typeof(DateTime),
            typeof(SkiaCalendarView),
            DateTime.Today);

        public DateTime SelectedDate
        {
            get => (DateTime) GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        #endregion SelectedDate property

        #endregion Bindable Properties

        public SKRect DaysSectionRect;
        public readonly Dictionary<int, SKRect> DaysRectDictionary = new Dictionary<int, SKRect>();

        public int DaySectionRowsCount;
        public int MaxDaySectionRowsCount = 6;
        public int DaySectionColumnsCount = 7;

        public float selectedDateSectionHeight;
        public float selectedDateSectionTopPadding;

        public float dayOfWeekSectionHeight;
        public float dayOfWeekSectionTopPadding;

        public float daysSectionHeight;
        public float daysSectionTopPadding;

        public SkiaCalendarView()
        {
            PaintSurface += CanvasViewOnPaintSurface;
            EnableTouchEvents = true;
            //Touch += OnTouch;

            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.NumberOfTapsRequired = 1;
            tapGestureRecognizer.Tapped += TapGestureRecognizerOnTapped;

            SwipeGestureRecognizer leftSwipeGestureRecognizer = new SwipeGestureRecognizer();
            leftSwipeGestureRecognizer.Direction = SwipeDirection.Left;
            leftSwipeGestureRecognizer.Swiped += LeftSwipeGestureRecognizerOnSwiped;

            SwipeGestureRecognizer rightSwipeGestureRecognizer = new SwipeGestureRecognizer();
            rightSwipeGestureRecognizer.Direction = SwipeDirection.Right;
            rightSwipeGestureRecognizer.Swiped += RightSwipeGestureRecognizerOnSwiped;

            GestureRecognizers.Add(tapGestureRecognizer);
            GestureRecognizers.Add(leftSwipeGestureRecognizer);
            GestureRecognizers.Add(rightSwipeGestureRecognizer);
        }

        private void RightSwipeGestureRecognizerOnSwiped(object sender, SwipedEventArgs e)
        {
            Debug.WriteLine("===== right");
        }

        private void LeftSwipeGestureRecognizerOnSwiped(object sender, SwipedEventArgs e)
        {
            Debug.WriteLine("===== left");
        }

        private void TapGestureRecognizerOnTapped(object sender, EventArgs e)
        {
            Debug.WriteLine("===== tap");
        }

        #region Private methods

        private void CanvasViewOnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            selectedDateSectionHeight = info.Height / 20f;
            selectedDateSectionTopPadding =
                CalculateTopPadding(0, 0);
            dayOfWeekSectionHeight = info.Height / 20f;
            dayOfWeekSectionTopPadding =
                CalculateTopPadding(selectedDateSectionTopPadding, selectedDateSectionHeight);
            daysSectionHeight = info.Height - dayOfWeekSectionHeight - selectedDateSectionHeight;
            daysSectionTopPadding =
                CalculateTopPadding(dayOfWeekSectionTopPadding, dayOfWeekSectionHeight);

            DrawSelectedDateSection(canvas, info.Width, selectedDateSectionHeight, selectedDateSectionTopPadding);
            DrawDaysOfWeekSection(canvas, info.Width, dayOfWeekSectionHeight, dayOfWeekSectionTopPadding);
            DrawDaysSection(canvas, info.Width, daysSectionHeight, daysSectionTopPadding);
        }

        private void DrawSelectedDateSection(SKCanvas canvas, float width, float height, float topPadding)
        {
            SKPaint datePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SeparatorColor.ToSKColor(),
                StrokeWidth = SeparatorWidth
            };

            SKRect dateRect = new SKRect
            {
                Size = new SKSize(width, height),
            };

            dateRect.Location = new SKPoint(0, topPadding);

            canvas.DrawRect(dateRect, datePaint);

            DrawTextInRectangle(canvas, dateRect, SelectedDate.Month + " " + SelectedDate.Year,
                PrimaryTextColor.ToSKColor());
        }

        private void DrawDaysOfWeekSection(SKCanvas canvas, float width, float height, float topPadding)
        {
            SKPaint dayPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SeparatorColor.ToSKColor(),
                StrokeWidth = SeparatorWidth
            };

            SKRect dayRect = new SKRect
            {
                Size = new SKSize(width / DaySectionColumnsCount, height),
            };

            for (var column = 0; column < DaySectionColumnsCount; column++)
            {
                dayRect.Location = new SKPoint(dayRect.Width * column, topPadding);

                canvas.DrawRect(dayRect, dayPaint);

                DrawTextInRectangle(canvas, dayRect, ((DayOfWeek) column).ToString(), PrimaryTextColor.ToSKColor());
            }
        }

        private void DrawDaysSection(SKCanvas canvas, float width, float height, float topPadding)
        {
            DaySectionRowsCount = CurrentDate.NumberOfWeeksInMonthRows();

            SKPaint itemPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SeparatorColor.ToSKColor(),
                StrokeWidth = SeparatorWidth
            };

            SKRect itemRect = new SKRect
            {
                Size = new SKSize(width / DaySectionColumnsCount, height / MaxDaySectionRowsCount)
            };

            SKPaint containerPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Red
            };

            DaysSectionRect = new SKRect
            {
                Left = 0,
                Right = width,
                Top = daysSectionTopPadding,
                Bottom = daysSectionTopPadding + itemRect.Height * DaySectionRowsCount
            };

            canvas.DrawRect(DaysSectionRect, containerPaint);

            var startMonthDate = SelectedDate.ToFirstDayOfMonth();
            var currentDate = CurrentDate.Date;
            var endMonthDate = SelectedDate.ToLastDateOfMonth();

            var tempDate = startMonthDate;
            var previousDate = endMonthDate.AddMonths(-1).ToStartOfWeek(DayOfWeek.Sunday);
            var nextDate = startMonthDate.AddMonths(1);

            for (var row = 0; row < DaySectionRowsCount; row++)
            {
                for (var column = 0; column < DaySectionColumnsCount; column++)
                {
                    itemRect.Location = new SKPoint(itemRect.Width * column, itemRect.Height * row + topPadding);

                    if ((DayOfWeek) column < startMonthDate.DayOfWeek && row == 0)
                    {
                        DrawTextInRectangle(canvas, itemRect, previousDate.Day.ToString(),
                            SecondaryTextColor.ToSKColor());

                        previousDate = previousDate.AddDays(1);
                    }
                    //else if (tempDate.ToPreviousDay().Month == startMonthDate.Month &&
                    //         tempDate.ToPreviousDay().IsLastDayOfMonth() &&
                    //         nextDate.IsFirstDayOfWeek(DayOfWeek.Sunday))
                    //{
                    //    break;
                    //}
                    else if (tempDate.ToPreviousDay().Month == startMonthDate.Month &&
                             tempDate.ToPreviousDay().IsLastDayOfMonth())
                    {
                        DrawTextInRectangle(canvas, itemRect, nextDate.Day.ToString(),
                            SecondaryTextColor.ToSKColor());

                        nextDate = nextDate.ToNextDay();
                    }
                    else
                    {
                        DrawTextInRectangle(canvas, itemRect, tempDate.Day.ToString(),
                            PrimaryTextColor.ToSKColor());
                        tempDate = tempDate.ToNextDay();
                    }

                    canvas.DrawRect(itemRect, itemPaint);

                    var key = row * DaySectionColumnsCount + column;

                    DaysRectDictionary.Add(key, itemRect);
                }
            }
        }

        private void DrawTextInRectangle(SKCanvas canvas, SKRect rect, string text, SKColor textColor)
        {
            SKPaint textPaint = new SKPaint
            {
                Color = textColor
            };

            float textWidth = textPaint.MeasureText(text);
            //textPaint.TextSize = (rect.Width * textPaint.TextSize / textWidth > rect.Height) 
            //    ? 0.7f * rect.Height * textPaint.TextSize / textWidth
            //    : 0.7f * rect.Width * textPaint.TextSize / textWidth;

            textPaint.TextSize = 40;

            // Find the text bounds
            SKRect textBounds = new SKRect();
            textPaint.MeasureText(text, ref textBounds);

            // Calculate offsets to center the text on the itemRect
            float xText = rect.MidX - textBounds.MidX;
            float yText = rect.MidY - textBounds.MidY;

            canvas.DrawText(text, xText, yText, textPaint);
        }

        private static float CalculateTopPadding(float previousTopPadding, float previousHeight) =>
            previousHeight + previousTopPadding;

        private void DayRect_OnClick(SKRect daysSectionRect, int rowsCount, SKPoint touchLocation)
        {
            if (!daysSectionRect.Contains(touchLocation))
                return;

            var itemWidth = daysSectionRect.Width / DaySectionColumnsCount;
            var itemHeight = daysSectionRect.Height / rowsCount;

            var tempX = touchLocation.X / itemWidth;
            var tempY = (touchLocation.Y - daysSectionTopPadding) / itemHeight;

            int xIndex = (int) Math.Floor(tempX);
            int yIndex = (int) Math.Floor(tempY);

            var rectangleIndex = yIndex * DaySectionColumnsCount + xIndex;

            var temp = DaysRectDictionary[rectangleIndex];
        }

        #endregion Private methods

        #region SkiaUtils

        List<long> touchIds = new List<long>();

        public bool isMoved = false;
        public SKPoint startPoint;
        public SKPoint finishPoint;

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            Debug.WriteLine(" ------------- OnTouch");

            if (sender == null)
                return;

            e.Handled = true;

            SKPoint touchLocation = e.Location;

            startPoint = touchLocation;
            finishPoint = startPoint;


            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    Debug.WriteLine("=== Pressed");
                    if (HitTest(touchLocation))
                    {
                        touchIds.Add(e.Id);
                    }
                    break;
                case SKTouchAction.Moved:
                    Debug.WriteLine("=== Moved");
                    isMoved = true;
                    break;
                case SKTouchAction.Released:
                    Debug.WriteLine("=== Released");
                    if (touchIds.Contains(e.Id))
                    {
                        touchIds.Remove(e.Id);

                        if (!isMoved)
                            DayRect_OnClick(DaysSectionRect, DaySectionRowsCount, touchLocation);
                    }
                    isMoved = false;
                    break;
                case SKTouchAction.Cancelled:
                    Debug.WriteLine("=== Cancelled");
                    if (touchIds.Contains(e.Id))
                    {
                        touchIds.Remove(e.Id);
                    }
                    break;
                case SKTouchAction.Exited:
                    Debug.WriteLine("=== Exited");
                    if (touchIds.Contains(e.Id))
                    {
                        touchIds.Remove(e.Id);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool HitTest(SKPoint touchLocation)
        {
            return new SKRect(0, 0, CanvasSize.Width, CanvasSize.Height)
                .Contains(touchLocation);
        }

        #endregion SkiaUtils
    }
}