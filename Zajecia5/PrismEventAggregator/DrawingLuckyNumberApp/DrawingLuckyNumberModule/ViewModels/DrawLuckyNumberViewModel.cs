namespace DrawingLuckyNumberModule.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using DrawingLuckyNumber.Core;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    internal class DrawLuckyNumberViewModel : BindableBase
    {
        private CancellationTokenSource cancellationTokenSource;
        private readonly IEventAggregator eventAggregator;

        public DrawLuckyNumberViewModel(IEventAggregator eventAggregator)
        {
            this.StartDrawingCommand = new DelegateCommand(this.StartDrawing);
            this.StopDrawingCommand = new DelegateCommand(this.StopDrawing);
            this.eventAggregator = eventAggregator;
        }

        public DelegateCommand StartDrawingCommand { get; private set; }
        public DelegateCommand StopDrawingCommand { get; private set; }

        private async void StartDrawing()
        {
            this.eventAggregator.GetEvent<IsDrawingInProgressEvent>().Publish(true);
            this.eventAggregator.GetEvent<LuckyNumberDrawnEvent>().Publish(0);
            this.cancellationTokenSource = new CancellationTokenSource();
            await this.StartDrawingLuckyNumber(this.cancellationTokenSource.Token);
        }

        private void StopDrawing()
        {
            this.eventAggregator.GetEvent<IsDrawingInProgressEvent>().Publish(false);
            this.cancellationTokenSource?.Cancel();
        }

        private async Task<bool> StartDrawingLuckyNumber(CancellationToken cancellationToken)
        {
            var random = new Random();

            await Task.Run(() =>
                           {
                               try
                               {
                                   while (!cancellationToken.IsCancellationRequested)
                                   {
                                       var luckyNumber = random.Next(1, 99);
                                       this.eventAggregator.GetEvent<LuckyNumberDrawnEvent>().Publish(luckyNumber);
                                       Task.Delay(100, cancellationToken);
                                   }

                                   cancellationToken.ThrowIfCancellationRequested();
                               }
                               catch (OperationCanceledException ex)
                               {
                               }
                               catch (Exception ex)
                               {
                               }
                           }, cancellationToken);

            return false;
        }
    }
}