namespace ResultsModule.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public partial class LuckyNumberControl : UserControl
{
    private static readonly DependencyProperty IsDrawingProperty = DependencyProperty.Register(
         nameof(IsDrawing),
         typeof(bool),
         typeof(LuckyNumberControl),
         new FrameworkPropertyMetadata(false, OnDrawingStatusChanged));

    public LuckyNumberControl()
    {
        this.InitializeComponent();
    }

    public bool IsDrawing
    {
        get => (bool)GetValue(IsDrawingProperty);
        set => SetValue(IsDrawingProperty, value);
    }

    private static void OnDrawingStatusChanged(DependencyObject target,
                                               DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue != null)
        {
            (target as LuckyNumberControl)?.UpdateStatus((bool)e.NewValue);
        }
    }

    private void UpdateStatus(bool isDrawing)
    {
        this.IsDrawing = isDrawing;
        if (isDrawing)
        {
            this.Background = new SolidColorBrush(Color.FromRgb(252, 127, 3));
            return;
        }
        this.Background = new SolidColorBrush(Color.FromRgb(0, 155, 0));
    }
}