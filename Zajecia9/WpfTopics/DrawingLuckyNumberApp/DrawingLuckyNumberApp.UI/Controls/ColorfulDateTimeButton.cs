namespace DrawingLuckyNumberApp.UI.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public class ColorfulDateTimeButton : Button
{
    private static readonly DependencyProperty ClickDateTimeValueProperty = DependencyProperty.Register(
        nameof(ClickDateTimeValue), 
        typeof(DateTime), 
        typeof(ColorfulDateTimeButton),
        new PropertyMetadata(DateTime.Now.AddDays(-2), OnPropertyValueChanged));

    private readonly Random random = new Random();

    public ColorfulDateTimeButton()
    {
        this.Background = GetRandomBrush();
    }

    public DateTime ClickDateTimeValue
    {
        get => (DateTime)GetValue(ClickDateTimeValueProperty);
        set => SetValue(
                        ClickDateTimeValueProperty, 
                        value);
    }

    protected override void OnClick()
    {
        this.ClickDateTimeValue = DateTime.Now;
        this.Background = GetRandomBrush();
        base.OnClick();
    }

    private SolidColorBrush GetRandomBrush()
    {
        return new SolidColorBrush(Color.FromRgb(
                                                 (byte)this.random.Next(1, 255),
                                                 (byte)this.random.Next(1, 255),
                                                 (byte)this.random.Next(1, 255)));
    }

    private static void OnPropertyValueChanged(
        DependencyObject target,
        DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue != null)
        {
            (target as ColorfulDateTimeButton)?.UpdateDateTime((DateTime)e.NewValue);
        }
    }

    private void UpdateDateTime(DateTime dateTime)
    {
        if (dateTime <= DateTime.Now)
        {
            this.ClickDateTimeValue = dateTime;
        }
    }
}