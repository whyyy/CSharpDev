﻿namespace ResultsModule.ViewModels;

using System.Windows.Media;
using DrawingLuckyNumber.Core;
using DrawingLuckyNumber.Core.Events;
using Prism.Events;
using Prism.Mvvm;

public class ResultsViewModel : BindableBase
{
    private DrawingStatus drawingStatus;
    private bool isDrawing;
    private string luckyNumber;
    private string drawingTotalTimeInSeconds;

    public ResultsViewModel(IEventAggregator eventAggregator)
    {
        this.DrawingStatus = DrawingStatus.None;
        eventAggregator.GetEvent<DrawingStatusEvent>().Subscribe(this.DrawingReceived);
        eventAggregator.GetEvent<LuckyNumberDrawnEvent>().Subscribe(this.LuckyNumberReceived);
        eventAggregator.GetEvent<DrawingTotalTimeInSecondsEvent>().Subscribe(this.DrawingTotalTimeInSecondsReceived);
    }

    public DrawingStatus DrawingStatus
    {
        get => this.drawingStatus;
        set => SetProperty(ref this.drawingStatus, value);
    }

    public bool IsDrawing
    {
        get => this.isDrawing;
        set => SetProperty(ref this.isDrawing, value);
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

    private void DrawingReceived(DrawingStatus receivedDrawingStatus)
    {
        this.IsDrawing = receivedDrawingStatus is DrawingStatus.InProgress;
        this.DrawingStatus = receivedDrawingStatus;
    }

    private void LuckyNumberReceived(int receivedLuckyNumber)
    {
        if (receivedLuckyNumber == 0)
        {
            this.LuckyNumber = "";
            return;
        }

        this.LuckyNumber = receivedLuckyNumber.ToString();
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