using DrawingLuckyNumberModule.ViewModels;

namespace DrawingLuckyNumberModule.Views
{
    using System.Windows.Controls;
    using DrawingLuckyNumber.Infrastructure;

    public partial class DrawLuckyNumberView : UserControl
    {
        public DrawLuckyNumberView()
        {
            this.InitializeComponent();
            DataContext = new DrawLuckyNumberViewModel(ShellBootstrapper.EventAggregator);
        }
    }
}