﻿namespace ResultsModule.ViewModels
{
    using DrawingLuckyNumber.Core;
    using Prism.Events;
    using Prism.Mvvm;

    internal class ResultsViewModel : BindableBase
    {
        private string drawingStatus;
        private string luckyNumber;

        public ResultsViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<IsDrawingInProgressEvent>().Subscribe(this.DrawingStatusReceived);
            eventAggregator.GetEvent<LuckyNumberDrawnEvent>().Subscribe(this.LuckyNumberReceived);
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
    }
}