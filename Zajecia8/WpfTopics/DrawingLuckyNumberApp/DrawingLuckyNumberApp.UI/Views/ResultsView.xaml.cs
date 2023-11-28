namespace DrawingLuckyNumberApp.UI.Views;

using System.Windows.Controls;
using DrawingLuckyNumber.Infrastructure;
using ResultsModule.ViewModels;

public partial class ResultsView : UserControl
{
    public ResultsView()
    {
        this.InitializeComponent();
        DataContext = new ResultsViewModel(ShellBootstrapper.EventAggregator);
    }
}