namespace ResultsModule.ViewModels;

using DrawingLuckyNumber.Core;
using Prism.Events;
using Prism.Mvvm;

public class ResultsViewModel : BindableBase
{
    private string drawingStatus;
    private string luckyNumber;
    private string drawingTotalTimeInSeconds;

    public ResultsViewModel(IEventAggregator eventAggregator)
    {
        eventAggregator.GetEvent<IsDrawingInProgressEvent>().Subscribe(this.DrawingStatusReceived);
        eventAggregator.GetEvent<LuckyNumberDrawnEvent>().Subscribe(this.LuckyNumberReceived);
        eventAggregator.GetEvent<DrawingTotalTimeInSecondsEvent>().Subscribe(this.DrawingTotalTimeInSecondsReceived);
    }

    public string DrawingStatus
    {
        get => this.drawingStatus;
        set => SetProperty(ref this.drawingStatus, value);
    }

    public string LuckyNumber
    {
        get => this.luckyNumber;
        set => SetProperty(ref this.luckyNumber, value);
    }

    public string DrawingTotalTimeInSeconds
    {
        get => this.drawingTotalTimeInSeconds;
        set => SetProperty(ref this.drawingTotalTimeInSeconds, value);
    }

    private void DrawingStatusReceived(bool receivedDrawingStatus)
    {
        this.DrawingStatus = receivedDrawingStatus ? "Drawing is in progress" : "";
    }

    private void LuckyNumberReceived(int receivedLuckyNumber)
    {
        if (receivedLuckyNumber == 0)
        {
            this.LuckyNumber = "";
            return;
        }

        this.LuckyNumber = $"Lucky number is: {receivedLuckyNumber}";
    }

    private void DrawingTotalTimeInSecondsReceived(double totalTimeInSeconds)
    {
        if (totalTimeInSeconds == 0)
        {
            this.DrawingTotalTimeInSeconds = "";
            return;
        }

        this.DrawingTotalTimeInSeconds = $"Drawing total time: {totalTimeInSeconds} seconds";
    }
}