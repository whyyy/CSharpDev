namespace DrawingLuckyNumberApp.UI.Behaviors
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Microsoft.Xaml.Behaviors;

    public class ButtonHoverBehavior : Behavior<Button>
    {
        private Thickness thicknessToRestore;
        private double fontSizeToRestore;

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseEnter += this.ApplyHoverStyle;
            this.AssociatedObject.MouseLeave += this.RestoreStyle;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseLeave -= this.RestoreStyle;
            this.AssociatedObject.MouseEnter -= this.ApplyHoverStyle;
        }

        private void ApplyHoverStyle(
            object sender,
            MouseEventArgs args)
        {
            this.thicknessToRestore = this.AssociatedObject.BorderThickness;
            this.fontSizeToRestore = this.AssociatedObject.FontSize;

            this.AssociatedObject.BorderThickness = new Thickness(5);
            this.AssociatedObject.FontSize = 18;
        }

        private void RestoreStyle(
            object sender,
            MouseEventArgs args)
        {
            this.AssociatedObject.BorderThickness = this.thicknessToRestore;
            this.AssociatedObject.FontSize = this.fontSizeToRestore;
        }
    }
}