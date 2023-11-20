namespace DrawingLuckyNumberModule.Views;

using System.Windows.Controls;
using DrawingLuckyNumber.Infrastructure;
using ViewModels;

public partial class DrawLuckyNumberView : UserControl
{
    public DrawLuckyNumberView()
    {
        this.InitializeComponent();
        DataContext = new DrawLuckyNumberViewModel(ShellBootstrapper.EventAggregator);
    }
}