namespace DrawingLuckyNumberApp.UI.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public class ColorfulDateTimeButton : Button
{
    private static readonly DependencyProperty ClickDateTimeValueProperty = DependencyProperty.Register(
        nameof(ClickDateTimeValue), 
        typeof(DateTime), 
        typeof(ColorfulDateTimeButton));

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
}