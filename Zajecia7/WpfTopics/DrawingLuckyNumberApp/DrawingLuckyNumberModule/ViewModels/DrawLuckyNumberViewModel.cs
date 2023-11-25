namespace DrawingLuckyNumberModule.ViewModels;

using System;
using System.Threading.Tasks;
using DrawingLuckyNumber.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

public class DrawLuckyNumberViewModel : BindableBase
{
    private CancellationTokenSource cancellationTokenSource;
    private bool startIsEnabled;
    private bool stopIsEnabled;

    private readonly IEventAggregator eventAggregator;

    public DrawLuckyNumberViewModel(IEventAggregator eventAggregator)
    {
        this.StartDrawingCommand = new DelegateCommand(this.StartDrawing);
        this.StopDrawingCommand = new DelegateCommand(this.StopDrawing);
        this.eventAggregator = eventAggregator;
        this.StartIsEnabled = true;
    }

    public DelegateCommand StartDrawingCommand { get; private set; }
    public DelegateCommand StopDrawingCommand { get; private set; }

    public bool StartIsEnabled
    {
        get => this.startIsEnabled;
        set => this.SetProperty(ref this.startIsEnabled, value);
    }

    public bool StopIsEnabled
    {
        get => this.stopIsEnabled;
        set => this.SetProperty(ref this.stopIsEnabled, value);
    }

    public DateTime StartDateTimeValue { get; set; }
    public DateTime StopDateTimeValue { get; set; }

    public async void StartDrawing()
    {
        this.StopIsEnabled = true;
        this.eventAggregator.GetEvent<DrawingTotalTimeInSecondsEvent>().Publish(0);
        this.eventAggregator.GetEvent<IsDrawingInProgressEvent>().Publish(true);
        this.eventAggregator.GetEvent<LuckyNumberDrawnEvent>().Publish(0);
        this.cancellationTokenSource = new CancellationTokenSource();
        await this.StartDrawingLuckyNumber(this.cancellationTokenSource.Token);
    }

    public void StopDrawing()
    {
        this.StartIsEnabled = true;
        this.StopIsEnabled = false;
        this.eventAggregator.GetEvent<IsDrawingInProgressEvent>().Publish(false);
        this.cancellationTokenSource?.Cancel();
        var drawingTotalTimeInSeconds = (this.StopDateTimeValue - this.StartDateTimeValue).TotalSeconds;
        this.eventAggregator.GetEvent<DrawingTotalTimeInSecondsEvent>().Publish(drawingTotalTimeInSeconds);
    }

    public async Task<int> StartDrawingLuckyNumber(CancellationToken cancellationToken)
    {
        var random = new Random();
        var luckyNumber = 0;
        this.StartIsEnabled = false;

        await Task.Run(() =>
                       {
                           try
                           {
                               while (!cancellationToken.IsCancellationRequested)
                               {
                                   luckyNumber = random.Next(1, 99);
                                   this.eventAggregator.GetEvent<LuckyNumberDrawnEvent>().Publish(luckyNumber);
                                   Task.Delay(100, cancellationToken);
                               }

                               cancellationToken.ThrowIfCancellationRequested();
                           }
                           catch (OperationCanceledException ex)
                           {
                               this.StartIsEnabled = true;
                               return luckyNumber;
                           }
                           catch (Exception ex)
                           {
                           }

                           return luckyNumber;
                       }, cancellationToken);

        return luckyNumber;
    }
}